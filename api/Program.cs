using AutoMapper;
using MySqlConnector;

using api.Utility;
using api.Utility.Validation;

var builder = WebApplication.CreateBuilder(args);

//* Conneciton Strings configuration
builder.Configuration.AddJsonFile("connectionStrings.json");
builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(builder.Configuration.GetConnectionString("SbConnection")));

//* Endpoint Configuration
builder.Services.AddControllers();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

//* Cors Configuration
builder.Services.AddCors(options =>
{
  options.AddPolicy("VueCorsPolicy", b =>
  {
    b
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins(builder.Configuration["FrontendUri"]);
  });
});

//* Configuration for AutoMapper
var config = new MapperConfiguration(cfg =>
{

});
builder.Services.AddSingleton(config.CreateMapper());

//* Dependency Injection  Declaration
// Utility Layer
builder.Services.AddScoped<IXfrHandler, XfrHandler>();
builder.Services.AddScoped<IValidationFactory, ValidationFactory>();
builder.Services.AddSingleton<IValidationHandler, ValidationHandler>();

// Logic Layer
// Repository Layer

var app = builder.Build();
app.UseRouting();
app.UseCors("VueCorsPolicy");
app.UseEndpoints(endpoints =>
{
  endpoints.MapControllers();
});

app.Run();
