using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCaloriesBoards.Enteties.Configurations
{
    public class MealStateConfiguration : IEntityTypeConfiguration<StateMeal>
    {
        public void Configure(EntityTypeBuilder<StateMeal> eb)
        {
            eb.Property(x => x.Value)
                    .IsRequired()
                    .HasMaxLength(50);

            eb.HasMany(x => x.Meals)
                .WithOne(x => x.StateMeal)
                .HasForeignKey(x => x.StateMealId);

            eb.HasData(new StateMeal() { Id = 1, Value = "To Do" },
                            new StateMeal() { Id = 2, Value = "Eating" },
                            new StateMeal() { Id = 3, Value = "Done" }
                );
        }
    }
}
