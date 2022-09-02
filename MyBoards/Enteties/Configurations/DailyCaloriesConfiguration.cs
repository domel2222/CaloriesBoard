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
    public class DailyCaloriesConfiguration : IEntityTypeConfiguration<DailyCalories>
    {
        public void Configure(EntityTypeBuilder<DailyCalories> eb)
        {
            eb.Property(x => x.DailyCaloriesIntake).HasColumnType("decimal(7,2)");
        }
    }
}
