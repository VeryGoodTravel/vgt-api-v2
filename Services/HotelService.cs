using System.Web;
using Newtonsoft.Json;
using vgt_api.Models.Common;
using vgt_api.Models.Requests;
using vgt_api.Models.Requests.Hotels;
using vgt_api.Models.Responses.Hotels;

namespace vgt_api.Services;

public class HotelService
{
    private readonly ILogger<HotelService> _logger;
    private readonly HttpClient _httpClient;
    private readonly ConfigurationService _configurationService;
    
    
    public HotelService(HttpClient httpClient, ConfigurationService configurationService, ILogger<HotelService> logger)
    {
        _httpClient = httpClient;
        _configurationService = configurationService;
        _logger = logger;
    }
    
    public async Task<LocationsResponse> GetLocations()
    {
        var response = await _httpClient.GetAsync(_configurationService.HotelApiUrl + "/locations");
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<LocationsResponse>(content);
    }
    
    public async Task<HotelsResponse> GetHotels(HotelsRequest request)
    {
        var builder = new UriBuilder(_configurationService.HotelApiUrl + "/hotels");
        var query = HttpUtility.ParseQueryString(builder.Query);
        query["dates"] = JsonConvert.SerializeObject(request.Dates);
        if (request.Cities != null)
            query["cities"] = JsonConvert.SerializeObject(request.Cities);
        query["participants"] = JsonConvert.SerializeObject(request.Participants);
        builder.Query = query.ToString();
        _logger.LogInformation($"Requesting hotels: {builder}");
        var response = await _httpClient.GetAsync(builder.ToString());
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<HotelsResponse>(content);
    }
    
    public async Task<HotelResponse> GetHotel(HotelRequest request)
    {
        // hotel
        return new HotelResponse();
    }
    
}