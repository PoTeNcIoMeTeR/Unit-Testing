using RestSharp;
using RestSharp.Authenticators; 

namespace TestLab3
{
    public class BookingClient
    {
        private readonly RestClient _client;
        public BookingClient()
        {
            var options = new RestClientOptions("https://restful-booker.herokuapp.com");
            options.Authenticator = new HttpBasicAuthenticator("admin", "password123");
            _client = new RestClient(options);
        }
        public async Task<RestResponse<BookingResponse>> CreateBooking(Booking booking)
        {
            var request = new RestRequest("/booking", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(booking);
            return await _client.ExecuteAsync<BookingResponse>(request);
        }
        public async Task<RestResponse<Booking>> GetBooking(int id)
        {
            var request = new RestRequest($"/booking/{id}", Method.Get);
            request.AddHeader("Accept", "application/json");
            return await _client.ExecuteAsync<Booking>(request);
        }
        public async Task<RestResponse<Booking>> UpdateBooking(int id, Booking booking)
        {
            var request = new RestRequest($"/booking/{id}", Method.Put);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(booking);
            return await _client.ExecuteAsync<Booking>(request);
        }
        public async Task<RestResponse> DeleteBooking(int id)
        {
            var request = new RestRequest($"/booking/{id}", Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            return await _client.ExecuteAsync(request);
        }
    }
}