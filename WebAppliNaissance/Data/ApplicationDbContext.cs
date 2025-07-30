using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppliNaissance.Models;

namespace WebAppliNaissance.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }

        public DbSet<Naissance> Naissances { get; set; }
        public DbSet<Declarant> Declarants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Naissance>()
                .HasOne(n => n.Declarant)
                .WithMany()
                .HasForeignKey(n => n.DeclarantId);
        }
    }
}
