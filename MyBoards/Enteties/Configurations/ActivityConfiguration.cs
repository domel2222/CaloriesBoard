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
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> eb)
        {
            eb.Property(x => x.ActivityKind).HasMaxLength(200);
            eb.Property(x => x.RemainingCalories).HasPrecision(14, 2);
            eb.Property(x => x.BurnCalories).HasPrecision(14, 2);
        }
    }
}
