using Steeltoe.Discovery.Client;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Discovery;

var  policyName = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.Configure<CookBookDatabaseSettings>(builder.Configuration.GetSection("CookBookDatabase"));
builder.Services.AddSingleton<CookBookDB>();
builder.Services.AddDiscoveryClient(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                    builder =>
                    {
                        builder
                            .WithOrigins("*") // specifying the allowed origin
                            .WithMethods("*") // defining the allowed HTTP method
                            .AllowAnyHeader(); // allowing any header to be sent
                    });
});

var app = builder.Build();
app.UseCors(policyName);
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
