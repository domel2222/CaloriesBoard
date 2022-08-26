using Microsoft.EntityFrameworkCore;
using MyBoards.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCaloriesBoards.Enteties
{
    public class CaloriesContext : DbContext
    {
        public CaloriesContext(DbContextOptions<CaloriesContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
    
}
