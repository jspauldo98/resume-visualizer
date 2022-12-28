using AutoMapper;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("connectionStrings.json");
builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(builder.Configuration.GetConnectionString("SbConnection")));

builder.Services.AddControllers();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

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

// TODO - auto mapper configuration here

// TODO - Dependency injection declarations here

var app = builder.Build();
app.UseRouting();
app.UseCors("VueCorsPolicy");
app.UseEndpoints(endpoints =>
{
  endpoints.MapControllers();
});

app.Run();
