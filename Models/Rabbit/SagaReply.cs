using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace vgt_api.Models.Rabbit;

public enum SagaAnswer
{
    Success,
    HotelFailure,
    FlightFailure,
    PaymentFailure,
    HotelAndFlightFailure
}

/// <summary>
/// reply from the OrderService to all the backends
/// notifies the backends of the finished saga transaction
/// It is sent to all instances of the backend
/// </summary>
public record struct SagaReply()
{
    /// <summary>
    /// Guid of the SAGA transaction
    /// </summary>
    public Guid TransactionId { get; set; }
    
    /// <summary>
    /// ID of the offer as specified by the backend
    /// </summary>
    public int OfferId { get; set; }
    
    [JsonConverter(typeof(StringEnumConverter))]
    public SagaAnswer Answer { get; set; }
    
}