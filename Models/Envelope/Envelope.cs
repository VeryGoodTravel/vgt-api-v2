namespace vgt_api.Models.Envelope
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes API envelope. All responses should be wrapped in an envelope.
    /// </summary>
    public partial class Envelope
    {
        /// <summary>
        /// Message response data should be set here.
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }

        /// <summary>
        /// Response error messages and comments should be set here.
        /// </summary>
        [JsonProperty("message")]
        public string[] Message { get; set; }

        /// <summary>
        /// Describes whether request was handled succesfully.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Timestamp of handled response in 'dd-MM-yyyy hh:mm:ss' format.
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
