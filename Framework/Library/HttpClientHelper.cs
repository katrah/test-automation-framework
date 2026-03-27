using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Framework.Library
{
    public static class HttpClientHelper
    {
        public static string HtmlEncode(string inputStr, bool fullEncode = false)
        {
            if (fullEncode == true)
            {
                return inputStr;
            }
            return inputStr.Replace("%", "%25").Replace("<", "%3C").Replace(">", "%3E").Replace(" ", "%20").Replace("#", "%23").Replace("{", "%7B")
                .Replace("}", "%7D").Replace("|", "%7C").Replace("^", "%5E").Replace("~", "%7E").Replace("[", "%5B").Replace("]", "%5D").Replace("'", "%60")
                .Replace("\\", "%5C").Replace("\"", "%22");
        }

        public static HttpResponseWrapper SendHttpRequest(string url, string method, string body = null, WebHeaderCollection headers = null, CookieContainer cookieContainer = null, PostType bodyType = PostType.Json)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new TestProxyException("Invalid URL", new ArgumentNullException("url"));
            }

            if (cookieContainer == null)
            {
                cookieContainer = new CookieContainer();
            }

            HttpClientHandler handler = new HttpClientHandler
            {
                CookieContainer = cookieContainer,
                UseCookies = true
            };

            HttpClient client = new HttpClient(handler);
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(url)
            };

            // Add User-Agent:
            request.Headers.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36"
            );

            // Sanitize and attach headers 
            if (headers != null)
            {
                foreach (string key in headers.AllKeys)
                {
                    string value = headers.Get(key);
                    if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
                        continue;

                    // Remove control characters
                    value = value.Replace("\r", "").Replace("\n", "").Trim();

                    request.Headers.TryAddWithoutValidation(key, value);
                }
            }

            // Determine Http metho and content
            string upperMethod = method.ToUpper();

            switch (upperMethod)
            {
                case "GET":
                case "DELETE":
                    request.Method = new HttpMethod(upperMethod);
                    break;

                case "POST":
                case "PUT":
                case "PATCH":
                    request.Method = new HttpMethod(upperMethod);
                    if (string.IsNullOrEmpty(body) && bodyType != PostType.MultipartFile)
                        break;

                    switch (bodyType)
                    {
                        case PostType.Json:
                            request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                            request.Headers.Accept.ParseAdd("application/json, text/plain */*");
                            break;

                        case PostType.GraphQl:
                            if (string.IsNullOrEmpty(body))
                                throw new TestProxyException("GraphQL request must contain a body");
                            
                            string jsonBody = body
                                .Replace("\r", "")
                                .Replace("\n", " ")
                                .Replace("\t", " ")
                                .Trim();

                            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                            break;
                        case PostType.UrlEncoded:
                            if(string.IsNullOrEmpty(body))
                                throw new TestProxyException($"{upperMethod} request must contain a body");

                            request.Content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
                            request.Headers.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                            break;

                        case PostType.MultipartFile:
                            if (!File.Exists(body))
                                throw new FileNotFoundException("File not found for multipart upload", body);

                            MultipartFormDataContent multipartContent = new MultipartFormDataContent();
                            ByteArrayContent fileContent = new ByteArrayContent(File.ReadAllBytes(body));
                            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                            multipartContent.Add(fileContent, "file", Path.GetFileName(body));
                            request.Content = multipartContent;
                            request.Headers.Accept.ParseAdd("*/*");
                            break;

                        case PostType.None:
                            break;

                        default:
                            throw new TestProxyException($"Unsupported body type: {bodyType}");
                    }
                    break;

                default:
                    throw new TestProxyException("Unsupported Request Method: " + method);
            }

            // Send the request
            try
            {
                HttpResponseMessage response = client.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
                HttpResponseWrapper wrapper = new HttpResponseWrapper(client, response);
                wrapper.RequestUri = request.RequestUri;
                return wrapper;
            }
            catch (HttpRequestException httpEx)
            {
                throw new TestProxyException($"Http request error occured: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                throw new TestProxyException($"An error occurred during the Http request: {ex.Message}", ex);
            }
        }
    }
}
