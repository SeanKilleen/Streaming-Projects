﻿Feature: Submitting Submission
	
Scenario: Input from submission page is saved

	Assumption: There are no entries in the database

	Given the filled out submission entry form
		| Label       | Value                    |
		| Url         | https://www.specflow.org |
		| Type        | Blog Posts               |
		| Email       | example@example.org      |
		| Description | foo                      |
	And the privacy policy is accepted

	When the submission entry form is submitted
	Then there is 'one' submission entry stored

Scenario: Entered values from submission page is saved

	Given the filled out submission entry form
		| Label       | Value                    |
		| Url         | https://www.specflow.org |
		| Type        | Blog Posts               |
		| Email       | youremail@example.org    |
		| Description | Test Input               |
	And the privacy policy is accepted

	When the submission entry form is submitted
	Then there is a submission entry stored with the following data:
		| Url                      | Type       | Email                 | Description |
		| https://www.specflow.org | Blog Posts | youremail@example.org | Test Input  |