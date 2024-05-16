using vgt_api.Models.Common;
using vgt_api.Models.Requests;
using vgt_api.Models.Requests.Flights;
using vgt_api.Models.Responses.Flights;

namespace vgt_api.Services;

public class FlightService
{
    public FlightService()
    {
    }
    
    public async Task<DepartureAirports> GetDepartureAirports()
    {
        return new DepartureAirports();
    }
    
    public async Task<FlightResponse> GetFlight(FlightRequest request)
    {
        return new FlightResponse();
    }
    
    public async Task<FlightsResponse> GetFlights(FlightsRequest request)
    {
        return new FlightsResponse();
    }
}