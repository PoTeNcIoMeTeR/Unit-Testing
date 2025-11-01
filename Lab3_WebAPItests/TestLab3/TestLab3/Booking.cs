using System.Text.Json.Serialization;

namespace TestLab3
{
    public class Booking
    {
        [JsonPropertyName("firstname")]
        public string Firstname { get; set; }

        [JsonPropertyName("lastname")]
        public string Lastname { get; set; }

        [JsonPropertyName("totalprice")]
        public int Totalprice { get; set; }

        [JsonPropertyName("depositpaid")]
        public bool Depositpaid { get; set; }

        [JsonPropertyName("bookingdates")]
        public BookingDates Bookingdates { get; set; }

        [JsonPropertyName("additionalneeds")]
        public string Additionalneeds { get; set; }
    }
}