using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Text.Json; 
using TechTalk.SpecFlow;

namespace TestLab3
{
    [Binding]
    public class BookingSteps
    {
        private readonly BookingClient _bookingClient;
        private Booking _booking;
        private Booking _updatedBooking;
        private RestResponse<BookingResponse> _createResponse;
        private RestResponse<Booking> _getResponse;
        private RestResponse<Booking> _updateResponse;
        private RestResponse _deleteResponse;
        private int _createdBookingId;

        public BookingSteps()
        {
            _bookingClient = new BookingClient();
        }
       
        [Given(@"I have the following booking data as JSON:")]
        public void GivenIHaveTheFollowingBookingDataAsJSON(string bookingJson)
        {
            
            _booking = JsonSerializer.Deserialize<Booking>(bookingJson);
        }

        [When(@"I send a POST request to create the booking")]
        public async Task WhenISendAPOSTRequestToCreateTheBooking()
        {
            _createResponse = await _bookingClient.CreateBooking(_booking);
            if (!_createResponse.IsSuccessful) Assert.Fail($"API call failed: {_createResponse.StatusCode} - {_createResponse.Content}");
            _createdBookingId = _createResponse.Data.Bookingid;
        }

        [Then(@"the response status code should be (\d+)(?: (.*))?")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode, string statusDescription)
        {
            RestResponse response = _getResponse ?? _deleteResponse ?? _updateResponse ?? (RestResponse)_createResponse;
            Assert.AreEqual((HttpStatusCode)statusCode, response.StatusCode, $"Expected status code {statusCode} but was {(int)response.StatusCode}");
        }

        [Then(@"the response should contain the created booking details")]
        public void ThenTheResponseShouldContainTheCreatedBookingDetails()
        {
            var created = _createResponse.Data.Booking;
            Assert.AreEqual(_booking.Firstname, created.Firstname);
            Assert.AreEqual(_booking.Lastname, created.Lastname);
        }

        [When(@"I send a GET request for the created booking")]
        public async Task WhenISendAGETRequestForTheCreatedBooking()
        {
            _getResponse = await _bookingClient.GetBooking(_createdBookingId);
        }

        [Then(@"the retrieved booking details should match the original details")]
        public void ThenTheRetrievedBookingDetailsShouldMatchTheOriginalDetails()
        {
            var retrieved = _getResponse.Data;
            Assert.AreEqual(_booking.Firstname, retrieved.Firstname);
            Assert.AreEqual(_booking.Lastname, retrieved.Lastname);
        }

        [When(@"I update the booking with the following data as JSON:")]
        public async Task WhenIUpdateTheBookingWithTheFollowingDataAsJSON(string updatedBookingJson)
        {
            _updatedBooking = JsonSerializer.Deserialize<Booking>(updatedBookingJson);
            _updateResponse = await _bookingClient.UpdateBooking(_createdBookingId, _updatedBooking);
            if (!_updateResponse.IsSuccessful) Assert.Fail($"API call failed: {_updateResponse.StatusCode} - {_updateResponse.Content}");
        }

        [Then(@"the response should contain the updated booking details")]
        public void ThenTheResponseShouldContainTheUpdatedBookingDetails()
        {
            var updated = _updateResponse.Data;
            Assert.AreEqual(_updatedBooking.Totalprice, updated.Totalprice);
            Assert.AreEqual(_updatedBooking.Additionalneeds, updated.Additionalneeds);
        }

        [When(@"I send a DELETE request for the created booking")]
        public async Task WhenISendADELETERequestForTheCreatedBooking()
        {
            _deleteResponse = await _bookingClient.DeleteBooking(_createdBookingId);
        }

        [When(@"I send a GET request for the created booking after deletion")]
        public async Task WhenISendAGETRequestForTheCreatedBookingAfterDeletion()
        {
            _deleteResponse = null;
            _updateResponse = null;

            await Task.Delay(2000);
            _getResponse = await _bookingClient.GetBooking(_createdBookingId);
        }
    }
}