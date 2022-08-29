using MyCaloriesBoards.Enteties;

namespace MyBoards.Enteties
{
    public class Meal
    {
        public int Id { get; set; }

        public string Area { get; set; }

        public string WeeklyPath { get; set; }

        public int Calories { get; set; }

        //epic//period of time
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        //issue//Caloric
        public decimal DailyCaloriesIntake { get; set; }

        //task
        public string Activity { get; set; }

        public decimal RemainingCalories { get; set; }
        public string Type { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
        public User Author { get; set;}
        public Guid AuthorId { get; set;}
        public List<Tag> Tags { get; set; }

        public StateMeal StateMeal { get; set; }
        public int StateMealId { get; set; }
    }
}