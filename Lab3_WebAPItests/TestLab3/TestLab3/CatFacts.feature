Feature: Cat Facts API

Scenario: Retrieve a random cat fact
    When I send a GET request to the cat fact API
    Then the cat fact response status code should be 200 OK
    And the response should contain a cat fact with its length