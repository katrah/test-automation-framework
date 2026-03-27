
using Automation.Common.Reqnroll;
using Framework.Library;
using Reqnroll;
using Automation.DummyJson.Web.Api.StepMethods;

namespace Automation.DummyJson.Web.Api.StepDefinitions
{
    [Binding]
    public sealed class TestSteps
    {
        private static readonly Storage storage = Storage.GetInstance();
        //   ex. [Given(@"^(?i)(?:I\s)?use (?:the\s)?(?:following\s)?columns in an AffinityOrg(?:Find|Search) request:(.*)")]

        [Given(@"^(?i)(?:I\s)?create a user with (?:the\s)?first name (.*) and (?:the\s)?last name (.*)")]
        public void GivenCreateUserByFirstNameLastName(string firstName, string lastName)
        {
            switch (storage.GetWebTestMethod())
            {
                case WebTestMethodType.WebApi:
                    UserMethods.CreateUserByFirstNameLastName(firstName, lastName);
                    break;

                case WebTestMethodType.WebUi:
                    // UI test would go here 
                    break;
            }
        }
    }
}