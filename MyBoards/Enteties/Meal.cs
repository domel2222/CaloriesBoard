using MyCaloriesBoards.Enteties;

namespace MyBoards.Enteties

{
    public class PeriodOfTime : Meal
    {
        //epic//period of time
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class DailyCalories : Meal
    {
        //issue//Caloric
        public decimal DailyCaloriesIntake { get; set; }
    }

    public class Activity : Meal
    {
        //task/Activity
        public string ActivityKind { get; set; }
        public decimal BurnCalories { get; set; }
        public decimal RemainingCalories { get; set; }
    }

    public abstract class Meal
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public string WeeklyPath { get; set; }
        public int Calories { get; set; } // priority
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public User Author { get; set; }
        public Guid AuthorId { get; set; }
        public List<Tag> Tags { get; set; }
        public StateMeal StateMeal { get; set; }
        public int StateMealId { get; set; }
    }
}