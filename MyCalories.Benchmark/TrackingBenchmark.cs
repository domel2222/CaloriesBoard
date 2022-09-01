using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using MyCaloriesBoards.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalories.Benchmark
{
    [MemoryDiagnoser]
    public class TrackingBenchmark
    {
        [Benchmark]
        public int WithTracking()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CaloriesContext>()
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MyCaloriesBoardsDB;Trusted_Connection=True;");

            var _dbContext = new CaloriesContext(optionsBuilder.Options);

            var comments = _dbContext.Comments.ToList();

            return comments.Count();
        }
        [Benchmark]
        public int WithoutTracking()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CaloriesContext>()
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MyCaloriesBoardsDB;Trusted_Connection=True;");

            var _dbContext = new CaloriesContext(optionsBuilder.Options);

            var comments = _dbContext.Comments
                .AsNoTracking()
                .ToList();

            return comments.Count();
        }
    }
}
