using System.Net.Mime;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc.Formatters;
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
        
        var locations = JsonConvert.DeserializeObject<List<TravelLocation>>(content);
        return new LocationsResponse { Locations = locations };
    }
    
    public async Task<HotelsResponse> GetHotels(HotelsRequest request)
    {
        var participants = request.Participants;
        participants.TryAdd(1, 0);
        participants.TryAdd(2, 0);
        participants.TryAdd(3, 0);
        
        var dates = new
        {
            start = DateTime.ParseExact(request.Dates.Start, "dd-mm-yyyy", null),
            end = DateTime.ParseExact(request.Dates.End, "dd-mm-yyyy", null)
        };
        
        var json = new
        {
            dates,
            cities = request.Cities,
            participants
        };
        
        var httpRequest = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_configurationService.HotelApiUrl + "/hotels"),
            Content = new StringContent(
                JsonConvert.SerializeObject(json),
                Encoding.UTF8,
                MediaTypeNames.Application.Json
            )
        };

        var response = await _httpClient.SendAsync(httpRequest);
        
        var content = await response.Content.ReadAsStringAsync();
        var hotels = JsonConvert.DeserializeObject<List<Hotel>>(content);
        
        return new HotelsResponse { Hotels = hotels };
    }
    
    public async Task<HotelResponse> GetHotel(HotelRequest request)
    {        
        var participants = request.Participants;
        participants.TryAdd(1, 0); 
        participants.TryAdd(2, 0);
        participants.TryAdd(3, 0);

        var dates = new
        {
            start = DateTime.ParseExact(request.Dates.Start, "dd-mm-yyyy", null),
            end = DateTime.ParseExact(request.Dates.End, "dd-mm-yyyy", null)
        };
        
        var json = new
        {
            dates,
            participants,
            hotelId = request.HotelId,
            roomId = request.RoomId
        };
                
        var httpRequest = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_configurationService.HotelApiUrl + "/hotel"),
            Content = new StringContent(
                JsonConvert.SerializeObject(json),
                Encoding.UTF8,
                MediaTypeNames.Application.Json
            )
        };
        
        var response = await _httpClient.SendAsync(httpRequest);
                
        var content = await response.Content.ReadAsStringAsync();
            
        return JsonConvert.DeserializeObject<HotelResponse>(content);
    }
    
}
