using MyBoards.Enteties;

namespace MyCaloriesBoards.Enteties
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public List<Meal> Meals { get; set; } = new List<Meal>();
    }
}