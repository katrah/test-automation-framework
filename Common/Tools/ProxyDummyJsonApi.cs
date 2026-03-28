using System.Net;
using Common.Data.DummyJson.User.Request;
using Common.Data.DummyJson.User.Response;
using Framework.Library;

namespace Common.Tools
{
    public class ProxyDummyJsonApi : ProxyBase
    {
        private readonly WebHeaderCollection headers = new WebHeaderCollection();

        public ProxyDummyJsonApi(IntegrationTestContext context)
            : base(context)
        {
            ServerType = "DummyJsonAPI";
            BaseUrl = context.GetUrl(EndpointUrlType.DummyJsonApiBase);
            Context = context;
            // Add headers if needed:
            //headers.Add("header", "header");
        }

        public DummyJsonUserResponse CreateUser(DummyJsonUserRequest request)
        {
            return PostJson<DummyJsonUserResponse>(request, setUrl("users/add"),
            new PostOptions
            {
                BodyType = PostType.Json,
                Headers = headers
            });
        }

        private string setUrl(string uriPath)
        {
            return string.Format("{0}/{1}", BaseUrl, uriPath);
        }
    }
}