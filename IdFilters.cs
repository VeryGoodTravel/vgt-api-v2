using vgt_api.Models.Common;
using vgt_api.Models.Requests;

namespace vgt_api;

public class IdFilters
{
    public string HotelId { get; set; }
    public string HotelName { get; set; }
    public string RoomId { get; set; }
    public string RoomName { get; set; }
    public string FlightToId { get; set; }
    public string FlightFromId { get; set; }
    
    public string DepartureCity { get; set; }
    
    public string ArrivalCity { get; set; }
        
    public TravelDateRange Dates { get; set; }
    public int Adults { get; set; }
    public int Children18 { get; set; }
    public int Children10 { get; set; }
    public int Children3 { get; set; }
    public string DestinationCity { get; set; }
    public string Maintenance { get; set; }
    public string Transportation { get; set; }
    public override string ToString()
    {
        var hotelName = HotelName.Replace(" ", "_");
        var roomName = RoomName.Replace(" ", "_");
        var departureCity = DepartureCity.Replace(" ", "_");
        var arrivalCity = ArrivalCity.Replace(" ", "_");
        var maintenance = Maintenance.Replace(" ", "_");
        var transportation = Transportation.Replace(" ", "_");
        
        return
            $"{HotelId}${hotelName}${RoomId}${roomName}${departureCity}${arrivalCity}${FlightToId}${FlightFromId}${Dates.Start}${Dates.End}${Adults}${Children18}${Children10}${Children3}${DestinationCity}${maintenance}${transportation}";
    }
    
    public IdFilters() {}
    
    private static int GetChildrenCount(Dictionary<int,int> participants, ParticipantsEnum age)
    {
        return participants.ContainsKey((int)age) ? participants[(int)age] : 0;
    }
    
    public IdFilters(SearchFilters filters, Hotel hotel, Room room, Flight flightTo, Flight flightFrom, string maintenance, string transportation)
    {
        int children18 = GetChildrenCount(filters.Participants, ParticipantsEnum.Child18);
        int children10 = GetChildrenCount(filters.Participants, ParticipantsEnum.Child10);
        int children3 = GetChildrenCount(filters.Participants, ParticipantsEnum.Child3);
        
        HotelId = hotel.HotelId;
        RoomId = room.RoomId;
        FlightToId = flightTo.FlightId;
        FlightFromId = flightFrom.FlightId;
        Dates = filters.Dates;
        Adults = filters.Participants[(int)ParticipantsEnum.Adult];
        Children18 = children18;
        Children10 = children10;
        Children3 = children3;

        DepartureCity = flightTo.DepartureAirportName;
        ArrivalCity = flightTo.ArrivalAirportName;
        HotelName = hotel.Name;
        RoomName = room.Name;
        DestinationCity = hotel.City;
        Maintenance = maintenance;
        Transportation = transportation;
    }
        
    public static IdFilters FromId(string id)
    {
        var parts = id.Split('$');
        return new IdFilters
        {
            HotelId = parts[0],
            HotelName = parts[1].Replace("_", " "),
            RoomId = parts[2],
            RoomName = parts[3].Replace("_", " "),
            DepartureCity = parts[4].Replace("_", " "),
            ArrivalCity = parts[5].Replace("_", " "),
            FlightToId = parts[6],
            FlightFromId = parts[7],
            Dates = new TravelDateRange{ Start = parts[8], End = parts[9] },
            Adults = int.Parse(parts[10]),
            Children18 = int.Parse(parts[11]),
            Children10 = int.Parse(parts[12]),
            Children3 = int.Parse(parts[13]),
            DestinationCity = parts[14],
            Maintenance = parts[15],
            Transportation = parts[16]
        };
    }
}