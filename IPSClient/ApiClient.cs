using IPSClient.Objects;
using IPSClient.Objects.System;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IPSClient
{
    public class ApiClient
    {
        public ApiClient(string apiUrl, string apiKey)
        {
            ApiUrl = apiUrl;
            ApiKey = apiKey;
        }

        private string ApiUrl { get; set; }
        private string ApiKey { get; set; }

        private string BuildParameterString(Dictionary<string, string> parameters)
        {
            var output = new StringBuilder();
            foreach (var item in parameters)
            {
                if (output.Length > 0)
                {
                    output.Append("&");
                }
                output.Append(item.Key);
                output.Append("=");
                output.Append(item.Value);
            }
            return output.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of the response</typeparam>
        /// <param name="endpoint"></param>
        /// <param name="verb"></param>
        /// <param name="parameters">The request parameters, or null if there are no parameters</param>
        /// <returns></returns>
        private async Task<T> SendRequest<T>(string endpoint, HttpMethod verb, Dictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }
            using (var client = new HttpClient())
            {
                var requestUrl = ApiUrl.TrimEnd('/') + "/" + endpoint.TrimStart('/');
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(ApiKey)));

                HttpResponseMessage response;

                if (verb == HttpMethod.Get)
                {
                    response = await client.GetAsync(requestUrl + "?" + new StringContent(BuildParameterString(parameters)));
                }
                else if (verb == HttpMethod.Post)
                {
                    response = await client.PostAsync(requestUrl, new StringContent(BuildParameterString(parameters)));
                }
                else if (verb == HttpMethod.Delete)
                {
                    response = await client.DeleteAsync(requestUrl);
                }
                else
                {
                    throw new ArgumentException("Verb is not supported: " + verb.Method + ". Must be GET, POST, or DELETE.", nameof(verb));
                }

                var body = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(body);
                }
                else
                {
                    if (!string.IsNullOrEmpty(body) && body[0] == '{')
                    {
                        throw new ApiException(JsonConvert.DeserializeObject<ExceptionResponse>(body));
                    }
                    else
                    {
                        // Could not read response as error object
                        response.EnsureSuccessStatusCode();
                        throw new Exception($"Api call failed with status code '{response.StatusCode}' and response.EnsureSuccessStatusCode should have thrown.");
                    }                    
                }
            }
        }

        public async Task<HelloResponse> Hello()
        {
            return await SendRequest<HelloResponse>("core/hello", HttpMethod.Get, null);
        }
    }
}
