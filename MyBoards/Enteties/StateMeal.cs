using MyBoards.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCaloriesBoards.Enteties
{
    public class StateMeal
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public List<Meal> Meals { get; set; } = new List<Meal>();
    }
}
