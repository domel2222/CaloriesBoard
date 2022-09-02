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
    public class PeriodOfTimeConfiguration : IEntityTypeConfiguration<PeriodOfTime>
    {
        public void Configure(EntityTypeBuilder<PeriodOfTime> eb)
        {
            eb.Property(x => x.EndDate).HasPrecision(3);
        }
    }
}
