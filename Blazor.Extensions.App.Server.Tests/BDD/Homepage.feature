Feature: Homepage Testing

Homepage testing

@Homepage
Scenario: Homepage is displayed successfully
	Given Web application is running
	When user navigates to the homepage
	Then homepage should be displayed with the welcome text
