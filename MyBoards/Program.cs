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
    app.Run();
