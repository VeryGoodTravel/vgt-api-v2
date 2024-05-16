using vgt_api.Models.Common;
using vgt_api.Models.Requests;

namespace vgt_api;

public class IdFilters
{
    public string HotelId { get; set; }
    public string RoomId { get; set; }
    public string FlightToId { get; set; }
    public string FlightFromId { get; set; }
        
    public TravelDateRange Dates { get; set; }
    public int Adults { get; set; }
    public int Children18 { get; set; }
    public int Children10 { get; set; }
    public int Children3 { get; set; }
        
    public override string ToString()
    {
        return
            $"H{HotelId}R{RoomId}T{FlightToId}F{FlightFromId}S{Dates.Start}E{Dates.End}A{Adults}Y{Children18}C{Children10}I{Children3}";
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
    }
        
    public static IdFilters FromId(string id)
    {
        var filters = new IdFilters();
        
        var parts = id.Split('H', 'R', 'T', 'F', 'S', 'E', 'A', 'Y', 'C', 'I');
        
        filters.HotelId = parts[1];
        filters.RoomId = parts[2];
        filters.FlightToId = parts[3];
        filters.FlightFromId = parts[4];
        filters.Dates = new TravelDateRange { Start = parts[5], End = parts[6] };
        filters.Adults = int.Parse(parts[7]);
        filters.Children18 = int.Parse(parts[8]);
        filters.Children10 = int.Parse(parts[9]);
        filters.Children3 = int.Parse(parts[10]);
        
        return filters;
    }
}