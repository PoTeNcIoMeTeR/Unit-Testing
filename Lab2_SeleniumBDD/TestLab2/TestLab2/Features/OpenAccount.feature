Feature: Open Customer Account

As a bank manager
I want to be able to open new accounts for existing customers
In order to expand banking services

@smoke
Scenario: Manager successfully opens a new dollar account for an existing customer
    Given the bank manager is on the main banking page
    When he clicks on the "Bank Manager Login" button
    And he navigates to the "Open Account" tab
    And he selects the customer "Harry Potter" from the list
    And he selects the currency "Dollar"
    And he clicks the "Process" button
    Then he should see an alert with the text about successful account creation
    And the new account should be visible in the customer list for "Harry Potter" 
   