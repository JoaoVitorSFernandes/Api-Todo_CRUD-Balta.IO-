using Todo.Controllers;
using Todo.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<TodoContext>();

var app = builder.Build();

app.MapControllers();

app.Run();
