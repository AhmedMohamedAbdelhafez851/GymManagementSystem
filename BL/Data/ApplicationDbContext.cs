using Domains;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MemberShipsPlan> MemberShipsPlans { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Attendance> Attendances { get; set; } // Added DbSet
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed default data
            //modelBuilder.Entity<MemberShipsPlan>().HasData(
            //    new MemberShipsPlan { MemberShipsPlanId = 1, MemberShipPlanName = "Basic Plan", Durations = "1 Month", Price = 50 },
            //    new MemberShipsPlan { MemberShipsPlanId = 2, MemberShipPlanName = "Standard Plan", Durations = "3 Months", Price = 120 },
            //    new MemberShipsPlan { MemberShipsPlanId = 3, MemberShipPlanName = "Premium Plan", Durations = "6 Months", Price = 200 }
            //);


            

        }
    }
}
