using TechTalk.SpecFlow;

namespace AppointmentPlannerTests.Steps.Doctors;

[Binding]
public sealed class AddDoctorStep
{
	private readonly ScenarioContext _scenarioContext;

	public AddDoctorStep(ScenarioContext scenarioContext)
	{
		_scenarioContext = scenarioContext;
	}

	[When(@"I fill and submit the New Doctor form with the following data")]
	public void WhenIFillAndSubmitTheNewDoctorFormWithTheFollowingData(Table table)
	{
		throw new PendingStepException();
	}

	[Then(@"I verify that the new doctor is added successfully")]
	public void ThenIVerifyThatTheNewDoctorIsAddedSuccessfully()
	{
		throw new PendingStepException();
	}
}
