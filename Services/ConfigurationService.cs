namespace vgt_api.Services;

public class ConfigurationService
{
    private static IConfigurationRoot _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
    
    public string Image => _configuration["Offer:Image"];
    public string SecretKey => _configuration["Jwt:Key"];
    public string HotelApiUrl => _configuration["ApiUrls:Hotel"];
    public string FlightApiUrl => _configuration["ApiUrls:Flight"];
}