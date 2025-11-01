using RestSharp;

namespace TestLab3
{
    public class CatFactClient
    {
        private readonly RestClient _client;

        public CatFactClient()
        {
            var options = new RestClientOptions("https://catfact.ninja");
            _client = new RestClient(options);
        }

        public async Task<RestResponse<CatFact>> GetRandomCatFact()
        {
            
            var request = new RestRequest("/fact", Method.Get);
            return await _client.ExecuteAsync<CatFact>(request);
        }
    }
}