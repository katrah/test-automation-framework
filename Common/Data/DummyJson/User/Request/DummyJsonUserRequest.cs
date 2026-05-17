using System;
using Framework.Library;
using Newtonsoft.Json;

namespace Common.Data.DummyJson.User.Request
{
    [Serializable]
    public class DummyJsonUserRequest
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set ;}

        [JsonProperty("lastName")]
        public string LastName { get; set ;}

        public void AddField(string fieldName, string valStr)
        {
            switch (fieldName.ToLower())
            {
                case "firstname":
                    FirstName = valStr;
                    break;

                case "lastname":
                    LastName = valStr;
                    break;

                default:
                    throw new TestFailureException("Invalid field: {0}", fieldName);
            }
        }
    }
}