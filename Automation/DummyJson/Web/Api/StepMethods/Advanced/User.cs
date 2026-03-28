using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automation.Common.Helpers;
using Automation.Common.Reqnroll;
using Common.Data;
using Common.Data.DummyJson.User.Request;
using Common.Data.DummyJson.User.Response;
using Framework.Library;
using Reqnroll;

namespace Automation.DummyJson.Web.Api.StepMethods.Advanced
{
    [Binding]
    public class User
    {
        private static readonly Storage storage = Storage.GetInstance();

        public static ResponseBase CreateByFirstNameLastName(string firstName, string lastName)
        {
            DummyJsonUserRequest userRequest;
            if (!storage.GetCurrent(StringConstants.UserRequest, out userRequest))
            {
                userRequest = new DummyJsonUserRequest();
            }
            userRequest.FirstName = firstName;
            userRequest.LastName = lastName;
            storage.SetValue(userRequest, StringConstants.UserRequest);
            return new ResponseBase();
        }

         public static ResponseBase SubmitCreateRequest(WebTestBase wtb)
        {
            DummyJsonUserRequest userRequest;
            if (!storage.GetCurrent(StringConstants.UserRequest, out userRequest))
            {
                return new ResponseBase { Failed = true, Msg = "Couldn't find the DummyJsonCreateUserRequest" };
            }

            DummyJsonUserResponse response = wtb.DummyJson.CreateUser(userRequest);
            if (response.Failed)
            {
                return response;
            }
            storage.SetValue(response, StringConstants.UserRequest);
            return new ResponseBase();
        }
    }
}