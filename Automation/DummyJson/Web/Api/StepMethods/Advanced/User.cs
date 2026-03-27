using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automation.Common.Reqnroll;
using Common.Data;
using Reqnroll;

namespace Automation.DummyJson.Web.Api.StepMethods.Advanced
{
    [Binding]
    public class User
    {
        private static readonly Storage storage = Storage.GetInstance();

        public static ResponseBase CreateByFirstNameLastName(string firstName, string lastName)
        {
            //... 
            return new ResponseBase();
        }
    }
}