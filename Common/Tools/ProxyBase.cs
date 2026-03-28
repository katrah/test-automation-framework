using Framework.Library;
using System.Collections.Generic;
using System.Net;
using System.Reflection; // Used in Debug mode, don't remove
using Newtonsoft.Json;
using Framework.Library.Extensions;

namespace Common.Tools
{
    public abstract class ProxyBase
    {
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        protected ProxyBase(IntegrationTestContext context)
        {
            if (context == null)
            {
                throw new TestFrameworkException(string.Format("{0} must have a valid TestContext", GetType().Name), new ArgumentNullException("context"));
            }
            Context = context;
        }

        #region Members
        /// <summary>
        /// Test Context proxy is running in
        /// </summary>
        protected IntegrationTestContext Context { get; set; }

        /// <summary>
        /// Server type for validation purposes
        /// </summary>
        protected string ServerType { get; set; }

        /// <summary>
        /// The Base URL to which web requests will be sent
        /// </summary>
        protected string BaseUrl { get; set; }

        /// <summary>
        /// Last Response the Proxy Received (for tracking)
        /// </summary>
        public HttpResponseWrapper LastResponse { get; protected set; }

        /// <summary>
        /// The Order URL used to send web requests that are separate than the base URL
        /// </summary>
        protected string OrderUrl { get; set; }
        #endregion

        #region Methodd
        public T PostJson<T>(object body, string uriPath, PostOptions options = null)
        {
            if (options == null)
            {
                options = new PostOptions();
            }
#if DEBUG
            HttpResponseWrapper httpWrapper = sendRequest("POST", uriPath, body, options.Headers, options.FullEncode, options.NumberOfTries, options.CookieContainer, options.BodyType);
            try
            {
                T tmp = httpWrapper.Deserialize<T>();
                return tmp;
            }
            catch (Exception ex)
            {
                Context.Controller.FailAndResume(string.Format("{0}.{1}: {2}: {3}{4}Response Body: {5}", GetType().Name, MethodBase.GetCurrentMethod().Name, ex.GetType(), ex.Message,
                    Environment.NewLine, httpWrapper.ResponseBody));
                throw;
            }
#endif
            return sendRequest("POST", uriPath, body, options.Headers, options.FullEncode, options.NumberOfTries, options.CookieContainer, options.BodyType).Deserialize<T>();
        }

        public HttpResponseWrapper PostJson(string uriPath, object body, PostOptions options = null)
        {
            if (body == null)
            {
                throw new TestProxyException("Invalid POST body", new ArgumentNullException("jsonSerializableBody"));
            }
            if (options == null)
            {
                options = new PostOptions();
            }
            return sendRequest("POST", uriPath, body, options.Headers, options.FullEncode, options.NumberOfTries, null, options.BodyType);
        }

        public T Patch<T>(object jsonSerializableBody, string uriPath, WebHeaderCollection headers = null, bool fullEncode = false, uint numberOfTries = 1)
        {
            if (jsonSerializableBody == null)
            {
                throw new TestProxyException("Invalid PUT body", new ArgumentNullException("jsonSerializableBody"));
            }
            return sendRequest("PATCH", uriPath, jsonSerializableBody, headers, fullEncode, numberOfTries, null).Deserialize<T>();
        }

        public T Put<T>(object jsonSerializableBody, string uriPath, WebHeaderCollection headers = null, bool fullEncode = false, uint numberOfTries = 1)
        {
            if (jsonSerializableBody == null)
            {
                throw new TestProxyException("Invalid PUT body", new ArgumentNullException("jsonSerializableBody"));
            }
            return sendRequest("PUT", uriPath, jsonSerializableBody, headers, fullEncode, numberOfTries, null).Deserialize<T>();
        }

        public T Get<T>(string uriPath, WebHeaderCollection headers = null, bool fullEncode = false, uint numberOfTries = 1)
        {
            return sendRequest("GET", uriPath, null, headers, fullEncode, numberOfTries, null).Deserialize<T>();
        }

        public HttpResponseWrapper Get(string uriPath, WebHeaderCollection headers = null, CookieContainer cookieContainer = null, bool fullEncode = false, uint numberOfTries = 1)
        {
            return sendRequest("GET", uriPath, null, headers, fullEncode, numberOfTries, cookieContainer);
        }

        public T Delete<T>(string uriPath, WebHeaderCollection headers = null, bool fullEncode = false, uint numberOfTries = 1)
        {
            return sendRequest("DELETE", uriPath, null, headers, fullEncode, numberOfTries, null).Deserialize<T>();
        }

        private HttpResponseWrapper sendRequest(string method, string url, object postBodyObject, WebHeaderCollection headers, bool fullEncode, uint numberOfTries,
            CookieContainer cookieContainer, PostType bodyType = PostType.Json)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new TestProxyException("Invalid Url value", new ArgumentNullException("url"));
            }

            if (Context.UseInstanceToken && string.IsNullOrEmpty(Context.ActiveInstanceToken))
            {
                throw new TestProxyException("Cannot find Instance Token. Either the proxy is not authenticated or the user is not authorized in the specified Instance Code");
            }

            if (numberOfTries == 0)
            {
                throw new ArgumentException("numberOfTries should be > 0");
            }

            uint triesLeft = numberOfTries;
            string upperMethod = method.ToUpper();

            while (true)
            {
                try
                {
                    string requestUrl = url;
                    if (fullEncode)
                    {
                        requestUrl = HttpClientHelper.HtmlEncode(url, fullEncode);
                    }

                    Context.Controller.Log("{0}: Sending {1} Request: {2}", GetType().Name, upperMethod, url);

                    string postBody = null;
                    if (postBodyObject != null)
                    {
                        if (bodyType == PostType.UrlEncoded || bodyType == PostType.MultipartFile)
                        {
                            postBody = postBodyObject.ToString();
                        }
                        else
                        {
                            postBody = JsonConvert.SerializeObject(postBodyObject, serializerSettings);
                        }
                    }

                    // Log headers
                    List<string> headerList = new List<string>();
                    if (headers != null && headers.AllKeys != null)
                    {
                        foreach (string header in headers.AllKeys)
                        {
                            if (!string.IsNullOrEmpty(header))
                            {
                                string value = headers.Get(header) ?? "<null>";
                                headerList.Add(header + ":" + value);
                            }
                        }
                    }
                    else
                    {
                        headerList.Add("No headers provided");
                    }

                    Context.Controller.Log("{0}: Request Body: {1}{2}{3}", GetType().Name, postBody ?? "<None>", Environment.NewLine, string.Join(Environment.NewLine, headerList));

                    // Send request
                    HttpResponseWrapper httpResponse = HttpClientHelper.SendHttpRequest(requestUrl, method, postBody, headers, cookieContainer, bodyType);
                     
                    // Store last response
                    LastResponse = httpResponse; 

                    // Check for success
                    if (LastResponse.ResponseCode != HttpStatusCode.OK && LastResponse.ResponseCode != HttpStatusCode.Created)
                    {
                        string responseHeaders = LastResponse.ResponseHeaders?.DumpHeaders() ?? "<None>";
                        throw new TestProxyException("Unexpected Response. Status Code: {0}; Response Body: {1}", LastResponse.ResponseCode, LastResponse.ResponseBody ?? "<None>");
                    }

                    return LastResponse;
                }
                catch (Exception e)
                {
                    triesLeft --;
                    if (triesLeft == 0)
                    {
                        throw new TestProxyException(string.Format("{0}: {1} Request Failed: {2}", GetType().Name, upperMethod, e));
                    }

                    Context.Controller.Warn("{0}: {1} Request Failed (Tries Left: {2} : {3})", GetType().Name, upperMethod, --triesLeft, e);
                }
            }
        }
        #endregion
    }
}