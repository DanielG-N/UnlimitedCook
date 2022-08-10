using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<UserDB>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("userDb1")));


var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
