using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoards.Enteties
{
    public class Meal
    {
        public int MealId { get; set; }
        public string State { get; set; }
        public string Area { get; set; }
        public string IterationWeeklyPath { get; set; }
        public int Priority { get; set; }

        //epic//period of time
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        //issue//Caloric
        public decimal CaloriesIntake { get; set; }

        //task 
        public string Activity { get; set; }
        public decimal RemainingCalories { get; set; }

        public string Type { get; set; }

    }
}
