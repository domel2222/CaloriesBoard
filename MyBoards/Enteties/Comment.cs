using MyBoards.Enteties;

namespace MyCaloriesBoards.Enteties
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Message { get; set; }
        public User Author { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Meal Meal { get; set; }
        public int MealId { get; set; }
    }
}