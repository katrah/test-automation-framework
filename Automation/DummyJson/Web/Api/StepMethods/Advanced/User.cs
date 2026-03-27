using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automation.Common.Reqnroll;
using Common.Data;
using Common.Data.DummyJson.User.Request;
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
    }
}