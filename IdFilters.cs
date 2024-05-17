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
    
    public string City { get; set; }
        
    public TravelDateRange Dates { get; set; }
    public int Adults { get; set; }
    public int Children18 { get; set; }
    public int Children10 { get; set; }
    public int Children3 { get; set; }
        
    public override string ToString()
    {
        var hotelName = HotelName.Replace(" ", "_");
        var roomName = RoomName.Replace(" ", "_");
        var city = City.Replace(" ", "_");
        
        return
            $"{HotelId}${hotelName}${RoomId}${roomName}${city}${FlightToId}${FlightFromId}${Dates.Start}${Dates.End}${Adults}${Children18}${Children10}${Children3}";
    }
    
    public IdFilters() {}
    
    private static int GetChildrenCount(Dictionary<int,int> participants, ParticipantsEnum age)
    {
        return participants.ContainsKey((int)age) ? participants[(int)age] : 0;
    }
    
    public IdFilters(SearchFilters filters, Hotel hotel, Room room, Flight flightTo, Flight flightFrom)
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

        City = flightTo.ArrivalAirportName;
        HotelName = hotel.Name;
        RoomName = room.Name;
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
            City = parts[4].Replace("_", " "),
            FlightToId = parts[5],
            FlightFromId = parts[6],
            Dates = new TravelDateRange{ Start = parts[7], End = parts[8] },
            Adults = int.Parse(parts[9]),
            Children18 = int.Parse(parts[10]),
            Children10 = int.Parse(parts[11]),
            Children3 = int.Parse(parts[12])
        };
    }
}