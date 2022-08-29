using MyBoards.Enteties;

namespace MyCaloriesBoards.Enteties
{
    public class Tag
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public List<Meal> Meals { get; set; }
    }
}