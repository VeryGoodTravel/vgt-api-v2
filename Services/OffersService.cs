using Newtonsoft.Json;
using vgt_api.Models.Common;
using vgt_api.Models.Requests;
using vgt_api.Models.Requests.Flights;
using vgt_api.Models.Requests.Hotels;

namespace vgt_api.Services;

public class OffersService
{
    private readonly HotelService _hotelService;
    private readonly FlightService _flightService;
    private readonly ILogger<OffersService> _logger;
    
    public OffersService(HotelService hotelService, FlightService flightService, ILogger<OffersService> logger)
    {
        _hotelService = hotelService;
        _flightService = flightService;
        _logger = logger;
    }
    
    public async Task<TravelOffer> GetOffer(string id)
    {
        var filters = IdFilters.FromId(id);
        
        var participants = new Dictionary<int,int>()
        {
            { (int)ParticipantsEnum.Child10, filters.Children10 },
            { (int)ParticipantsEnum.Child18, filters.Children18 },
            { (int)ParticipantsEnum.Adult, filters.Adults }
        };
        
        HotelRequest request = new HotelRequest()
        {
            HotelId = filters.HotelId,
            RoomId = filters.RoomId,
            Participants = participants,
            Dates = filters.Dates
        };
        
        var hotel = await _hotelService.GetHotel(request);
        var room = hotel.Rooms.FirstOrDefault(r => r.RoomId == filters.RoomId);
        
        int numberOfParticipants = participants.Sum(x => x.Value);
        FlightRequest flightToRequest = new FlightRequest(filters.FlightToId, numberOfParticipants);
        var flightTo = await _flightService.GetFlight(flightToRequest);
        FlightRequest flightFromRequest = new FlightRequest(filters.FlightFromId, numberOfParticipants);
        var flightFrom = await _flightService.GetFlight(flightFromRequest);
        
        var available = flightTo.Available && flightFrom.Available && room != null;
        
        return new TravelOffer(available, filters, hotel, room, flightTo, flightFrom);
    }

    private async Task<Tuple<Flight, Flight>?> GetHotelPairOfFlights(FlightsRequest flightsToRequest, FlightsRequest flightsFromRequest)
    {
        //_logger.LogInformation(JsonConvert.SerializeObject(flightsToRequest));
       // _logger.LogInformation(JsonConvert.SerializeObject(flightsFromRequest));
        
        var flightsToResponse = await _flightService.GetFlights(flightsToRequest);
        var flightsFromResponse = await _flightService.GetFlights(flightsFromRequest);
        
        foreach (var flightTo in flightsToResponse.Flights)
        {
            foreach (var flightFrom in flightsFromResponse.Flights)
            {
                if (flightTo.DepartureAirportCode == flightFrom.ArrivalAirportCode)
                {
                    return new Tuple<Flight, Flight>(flightTo, flightFrom);
                }
            }
        }
        
        return null;
    }

    public async Task<Tuple<List<TravelOffer>,int>> GetOffers(int offset, int limit, SearchFilters filters)
    {
        var hotelsRequest = filters.ToHotelsRequest();
        var numberOfParticipants = filters.Participants.Sum(x => x.Value);

        var flightsToRequest = new FlightsRequest()
        {
            DepartureDate = filters.Dates.Start,
            ArrivalAirportCodes = new(),
            DepartureAirportCodes = filters.Origins.FirstOrDefault().Value,
            NumberOfPassengers = numberOfParticipants
        };
        
        var flightsFromRequest = new FlightsRequest()
        {
            DepartureDate = filters.Dates.End,
            ArrivalAirportCodes = filters.Origins.FirstOrDefault().Value,
            DepartureAirportCodes = new(),
            NumberOfPassengers = numberOfParticipants
        };
        
        var hotelsResponse = await _hotelService.GetHotels(hotelsRequest);

       // _logger.LogInformation(JsonConvert.SerializeObject(hotelsResponse));
        
        var cachedFlights = new Dictionary<string, Tuple<Flight, Flight>?>();
        _logger.LogInformation($"count {hotelsResponse.Hotels.Count}");
        foreach (var hotel in hotelsResponse.Hotels.ToList())
        {   

            var hotelAirports = new List<string>() { hotel.AirportCode };
            
            _logger.LogInformation(JsonConvert.SerializeObject(hotelAirports));
            
            flightsToRequest.ArrivalAirportCodes = hotelAirports;
            flightsFromRequest.DepartureAirportCodes = hotelAirports;
            
            //flightsToRequest.ArrivalAirportCodes.Add(hotel.AirportCode);
            //flightsFromRequest.DepartureAirportCodes.Add(hotel.AirportCode);

            _logger.LogInformation("departure:");
            _logger.LogInformation(JsonConvert.SerializeObject(flightsFromRequest.DepartureAirportCodes));

            if (!cachedFlights.ContainsKey(hotel.AirportCode))
            {
                cachedFlights[hotel.AirportCode] =
                    await GetHotelPairOfFlights(flightsToRequest, flightsFromRequest);
            }
        }
        
        int counter = 0;
        var offers = new List<TravelOffer>();
        foreach (Hotel hotel in hotelsResponse.Hotels)
        {
            if (cachedFlights[hotel.AirportCode] == null) continue;
            foreach (Room room in hotel.Rooms)
            {
                if (counter++ >= offset && counter <= offset + limit)
                {
                     offers.Add(new TravelOffer(
                        true, filters, hotel, room, 
                        cachedFlights[hotel.AirportCode]!.Item1,
                        cachedFlights[hotel.AirportCode]!.Item2));
                }
            }
        }

        return new Tuple<List<TravelOffer>,int>(offers, counter);
    }
    
}
