Feature: Booking API CRUD

Scenario: Full CRUD cycle for a booking
    
    Given I have the following booking data as JSON:
        """
        {
            "firstname": "Jim",
            "lastname": "Brown",
            "totalprice": 111,
            "depositpaid": true,
            "bookingdates": {
                "checkin": "2023-01-01",
                "checkout": "2024-01-01"
            },
            "additionalneeds": "Breakfast"
        }
        """
    When I send a POST request to create the booking
    Then the response status code should be 200 OK
    And the response should contain the created booking details
   
    When I send a GET request for the created booking
    Then the response status code should be 200 OK
    And the retrieved booking details should match the original details
    
    When I update the booking with the following data as JSON:
        """
        {
            "firstname": "Jim",
            "lastname": "Brown",
            "totalprice": 222,
            "depositpaid": true,
            "bookingdates": {
                "checkin": "2023-01-01",
                "checkout": "2024-01-01"
            },
            "additionalneeds": "Breakfast and Lunch"
        }
        """
    Then the response status code should be 200 OK
    And the response should contain the updated booking details
    When I send a DELETE request for the created booking
    Then the response status code should be 200 OK
    When I send a GET request for the created booking after deletion
    Then the response status code should be 404 Not Found