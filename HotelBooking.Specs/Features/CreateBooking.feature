Feature: CreateBooking

@mytag
Scenario: Start date and end date before occupied range
	Given the start date is 2021-11-04
	And the end date is 2021-11-05
	When the method 'CreateBooking' is called
	Then the result should return true

@mytag
Scenario: Start date and end date after the occupied range
	Given the start date is 2022-01-22
	And the end date is 2022-01-24
	When the method 'CreateBooking' is called
	Then the result should return true

@mytag
Scenario: Start date is before and end date is after the occupied range
	Given the start date is 2021-11-06
	And the end date is 2021-11-22
	When the method 'CreateBooking' is called
	Then the result should return false

@mytag
Scenario Outline: Start date is before and end date is during the occupied range
	Given the start date is <StartDate>
	And the end date is <EndDate>
	When the method 'CreateBooking' is called
	Then the result should return false

	Examples:
	| StartDate  | EndDate    |
	| 2021-11-05 | 2021-11-08 |
	| 2021-11-05 | 2021-11-20 |

@mytag
Scenario Outline: Start date is during and end date is after the occupied range
	Given the start date is <StartDate>
	And the end date is <EndDate>
	When the method 'CreateBooking' is called
	Then the result should return false

	Examples:
	| StartDate  | EndDate    |
	| 2021-11-08 | 2021-11-22 |
	| 2021-11-20 | 2021-11-22 |

@mytag
Scenario Outline: Start date and end date is during the occupied range
	Given the start date is <StartDate>
	And the end date is <EndDate>
	When the method 'CreateBooking' is called
	Then the result should return false

	Examples:
	| StartDate  | EndDate    |
	| 2021-11-08 | 2021-11-09 |
	| 2021-11-19 | 2021-11-21 |
	| 2021-11-08 | 2021-11-21 |