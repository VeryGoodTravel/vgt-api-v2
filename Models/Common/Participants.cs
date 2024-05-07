using Newtonsoft.Json;

namespace vgt_api.Models.Common;

public class Participants
{
    [JsonProperty("max")]
    public long Max { get; set; }

    [JsonProperty("min")]
    public long Min { get; set; }

    [JsonProperty("options")]
    public ParticipantOption[] Options { get; set; }
    
    public static Participants GetExample()
    {
        return new Participants
        {
            Max = 10,
            Min = 1,
            Options = new ParticipantOption[]
            {
                new ParticipantOption
                {
                    Id = "1",
                    Min = 0,
                    Max = 2,
                    Label = "Infant"
                },
                new ParticipantOption
                {
                    Id = "2",
                    Min = 0,
                    Max = 4,
                    Label = "Child"
                },
                new ParticipantOption
                {
                    Id = "3",
                    Min = 1,
                    Max = 10,
                    Label = "Adult"
                }
            }
        };
    }
}