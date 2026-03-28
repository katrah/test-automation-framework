Feature: Users
    Testing user entity and actions

Scenario: Create a user
  Given I create a user with first name "Daria" and last name "Quinn"
  And I submit the DummyJsonCreateUser request
  When I search for the user by returned id
  Then I verify the new user exists