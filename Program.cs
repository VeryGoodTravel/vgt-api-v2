using vgt_api.Controllers;
using vgt_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<ConfigurationService>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddSingleton<HotelService>();
builder.Services.AddSingleton<FlightService>();
builder.Services.AddSingleton<OffersService>();
builder.Services.AddSingleton<StatsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
