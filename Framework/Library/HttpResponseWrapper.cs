using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Framework.Library
{
    public class HttpResponseWrapper
    {
        public CookieContainer CookieJar { get; private set; }

        public HttpResponseMessage Response { get; private set; }

        public string ResponseBody { get; private set; }

        public HttpStatusCode ResponseCode { get; private set; }

        public WebHeaderCollection ResponseHeaders { get; private set; }

        public HttpClient Request { get; private set; }

        public Uri RequestUri { get; set; }

        public HttpResponseWrapper(HttpClient request, HttpResponseMessage response, 
            CookieContainer cookieContainer = null)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if(response == null)
            {
                throw new ArgumentNullException("response");
            }

            Request = request;
            Response = response;
            ResponseCode = response.StatusCode;
            RequestUri = request.BaseAddress;
            CookieJar = cookieContainer; //Optional 

            if(response.Content != null)
            {
                ResponseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // Map HttpClient headers to WebHeaderCollection for backward compatibility: 
            ResponseHeaders = new WebHeaderCollection();
            foreach (KeyValuePair<string, IEnumerable<string>> header in response.Headers)
            {
                ResponseHeaders[header.Key] = string.Join(",", header.Value);
            }

            if (response.Content != null)
            {
                foreach (KeyValuePair<string, IEnumerable<string>> header in response.Content.Headers)
                {
                    ResponseHeaders[header.Key] = string.Join(",", header.Value);
                }
            }
        }

        // Constructor for error scenario with just status code and message:
        public HttpResponseWrapper(HttpClient request, HttpStatusCode statusCode, string errorMessage,
            CookieContainer cookieContainer = null)
        {
            Request = request;
            ResponseCode = statusCode;
            ResponseBody = errorMessage;
            CookieJar = cookieContainer;
            ResponseHeaders = new WebHeaderCollection();
        }

        public T Deserialize<T>()
        {
            if (string.IsNullOrEmpty(ResponseBody))
            {
                throw new TestProxyException("Cannot Deserialize Empty Response");
            }
            return JsonConvert.DeserializeObject<T>(ResponseBody);
        }
    }
}
