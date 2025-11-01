using System.Text.Json.Serialization;

namespace TestLab3
{
    public class BookingResponse
    {
        [JsonPropertyName("bookingid")]
        public int Bookingid { get; set; }

        [JsonPropertyName("booking")]
        public Booking Booking { get; set; }
    }
}