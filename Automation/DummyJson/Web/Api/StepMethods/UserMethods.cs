using Common.Data;
using Automation.Common.Helpers;
using Automation.DummyJson.Web.Api.StepMethods.Advanced;
using Reqnroll;

namespace Automation.DummyJson.Web.Api.StepMethods
{
    public class UserMethods
    {
        private static readonly WebTestBase wtb = WebTestBase.GetInstance();

        public static void CreateUserByFirstNameLastName(string firstName, string lastName)
        {
            ResponseBase response = User.CreateByFirstNameLastName(firstName, lastName);
            wtb.Test.AssertAndResume(!response.Failed, response.Msg);
        }

        public static void SubmitCreateUserRequest()
        {
            ResponseBase response = User.SubmitCreateRequest(wtb);
            wtb.Test.AssertAndResume(!response.Failed, response.Msg);
        }
    }
}