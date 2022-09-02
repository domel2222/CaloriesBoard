using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoards.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCaloriesBoards.Enteties.Configurations
{
    public class Mealconfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> eb)
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
        }
    }
}
