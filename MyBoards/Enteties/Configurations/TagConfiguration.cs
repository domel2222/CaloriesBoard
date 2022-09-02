using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCaloriesBoards.Enteties.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasData(
                    new Tag() { Id = 1, Value = "Brakfast" },
                    new Tag() { Id = 2, Value = "Lunch" },
                    new Tag() { Id = 3, Value = "Cocktail" },
                    new Tag() { Id = 4, Value = "Dinner" },
                    new Tag() { Id = 5, Value = "Snack" }
                    );
        }
    }
}
