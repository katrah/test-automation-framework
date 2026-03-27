using System;
using Newtonsoft.Json;

namespace Common.Data.DummyJson.User.Response
{
    [Serializable]
    public class DummyJsonUserResponse : ResponseBase
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set ;}

        [JsonProperty("LastName")]
        public string LastName { get; set ;}

        [JsonProperty("id")]
        public int Id { get; set ;}
    }
}