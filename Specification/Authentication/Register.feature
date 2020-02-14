Feature: Registering a User for Authentication
	In order to join and use the system
	As an anonymous user
	I want to be able to register myself to the system.

@authentication @register @positive
Scenario Outline: Allow an anonymous user to join the system as a registered user.
	Given I have created the endpoint "/users/register"
	And I have prepared a "POST" request
	And the prepared request has a RegisterModel parameter
	And the prepared RegisterModel parameter has a "FirstName" parameter whose value is "<FirstName>"
	And the prepared RegisterModel parameter has a "LastName" parameter whose value is "<LastName>"
	And the prepared RegisterModel parameter has a "Username" parameter whose value is "<Username>"
	And the prepared RegisterModel parameter has a "EmailAddress" parameter whose value is "<EmailAddress>"
	And the prepared RegisterModel parameter has a "Password" parameter whose value is "<Password>"
	And the prepared RegisterModel parameter has a "ConfirmPassword" parameter whose value is "<Password>"
	When I submit the prepared request to "/users/register"
	Then the endpoint will return a status code of "200"
	And the endpoint will return a body of '{"id":<ID>}'

	Examples:
		| FirstName | LastName | Username     | EmailAddress              | Password      | ID |
		| Joe       | Bloggs   | Joe.Bloggs   | joebloggs@gmail.com       | P4ssw0rd      | 1  |
		| Harry     | Potter   | Harry.Potter | harry.potter@hogwarts.com | I4mth3Ch0sen1 | 2  |

@authentication @register @negative
Scenario Outline: Disallow a user to join the system when an existing user already exists with a specific username.
	Given I have created the endpoint "/users/register"
	And I have registered a user with the details (FirstName: "<FirstName>", LastName: "<LastName>", Username: "<Username>", Email Address: "<EmailAddress>", Password: "<Password>", ConfirmPassword: "<Password>")
	And I have prepared a "POST" request
	And the prepared request has a RegisterModel parameter
	And the prepared RegisterModel parameter has a "FirstName" parameter whose value is "<FirstName>"
	And the prepared RegisterModel parameter has a "LastName" parameter whose value is "<LastName>"
	And the prepared RegisterModel parameter has a "Username" parameter whose value is "<Username>"
	And the prepared RegisterModel parameter has a "EmailAddress" parameter whose value is "<EmailAddress>"
	And the prepared RegisterModel parameter has a "Password" parameter whose value is "<Password>"
	And the prepared RegisterModel parameter has a "ConfirmPassword" parameter whose value is "<Password>"
	When I submit the prepared request to "/users/register"
	Then the endpoint will return a status code of "400"
	And the endpoint will return a body of '{"Code":2,"Message":"That username is taken."}'

	Examples:
		| FirstName | LastName | Username   | EmailAddress        | Password |
		| Joe       | Bloggs   | Joe.Bloggs | joebloggs@gmail.com | P4ssw0rd |

@authentication @register @negative
Scenario Outline: Disallow a user to join the system when submitting Password and ConfirmPassword do not match.
	Given I have created the endpoint "/users/register"
	And I have prepared a "POST" request
	And the prepared request has a RegisterModel parameter
	And the prepared RegisterModel parameter has a "FirstName" parameter whose value is "<FirstName>"
	And the prepared RegisterModel parameter has a "LastName" parameter whose value is "<LastName>"
	And the prepared RegisterModel parameter has a "Username" parameter whose value is "<Username>"
	And the prepared RegisterModel parameter has a "EmailAddress" parameter whose value is "<EmailAddress>"
	And the prepared RegisterModel parameter has a "Password" parameter whose value is "<Password>"
	And the prepared RegisterModel parameter has a "ConfirmPassword" parameter whose value is "<ConfirmPassword>"
	When I submit the prepared request to "/users/register"
	Then the endpoint will return a status code of "400"
	And the endpoint will return a body of '{"Code":3,"Message":"Password and ConfirmPassword do not match."}'

	Examples:
		| FirstName | LastName | Username   | EmailAddress        | Password | ConfirmPassword |
		| Joe       | Bloggs   | Joe.Bloggs | joebloggs@gmail.com | P4ssw0rd | Password        |

@authentication @register @negative
Scenario Outline: Disallow a user to join the system when submitting missing data.
	Given I have created the endpoint "/users/register"
	And I have prepared a "POST" request
	And the prepared request has a RegisterModel parameter
	And the prepared RegisterModel parameter has a "FirstName" parameter whose value is "<FirstName>"
	And the prepared RegisterModel parameter has a "LastName" parameter whose value is "<LastName>"
	When I submit the prepared request to "/users/register"
	Then the endpoint will return a status code of "400"

	Examples:
		| FirstName | LastName |
		| Joe       | Bloggs   |
