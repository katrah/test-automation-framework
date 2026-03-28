using System.Reflection;
using Automation.Common.Helpers;
using Common.Data.DummyJson.User.Request;
using Common.Data.DummyJson.User.Response;
using Common.Tools;
using Framework.Library;

namespace Automation.DummyJson.Web.Api.Helpers
{
    public class DummyJsonBaseMethods : MethodsBase
    {
        private readonly TestPerformanceHelper performance;
        private readonly ProxyDummyJsonApi proxyDummyJsonApi;

        public DummyJsonBaseMethods(TestPerformanceHelper testPerfHelper, ProxyDummyJsonApi dummyJsonProxy)
        {
            performance = testPerfHelper;
            proxyDummyJsonApi = dummyJsonProxy;
        }

        public DummyJsonUserResponse CreateUser(DummyJsonUserRequest request)
        {
            DummyJsonUserResponse result = new DummyJsonUserResponse();
            performance.StartTest(MakeKey(MethodBase.GetCurrentMethod().Name), TimeConstants.FiveSec);
            try
            {
                result = proxyDummyJsonApi.CreateUser(request);   
            }
            catch (Exception ex)
            {
                result = new DummyJsonUserResponse { Failed = true, Msg = string.Format("{0}.{1}: {2}: {3}", GetType().Name, MethodBase.GetCurrentMethod().Name, ex.GetType(), ex.Message) };
            }
            performance.EndTest(GetKey(MethodBase.GetCurrentMethod().Name));

            if (result.Failed)
            {
                return result;
            }
            return result;
        }
    }
}