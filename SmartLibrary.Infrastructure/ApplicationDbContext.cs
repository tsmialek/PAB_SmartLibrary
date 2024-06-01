using Microsoft.EntityFrameworkCore;
using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set up guids
            var adminUserId = Guid.NewGuid();
            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();

            // Add amin user
            var adminUser = new User
            {
                Id = adminUserId,
                FirstName = "Tomasz",
                LastName = "Smialek",
                Email = "admin@wsei.pl",
                Password = "admin",
            };

            // Add basic roles
            var adminRole = new Role
            {
                Id = adminRoleId,
                Name = "Admin"
            };

            var userRole = new Role
            {
                Id = userRoleId,
                Name = "User"
            };

            // Add many-to-many relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.HasData(
                    new { UsersId = adminUserId, RolesId = adminRoleId }
                ));

            modelBuilder.Entity<User>().HasData(adminUser);
            modelBuilder.Entity<Role>().HasData(adminRole, userRole);

        }
    }
}
