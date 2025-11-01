using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

namespace TestLab3
{
    [Binding]
    public class CatFactSteps
    {
        private readonly CatFactClient _catFactClient;
        private RestResponse<CatFact> _response;

        public CatFactSteps()
        {
            _catFactClient = new CatFactClient();
        }

        [When(@"I send a GET request to the cat fact API")]
        public async Task WhenISendAGETRequestToTheCatFactAPI()
        {
            _response = await _catFactClient.GetRandomCatFact();
        }

        [Then(@"the cat fact response status code should be 200 OK")]
        public void ThenTheResponseStatusCodeShouldBe200OK()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then(@"the response should contain a cat fact with its length")]
        public void ThenTheResponseShouldContainACatFactWithItsLength()
        {
            var catFact = _response.Data;

            Assert.IsNotNull(catFact, "Response data is null.");

            Assert.IsNotEmpty(catFact.Fact, "Fact string is empty.");

            Assert.Greater(catFact.Length, 0, "Length is not greater than 0.");

            Assert.AreEqual(catFact.Fact.Length, catFact.Length, "Actual fact length does not match the 'length' property.");
        }
    }
}