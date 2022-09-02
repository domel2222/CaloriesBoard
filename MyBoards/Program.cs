using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using MyBoards.Enteties;
using MyCaloriesBoards.Dto;
using MyCaloriesBoards.Enteties;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(option =>
{
    option.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<CaloriesContext>(
    option => option
    //.UseLazyLoadingProxies() also add property virtual into models for navigation properites
    .UseSqlServer(builder.Configuration.GetConnectionString("MyCaloriesConnectionString"))
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
        .Select(g => new { stateMealId = g.Key, count = g.Count() })
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

app.MapPost("update", async (CaloriesContext db) =>
{
    var period = await db.PeriodOfTimes.FirstAsync(p => p.Id == 1);

    var vomitState = await db.StateMeals.FirstAsync(a => a.Value == "Vomited");
    //period.Area = "Sport Meal";
    //period.Calories = 650;
    //period.StartDate = DateTime.Now;
    period.StateMeal = vomitState;

    await db.SaveChangesAsync();

    return period;
});

app.MapPost("create", async (CaloriesContext db) =>
{
    Tag tag = new Tag()
    {
        Value = "sport drink"
    };

    await db.AddAsync(tag);
    await db.SaveChangesAsync();
});

app.MapPost("createrUser", async (CaloriesContext db) =>
{
    var addres = new Address()
    {
        Id = Guid.Parse("6ca97112-2c4a-457a-96d9-35e83ead7c26"),
        City = "Rzeszów",
        Country = "Poland",
        Street = "Miła"
    };

    var user = new User()
    {
        Email = "UserThird@gmial.com",
        FullName = "Marek Zbik",
        Address = addres
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();
});

app.MapGet("complexRequest", async (CaloriesContext db) =>
{
    var user = await db.Users
        .AsNoTracking()
        .Include(u => u.Comments).ThenInclude(m => m.Meal)
        .Include(user => user.Address)
        .FirstAsync(x => x.Id == Guid.Parse("68366DBE-0809-490F-CC1D-08DA10AB0E61"));

    var tracke = db.ChangeTracker.Entries();
    return user;
});

app.MapDelete("delete", async (CaloriesContext db) =>
{
    //with cascade delete
    var mealTags = await db.MealTag.Where(c => c.MealId == 12).ToListAsync();

    db.MealTag.RemoveRange(mealTags);

    var meal = await db.Meals.FirstAsync(x => x.Id == 16);

    db.RemoveRange(meal);

    //without cascade
    var user = await db.Users.FirstAsync(x => x.Id == Guid.Parse("DC231ACF-AD3C-445D-CC08-08DA10AB0E61"));

    var userComment = await db.Comments.Where(x => x.AuthorId == user.Id).ToListAsync();
    db.RemoveRange(userComment);

    db.Users.Remove(user);

    await db.SaveChangesAsync();
});

app.MapDelete("deleteOnCascade", async (CaloriesContext db) =>
{
    //EF cascade ON
    var user = await db.Users
        .Include(u => u.Comments)
        .FirstAsync(x => x.Id == Guid.Parse("4EBB526D-2196-41E1-CBDA-08DA10AB0E61"));

    db.Users.Remove(user);

    await db.SaveChangesAsync();
});
app.MapGet("attach", async (CaloriesContext db) =>
    {
        var meal = new PeriodOfTime()
        {
            Id = 3
        };

        var entry = db.Attach(meal);
        entry.State = EntityState.Deleted;

        await db.SaveChangesAsync();
    });

app.MapGet("RawSql",  (CaloriesContext db) =>
{
     
    // without parameters
    var status = db.StateMeals
        .FromSqlRaw(
        @"
        SELECT sm.Id, Count(*),sm.Value 
        FROM StateMeals sm
        JOIN Meals m ON sm.Id = m.StateMealId
        GROUP BY sm.Id, sm.Value
        HAVING Count(*) > 85
        "
        ).ToList();

//    db.Database.ExecuteSqlRaw(@"
//            Update Commetns
//            SET UpdatedDate = GETDate(),
//            WHERE AuthotId = 'guid......'
//");

    var mealCount = 85;
    //prevent sql injection with parameter 
    var status2 = db.StateMeals
        .FromSqlInterpolated(
        $@"
        SELECT sm.Id, Count(*),sm.Value 
        FROM StateMeals sm
        JOIN Meals m ON sm.Id = m.StateMealId
        GROUP BY sm.Id, sm.Value
        HAVING Count(*) > {mealCount}
        "
        ).ToList();
    return  status;
});

app.MapGet("dataLaz", async (CaloriesContext db) =>
{
    var user = db.Users
                .FirstOrDefault(x => x.Id == Guid.Parse("E4E81277-99E7-4ACF-CBC5-08DA10AB0E61"));
});

app.MapGet("pagination",async (CaloriesContext db) =>
{
    var filter = "a";
    string sortBy = "FullName"; // email, null, fullname
    bool sortByDesc = false;
    int pageNumber = 1;
    int pageSize = 10;



    var query = db.Users
        .Where(f => filter == null
                            //|| (f.Email.Contains(filter, StringComparison.OrdinalIgnoreCase) || (f.FullName.Contains(filter, StringComparison.OrdinalIgnoreCase))));   not working in EF 
                                || (f.Email.ToLower().Contains(filter.ToLower()) || (f.FullName.ToLower().Contains(filter.ToLower()))));

    var totalCount = query.Count();

    if (sortBy != null)
    {
        //Expression<Func<User, object>> sortByexpresion = user => user.Email;
        var columnsSelector = new Dictionary<string, Expression<Func<User, object>>>()
        {
            //{"Email",  user => user.Email},
            //{"FullName",  user => user.FullName},            
            {nameof(User.Email),  user => user.Email},
            {nameof(User.FullName),  user => user.FullName},
        };

        var sortByExpresssion = columnsSelector[sortBy];
        query = sortByDesc
                ? query.OrderByDescending(sortByExpresssion)
                : query.OrderBy(sortByExpresssion);
                
    }

    var result = query.Skip(pageSize * (pageNumber - 1))
                        .Take(pageSize)
                        .ToList();

    var pageResult = new PageResult<User>(result, totalCount, pageSize, pageNumber);

    return pageResult;
});
app.Run();