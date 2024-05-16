using Newtonsoft.Json;
using vgt_api.Models.Common;
using vgt_api.Models.Requests;
using vgt_api.Models.Requests.Hotels;
using vgt_api.Models.Responses.Hotels;

namespace vgt_api.Services;

public class HotelService
{
    private readonly HttpClient _httpClient;
    
    public HotelService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<LocationsResponse> GetLocations()
    {
        return new LocationsResponse();
    }
    
    public async Task<HotelsResponse> GetHotels(HotelsRequest request)
    {
        return new HotelsResponse();
    }
    
    public async Task<HotelResponse> GetHotel(HotelRequest request)
    {
        return new HotelResponse();
    }
    
}