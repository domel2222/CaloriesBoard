using MyBoards.Enteties;

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