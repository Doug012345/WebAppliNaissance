using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using WebAppliNaissance.Data;
using WebAppliNaissance.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Ajouter Identity avec les rôles
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Exception filter pour les erreurs en dev
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Ajout des contrôleurs + Razor pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddTransient<IEmailSender, NewEmailSender>();

var app = builder.Build();

// Middleware HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Seed des rôles et admin
await SeedRolesAndAdminAsync(app.Services);

app.Run();

async Task SeedRolesAndAdminAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roleNames = { "Admin", "Agent", "User" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    string adminEmail = "admin@mairie.com";
    string adminPassword = "Admin123!";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            // Confirmer l'email explicitement
            adminUser.EmailConfirmed = true;
            await userManager.UpdateAsync(adminUser);

            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        else
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            foreach (var error in result.Errors)
            {
                logger.LogError("Erreur lors de la création de l'admin: {Error}", error.Description);
            }
        }
    }
}
