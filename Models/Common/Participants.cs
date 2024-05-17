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
                    Id = ((int)ParticipantsEnum.Adult).ToString(),
                    Min = 1,
                    Max = 10,
                    Label = "Dorosły"
                },
                new ParticipantOption
                {
                    Id = ((int)ParticipantsEnum.Child18).ToString(),
                    Min = 0,
                    Max = 4,
                    Label = "Dziecko < 18 lat"
                },
                new ParticipantOption
                {
                    Id = ((int)ParticipantsEnum.Child18).ToString(),
                    Min = 0,
                    Max = 3,
                    Label = "Dziecko < 10 lat"
                },
                new ParticipantOption
                {
                    Id = ((int)ParticipantsEnum.Child3).ToString(),
                    Min = 0,
                    Max = 2,
                    Label = "Dziecko < 3 lat"
                },
            }
        };
    }
}