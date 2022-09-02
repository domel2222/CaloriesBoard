using Microsoft.EntityFrameworkCore;
using MyBoards.Enteties;
using MyCaloriesBoards.Enteties.Configurations;
using MyCaloriesBoards.Enteties.ViewModels;

namespace MyCaloriesBoards.Enteties
{
    public class CaloriesContext : DbContext
    {
        public CaloriesContext(DbContextOptions<CaloriesContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<PeriodOfTime> PeriodOfTimes { get; set; }
        public DbSet<DailyCalories> DailyCaloriesAmount { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<StateMeal> StateMeals { get; set; }

        public DbSet<MealTag> MealTag { get; set; }
        public DbSet<TopAuthor> ViewTopAuthors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasKey(x => new { x.Email, x.LastName});
            //new AddressConfiguration().Configure(modelBuilder.Entity<Address>());

            //get all configuration 
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            //modelBuilder.Entity<PeriodOfTime>(eb =>
            //{
            //    eb.Property(x => x.EndDate).HasPrecision(3);
            //});

            //modelBuilder.Entity<MealTag>()
            //        .HasKey(x => new { x.TagId, x.MealId });
        }
    }
}