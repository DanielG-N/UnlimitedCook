var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.Configure<RecipeDatabaseSettings>(builder.Configuration.GetSection("RecipeDatabase"));
builder.Services.AddSingleton<RecipeDB>();

var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
