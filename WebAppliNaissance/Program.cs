using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using WebAppliNaissance.Data;
using WebAppliNaissance.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔗 Configuration de la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 🔐 Ajout de l'identité avec gestion des rôles
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 🔧 Configuration des services
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailSender, NewEmailSender>();

var app = builder.Build();

// 🧱 Middlewares HTTP
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

// 🚀 Seed des rôles + utilisateurs initiaux
await SeedRolesAndUsersAsync(app.Services);

app.Run();


// 🔁 Méthode pour créer rôles + comptes initiaux
async Task SeedRolesAndUsersAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    string[] roles = { "Admin", "Agent", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
            logger.LogInformation($"Rôle '{role}' créé.");
        }
    }

    // 👤 Admin
    string adminEmail = "admin@mairie.com";
    string adminPassword = "Admin123!";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
            logger.LogInformation("Admin créé avec succès.");
        }
        else
        {
            foreach (var error in result.Errors)
                logger.LogError("Erreur Admin: {Error}", error.Description);
        }
    }

    // 👤 Agent
    string agentEmail = "agent@mairie.com";
    string agentPassword = "Agent123!";
    var agentUser = await userManager.FindByEmailAsync(agentEmail);
    if (agentUser == null)
    {
        agentUser = new IdentityUser { UserName = agentEmail, Email = agentEmail, EmailConfirmed = true };
        var result = await userManager.CreateAsync(agentUser, agentPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(agentUser, "Agent");
            logger.LogInformation("Agent créé avec succès.");
        }
        else
        {
            foreach (var error in result.Errors)
                logger.LogError("Erreur Agent: {Error}", error.Description);
        }
    }
}
