var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RecipeDatabaseSettings>(builder.Configuration.GetSection("BookStoreDatabase"));
builder.Services.AddSingleton<RecipeDB>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
