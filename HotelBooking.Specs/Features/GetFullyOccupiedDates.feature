Feature: GetFullyOccupiedDates

@mytag
Scenario: End date after start date throws error
	Given the start date is 2021-11-03
	And the end date is 2021-10-28
	When the method 'GetFullyOccupiedDate' is called error thrown
	Then the result should be The start date cannot be later than the end date.