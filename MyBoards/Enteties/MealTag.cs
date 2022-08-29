using MyBoards.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCaloriesBoards.Enteties
{
    public class MealTag
    {
        public Meal Meal { get; set; }
        public int MealId { get; set; }

        public Tag Tag { get; set; }
        public int TagId { get; set; }
        public DateTime PublicationDate { get; set; }

    }
}
