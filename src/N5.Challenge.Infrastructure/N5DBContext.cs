using Microsoft.EntityFrameworkCore;
using N5.Challenge.Domain.Entities;

namespace N5.Challenge.Infrastructure
{
    public class N5DBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }


        public N5DBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Employee>().OwnsOne(x => x.Name);
            modelBuilder.Entity<Employee>().OwnsOne(x => x.LastName);


            modelBuilder.Entity<PermissionType>().HasKey(e => e.Id);
            modelBuilder.Entity<PermissionType>().OwnsOne(x => x.Name);

            modelBuilder.Entity<Permission>().HasKey(e => e.Id);

            modelBuilder.Entity<Employee>().HasMany(o => o.Permissions)
                .WithOne(o => o.Employee).HasForeignKey(o => o.EmployeeId);

            modelBuilder.Entity<PermissionType>().HasMany(o => o.Permissions)
                .WithOne(o => o.PermissionType).HasForeignKey(o => o.PermissionTypeId);
        }
    }
}
