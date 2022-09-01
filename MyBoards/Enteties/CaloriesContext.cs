﻿using Microsoft.EntityFrameworkCore;
using MyBoards.Enteties;
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

            modelBuilder.Entity<Meal>(eb =>
            {
                eb.Property(x => x.Area).HasColumnType("nvarchar(200)");
                eb.Property(x => x.WeeklyPath).HasColumnName("Weekly_Path");
                eb.Property(x => x.Calories).HasDefaultValue(350);

                eb.HasMany(x => x.Comments)
                    .WithOne(c => c.Meal)
                    .HasForeignKey(m => m.MealId);
                eb.HasOne(x => x.Author)
                    .WithMany(a => a.Meals)
                    .HasForeignKey(m => m.AuthorId);

                eb.HasMany(t => t.Tags)
                    .WithMany(m => m.Meals)
                    .UsingEntity<MealTag>(
                        t => t.HasOne(mt => mt.Tag)
                        .WithMany()
                        .HasForeignKey(mt => mt.TagId),

                        m => m.HasOne(mt => mt.Meal)
                        .WithMany()
                        .HasForeignKey(mt => mt.MealId),

                        mt =>
                        {
                            mt.HasKey(x => new { x.TagId, x.MealId });
                            mt.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                        }
                    );
            });

            modelBuilder.Entity<PeriodOfTime>(eb =>
            {
                eb.Property(x => x.EndDate).HasPrecision(3);
            });

            modelBuilder.Entity<Activity>(eb =>
            {
                eb.Property(x => x.ActivityKind).HasMaxLength(200);
                eb.Property(x => x.RemainingCalories).HasPrecision(14, 2);
                eb.Property(x => x.BurnCalories).HasPrecision(14, 2);
            });

            modelBuilder.Entity<DailyCalories>(eb =>
            {
                eb.Property(x => x.DailyCaloriesIntake).HasColumnType("decimal(7,2)");
            });

            modelBuilder.Entity<StateMeal>(eb =>
            {
                eb.Property(x => x.Value)
                    .IsRequired()
                    .HasMaxLength(50);

                eb.HasMany(x => x.Meals)
                    .WithOne(x => x.StateMeal)
                    .HasForeignKey(x => x.StateMealId);

                eb.HasData(new StateMeal() {Id = 1, Value = "To Do" },
                                new StateMeal() {Id = 2, Value = "Eating"},
                                new StateMeal() {Id = 3, Value = "Done"}
                    );
            });

            modelBuilder.Entity<Comment>(eb =>
            {
                eb.HasOne(x => x.Author)
                    .WithMany(x => x.Comments)
                    .HasForeignKey(x => x.AuthorId)
                    .OnDelete(DeleteBehavior.ClientCascade);
                
                eb.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
            });

            modelBuilder.Entity<User>(eb =>
            {
                eb.HasOne(x => x.Address)
                  .WithOne(x => x.User)
                  .HasForeignKey<Address>(x => x.UserId);
            });

            modelBuilder.Entity<Tag>()
                .HasData(
                    new Tag() { Id = 1, Value = "Brakfast" },
                    new Tag() { Id = 2, Value = "Lunch" },
                    new Tag() { Id = 3, Value = "Cocktail" },
                    new Tag() { Id = 4, Value = "Dinner" },
                    new Tag() { Id = 5, Value = "Snack" }
                );

            modelBuilder.Entity<TopAuthor>(eb =>
            {
                eb.ToView("View_TopAuthors");
                eb.HasNoKey();
            });

            modelBuilder.Entity<Address>()
                .OwnsOne(a => a.Coordinate, onb =>
                {
                    onb.Property(x => x.Latiitude).HasPrecision(18, 7);
                    onb.Property(x => x.Longitude).HasPrecision(18, 7);
                });
            //modelBuilder.Entity<MealTag>()
            //        .HasKey(x => new { x.TagId, x.MealId });
        }
    }
}