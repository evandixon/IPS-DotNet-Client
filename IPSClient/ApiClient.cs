using IPSClient.Objects;
using IPSClient.Objects.Downloads;
using IPSClient.Objects.Forums;
using IPSClient.Objects.Pages;
using IPSClient.Objects.System;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
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

        internal Dictionary<string, string> BuildParameterDictionary(object parameterObject)
        {
            var d = new Dictionary<string, string>();
            foreach (var item in parameterObject.GetType().GetTypeInfo().DeclaredProperties)
            {
                var t = item.PropertyType;
                var v = item.GetValue(parameterObject);
                if (v != null)
                {
                    if (t == typeof(string) || t == typeof(int?))
                    {
                        d.Add(item.Name, item.GetValue(parameterObject).ToString());
                    }
                    else if (t == typeof(bool?))
                    {
                        if ((v as bool?).Value)
                        {
                            // True
                            d.Add(item.Name, (1).ToString());
                        }
                        else
                        {
                            // False
                            d.Add(item.Name, (0).ToString());
                        }
                    }
                    else
                    {
                        throw new NotSupportedException(string.Format("Unsupported property type: {0} on property {1}", t.Name, item.Name));
                    }
                }
            }
            return d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="verb"></param>
        /// <param name="parameters">The request parameters, or null if there are no parameters</param>
        /// <param name="type">Type of the response</param>
        /// <returns></returns>
        internal async Task<object> SendRequest(string endpoint, HttpMethod verb, Dictionary<string, string> parameters, Type type)
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
                    response = await client.GetAsync(requestUrl + "?" + BuildParameterString(parameters)).ConfigureAwait(false);
                }
                else if (verb == HttpMethod.Post)
                {
                    response = await client.PostAsync(requestUrl, new StringContent(BuildParameterString(parameters))).ConfigureAwait(false);
                }
                else if (verb == HttpMethod.Delete)
                {
                    response = await client.DeleteAsync(requestUrl).ConfigureAwait(false);
                }
                else
                {
                    throw new ArgumentException("Verb is not supported: " + verb.Method + ". Must be GET, POST, or DELETE.", nameof(verb));
                }

                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject(body, type);
                }
                else
                {
                    if (!string.IsNullOrEmpty(body) && body[0] == '{')
                    {
                        var exception = JsonConvert.DeserializeObject<ExceptionResponse>(body);
                        switch (exception.errorMessage)
                        {
                            case "INVALID_ID":
                                throw new InvalidIdException(exception);
                            case "INVALID_DATABASE":
                                throw new DatabaseNotFoundException(exception);
                            case "INVALID_VERSION":
                                throw new InvalidVersionException(exception);
                            default:
                                throw new ApiException(exception);
                        }                        
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of the response</typeparam>
        /// <param name="endpoint"></param>
        /// <param name="verb"></param>
        /// <param name="parameters">The request parameters, or null if there are no parameters</param>
        /// <returns></returns>
        internal async Task<T> SendRequest<T>(string endpoint, HttpMethod verb, Dictionary<string, string> parameters) where T : class
        {
            return await SendRequest(endpoint, verb, parameters, typeof(T)) as T;
        }

        public async Task<HelloResponse> Hello()
        {
            return await SendRequest<HelloResponse>("core/hello", HttpMethod.Get, null);
        }

        #region Forums
        public PagedResultSet<Forum> GetForums(GetContentItemsRequest request)
        {
            return new PagedResultSet<Forum>(this, "forums/forums", HttpMethod.Get, request);
        }
        #endregion

        #region Downloads
        public PagedResultSet<GetFileResponse> GetFiles(GetContentItemsRequest request)
        {
            return new PagedResultSet<GetFileResponse>(this, "downloads/files", HttpMethod.Get, request);
        }

        public async Task<GetFileResponse> GetFile(int id, int? version = null)
        {
            return await SendRequest<GetFileResponse>(
                $"downloads/files/{id.ToString()}",
                HttpMethod.Get,
                version.HasValue ? new Dictionary<string, string> { { "version", version.Value.ToString() } } : null);
        }
        #endregion

        #region Pages
        public PagedResultSet<GetPageResponse> GetRecords(int databaseId, GetContentItemsRequest request)
        {
            return new PagedResultSet<GetPageResponse>(this, $"cms/records/{databaseId.ToString()}", HttpMethod.Get, request);
        }

        public async Task<GetPageResponse> GetRecord(int databaseId, int recordId)
        {
            return await SendRequest<GetPageResponse>(
                $"cms/records/{databaseId.ToString()}/{recordId.ToString()}",
                HttpMethod.Get,
                null);
        }

        public async Task<GetPageResponse> CreatePage(int databaseId, CreatePageRequest request)
        {
            return await SendRequest<GetPageResponse>(
                $"cms/records/{databaseId.ToString()}",
                HttpMethod.Post,
                null);
        }

        public async Task<GetPageResponse> EditPage(int databaseId, int recordId, CreatePageRequest request)
        {
            return await SendRequest<GetPageResponse>(
                $"cms/records/{databaseId.ToString()}/{recordId.ToString()}",
                HttpMethod.Post,
                null);
        }
        #endregion
    }
}
