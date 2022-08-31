using Microsoft.EntityFrameworkCore;
using MyCaloriesBoards.Enteties;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CaloriesContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyCaloriesConnectionString"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbCentext = scope.ServiceProvider.GetService<CaloriesContext>();

var pendingMigration = dbCentext.Database.GetPendingMigrations();
if (pendingMigration.Any())
{
    dbCentext.Database.Migrate();
}

var users = dbCentext.Users.ToList();

if (!users.Any())
{
    var user1 = new User()
    {
        Email = "user1@gmail.com",
        FullName = "User One",
        Address = new Address()
        {
            City = "Warszawa",
            Street = "Wolska"
        }
    };

    var user2 = new User()
    {
        Email = "user2@gmail.com",
        FullName = "User Second",
        Address = new Address()
        {
            City = "Bydgoszcz",
            Street = "Warszawska"
        }
    };
    dbCentext.Users.AddRange(user1, user2);
    dbCentext.SaveChanges();
}

app.MapGet("data", (CaloriesContext db) =>
{

    //var tags = db.Tags.ToList();
    //var calories = db.DailyCaloriesAmount.First();

    var ToDo = db.Meals.Where(x => x.StateMealId == 1).ToList();
    //return new {tags, calories };
    return new { ToDo };
});

app.MapGet("comment", async (CaloriesContext db) =>
        {
            var date = new DateTime(2022, 7, 20);

            var newComments = await db
                        .Comments
                        .Where(x => x.CreatedDate > date)
                        .OrderByDescending(c => c.CreatedDate)
                        .Take(5)
                        .ToListAsync();

            return newComments;
        });
app.MapGet("meal", async (CaloriesContext db) =>
{
    var statecount = await db.Meals
        .GroupBy(x => x.StateMealId)
        .Select(g => new { stateMealId = g.Key, count = g.Count()})
        .ToListAsync();

    return statecount;

});
app.MapGet("caloriesAndComment", async (CaloriesContext db) =>
{
    var onHold = await db.PeriodOfTimes
        .Where(x => x.StateMealId == 4)
        .OrderBy(x => x.Calories)
        .ToListAsync();

    var userCount = await db.Users
        .OrderByDescending(x => x.Comments.Count())
        .FirstOrDefaultAsync();

    var authorsCommentCount = await db.Comments
            .GroupBy(x => x.AuthorId)
            .Select(g => new { g.Key, Count = g.Count() })
            .ToListAsync();

    var topAuthor = authorsCommentCount
                    .First(x => x.Count == authorsCommentCount.Max(a => a.Count));

    var userDetails = db.Users.First(a => a.Id == topAuthor.Key);
                
    
    return new { userDetails, comment = authorsCommentCount.Count };
});

app.Run();
