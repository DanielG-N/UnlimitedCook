var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.Configure<CookBookDatabaseSettings>(builder.Configuration.GetSection("CookBookDatabase"));
builder.Services.AddSingleton<CookBookDB>();

var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
