namespace vgt_api.Services;

public class ConfigurationService
{
    private static IConfigurationRoot _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();
    
    public string Image => _configuration["Offer:Image"];
    public string SecretKey => _configuration["Jwt:Key"];
    public string HotelApiUrl => $"http://{Environment.GetEnvironmentVariable("HOTEL_API")}";
    public string FlightApiUrl => $"http://{Environment.GetEnvironmentVariable("FLIGHT_API")}";
    public string BrokerUrl => $"http://{Environment.GetEnvironmentVariable("RABBIT_HOST")}";
}