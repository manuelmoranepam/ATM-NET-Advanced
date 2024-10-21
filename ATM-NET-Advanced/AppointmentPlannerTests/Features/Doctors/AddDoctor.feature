Feature: AddDoctor

As a Software Test Automation Engineer,
I would like to regression test the Add Doctor functionality.

@Regression @Doctors
Scenario: Verify that the user is able to add a new doctor
	Given I navigate to the Doctors page
	When I fill and submit the New Doctor form with the following data
		| FirstName | LastName | IsMale | Department | Education | Experience | Designation         | DutyTiming          |
		| John      | Doe      | true   | Cardiology | MBBS      | 5+ years   | Senior Cardiologist | 08:00 AM - 05:00 PM |
	Then I verify that the new doctor is added successfully
