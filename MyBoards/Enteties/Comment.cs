using MyBoards.Enteties;

namespace MyCaloriesBoards.Enteties
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Meal Meal { get; set; }
        public int MealId { get; set; }
    }
}