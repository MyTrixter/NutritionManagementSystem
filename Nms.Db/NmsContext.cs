using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nms.Db.Entities;

namespace Nms.Db
{
    public class NmsContext : IdentityDbContext<IdentityUser>
    {
        public NmsContext(DbContextOptions<NmsContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<MealPlanItem> MealPlanItems { get; set; }
        public DbSet<ScheduledMealPlan> ScheduledMealPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MealPlanItem>()
                .HasOne(m => m.MealPlan)
                .WithMany(mp => mp.MealPlanItems)
                .HasForeignKey(m => m.MealPlanId)
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}