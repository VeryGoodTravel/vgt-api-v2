using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using vgt_api.Models.Requests.Flights;
using vgt_api.Models.Responses.Flights;

namespace vgt_api.Services;

public class FlightService
{
    private readonly HttpClient _httpClient;
    private readonly ConfigurationService _configurationService;
    private readonly ILogger<FlightService> _logger;
    
    public FlightService(ConfigurationService configurationService, HttpClient httpClient, 
        ILogger<FlightService> logger)
    {
        _configurationService = configurationService;
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<DepartureAirports> GetDepartureAirports()
    {
        var response = await _httpClient.GetAsync(_configurationService.FlightApiUrl + "/departure_airports");
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<DepartureAirports>(content);
    }
    
    public async Task<FlightResponse> GetFlight(FlightRequest request)
    {
        var json = new
        {
            request.FlightId,
            request.NumberOfPassengers
        };
        
        var httpRequest = new HttpRequestMessage()
        { 
            Method = HttpMethod.Post,
            RequestUri = new Uri(_configurationService.FlightApiUrl + "/flight"),
            Content = new StringContent(
                JsonConvert.SerializeObject(json),
                Encoding.UTF8, 
                MediaTypeNames.Application.Json
            )
        };
                
        var response = await _httpClient.SendAsync(httpRequest); 
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<FlightResponse>(content);
    }
    
    public async Task<FlightsResponse> GetFlights(FlightsRequest request)
    {
        var date = DateTime.ParseExact(request.DepartureDate, "dd-MM-yyyy", null);
        var json = new
        {
            request.DepartureAirportCodes,
            request.ArrivalAirportCodes,
            DepartureDate = date,
            request.NumberOfPassengers
        };
                
        var httpRequest = new HttpRequestMessage()
        { 
            Method = HttpMethod.Post,
            RequestUri = new Uri(_configurationService.FlightApiUrl + "/flights"),
            Content = new StringContent(
                JsonConvert.SerializeObject(json),
                Encoding.UTF8, 
                MediaTypeNames.Application.Json
                )
        };
        
        var response = await _httpClient.SendAsync(httpRequest); 
        var content = await response.Content.ReadAsStringAsync();

        var flightsResponse = new FlightsResponse
        {
            Flights = JsonConvert.DeserializeObject<List<FlightResponse>>(content) ?? new List<FlightResponse>()
        };
        return flightsResponse;
    }
}