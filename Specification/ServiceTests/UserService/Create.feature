Feature: Creating a User
	In order to allow a user to login
	As a developer
	I want to be able to store a new user in a database via a service.

	Background:
	Given I have configured a connection string

@authentication @service @positive
Scenario: Create a valid user on the database.
	Given I prepare a RegisterModel parameter
	And the prepared RegisterModel parameter has a "FirstName" parameter whose value is "<FirstName>"
	And the prepared RegisterModel parameter has a "LastName" parameter whose value is "<LastName>"
	And the prepared RegisterModel parameter has a "Username" parameter whose value is "<Username>"
	And the prepared RegisterModel parameter has a "EmailAddress" parameter whose value is "<EmailAddress>"
	And the prepared RegisterModel parameter has a "Password" parameter whose value is "<Password>"
	And the prepared RegisterModel parameter has a "ConfirmPassword" parameter whose value is "<Password>"
	When I call the UserService.Create method with the prepared parameters
	Then a CreateResponse object is returned
	And the CreateResponse object has a User object parameter
	And the CreateResponse.User object has a "FirstName" parameter whose value is "<FirstName>"
	And the CreateResponse.User object has a "LastName" parameter whose value is "<LastName>"
	And the CreateResponse.User object has a "Username" parameter whose value is "<Username>"
	And the CreateResponse.User object has a "EmailAddress" parameter whose value is "<EmailAddress>"
	And the CreateResponse object has a Error object parameter whose value is null
	And the database will contain a User record with the following data: "{ 'FirstName': '<FirstName>', 'LastName': '<LastName>', 'Username': '<Username>', 'EmailAddress': '<EmailAddress>' }"
