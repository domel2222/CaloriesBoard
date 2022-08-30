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

var  pendingMigration = dbCentext.Database.GetPendingMigrations();
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
    var tags = db.Tags.ToList();
    return tags;
});
app.Run();
