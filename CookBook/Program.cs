using Steeltoe.Discovery.Client;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Discovery;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.Configure<CookBookDatabaseSettings>(builder.Configuration.GetSection("CookBookDatabase"));
builder.Services.AddSingleton<CookBookDB>();
builder.Services.AddDiscoveryClient(builder.Configuration);

var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
