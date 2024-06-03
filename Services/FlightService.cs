using System.Net.Mime;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using vgt_api.Models.Common;
using vgt_api.Models.Requests;
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
        var builder = new UriBuilder(_configurationService.FlightApiUrl + "/flight");
        var query = HttpUtility.ParseQueryString(builder.Query);
        query["flight_id"] = request.FlightId;
        query["number_of_passengers"] = request.NumberOfPassengers.ToString();
        builder.Query = query.ToString();
        var response = await _httpClient.GetAsync(builder.ToString());
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<FlightResponse>(content);
    }
    
    public async Task<FlightsResponse> GetFlights(FlightsRequest request)
    {
        var date = DateTime.ParseExact(request.DepartureDate, "dd-mm-yyyy", null);
        _logger.LogInformation(JsonConvert.SerializeObject(request)); 
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
        
        _logger.LogInformation(JsonConvert.SerializeObject(httpRequest));
        
        var response = await _httpClient.SendAsync(httpRequest); 
        var content = await response.Content.ReadAsStringAsync();
        
        _logger.LogInformation(JsonConvert.SerializeObject(content));
        
        return JsonConvert.DeserializeObject<FlightsResponse>(content);
    }
}