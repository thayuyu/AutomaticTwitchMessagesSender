using System.Text.Json.Serialization;

namespace TwitchAutomaticMessageSend.Models
{
    public class TwitchMessage
    {
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; set; }

        [JsonPropertyName("sender_id")]
        public string SenderId { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
