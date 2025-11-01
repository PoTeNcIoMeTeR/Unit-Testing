using System.Text.Json.Serialization;

namespace TestLab3
{
    public class BookingDates
    {
        [JsonPropertyName("checkin")]
        public string Checkin { get; set; }

        [JsonPropertyName("checkout")]
        public string Checkout { get; set; }
    }
}