﻿using IPSClient.Objects;
using IPSClient.Objects.Downloads;
using IPSClient.Objects.Forums;
using IPSClient.Objects.Pages;
using IPSClient.Objects.System;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            foreach (var item in parameterObject?.GetType().GetTypeInfo().DeclaredProperties ?? Enumerable.Empty<PropertyInfo>())
            {
                var t = item.PropertyType;
                var v = item.GetValue(parameterObject);
                if (v != null)
                {
                    if (t == typeof(string) || t == typeof(int?) || t == typeof(int))
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
                    else if (t == typeof(DateTime?))
                    {
                        if ((v as DateTime?).HasValue)
                        {
                            d.Add(item.Name, (v as DateTime?).Value.ToString());
                        }
                    }
                    else if (t == typeof(object))
                    {
                        d.Add(item.Name, JsonConvert.SerializeObject(item.GetValue(parameterObject)));
                    }
                    else
                    {
                        throw new NotSupportedException(string.Format("Unsupported property type: {0} on property {1}", t.Name, item.Name));
                    }
                }
            }
            return d;
        }

        internal MultipartFormDataContent BuildMultipart(object parameterObject)
        {
            var multipart = new MultipartFormDataContent();
            foreach (var item in parameterObject.GetType().GetTypeInfo().DeclaredProperties)
            {
                var t = item.PropertyType;
                var v = item.GetValue(parameterObject);
                if (v != null)
                {
                    if (v is IDictionary<int, string>)
                    {
                        foreach (var subitem in v as IDictionary<int, string>)
                        {
                            multipart.Add(new StringContent(subitem.Value.ToString()), item.Name + $"[{subitem.Key}]");
                        }
                    }
                    else
                    {
                        multipart.Add(new StringContent(v.ToString()), item.Name);
                    }
                }
            }

            return multipart;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="verb"></param>
        /// <param name="parameters">The request parameters, or null if there are no parameters</param>
        /// <param name="type">Type of the response</param>
        /// <returns></returns>
        internal async Task<object> SendRequest(string endpoint, HttpMethod verb, object request, Type type)
        {
            using (var client = new HttpClient())
            {
                var requestUrl = ApiUrl.TrimEnd('/') + "/" + endpoint.TrimStart('/');
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(ApiKey)));

                HttpResponseMessage response;

                if (verb == HttpMethod.Get)
                {
                    response = await client.GetAsync(requestUrl + "?" + BuildParameterString(BuildParameterDictionary(request))).ConfigureAwait(false);
                }
                else if (verb == HttpMethod.Post)
                {
                    response = await client.PostAsync(requestUrl, BuildMultipart(request)).ConfigureAwait(false);                    
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
                            case "USERNAME_EXISTS":
                                throw new UsernameExistsException(exception);
                            case "EMAIL_EXISTS":
                                throw new EmailExistsException(exception);
                            case "INVALID_GROUP":
                                throw new InvalidGroupException(exception);
                            case "NO_USERNAME_OR_EMAIL":
                                throw new NoUsernameOrEmailException(exception);
                            case "NO_PASSWORD":
                                throw new NoPasswordException(exception);
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
        internal async Task<T> SendRequest<T>(string endpoint, HttpMethod verb, object request) where T : class
        {
            return await SendRequest(endpoint, verb, request, typeof(T)) as T;
        }

        #region System

        /// <summary>
        /// Get basic information about the community
        /// </summary>
        public async Task<HelloResponse> Hello()
        {
            return await SendRequest<HelloResponse>("core/hello", HttpMethod.Get, null);
        }

        #region Groups

        /// <summary>
        /// Get list of groups
        /// </summary>
        public PagedResultSet<Group> GetGroups()
        {
            return new PagedResultSet<Group>(this, "core/groups", HttpMethod.Get, new SimplePagedRequest(0));
        }

        /// <summary>
        /// Get information about a specific group
        /// </summary>
        /// <param name="groupID">ID of the group</param>
        /// <exception cref="InvalidIdException">Thrown if the group ID does not exist</exception>
        public async Task<Group> GetGroup(int groupID)
        {
            return await SendRequest<Group>(
                $"core/groups/{groupID.ToString()}",
                HttpMethod.Get,
                null);
        }

        /// <summary>
        /// Deletes a group
        /// </summary>
        /// <param name="groupId">ID of the group</param>
        /// <exception cref="InvalidIdException">Thrown if the group ID does not exist</exception>
        public async Task DeleteGroup(int groupId)
        {
            await SendRequest<Group>(
                $"core/groups/{groupId.ToString()}",
                HttpMethod.Delete,
                null);
        }
        #endregion

        #region Members

        /// <summary>
        /// Get list of members
        /// </summary>
        public PagedResultSet<Member> GetMembers(GetMembersRequest request)
        {
            return new PagedResultSet<Member>(this, "core/members", HttpMethod.Get, request);
        }

        /// <summary>
        /// Get information about a specific member
        /// </summary>
        /// <param name="memberID">ID of the member</param>
        /// <exception cref="InvalidIdException">Thrown if the member ID does not exist</exception>
        public async Task<Member> GetMember(int memberID)
        {
            return await SendRequest<Member>(
                $"core/members/{memberID.ToString()}",
                HttpMethod.Get,
                null);
        }

        /// <summary>
        /// Create a member
        /// </summary>
        /// <param name="request">Request for the call</param>
        /// <exception cref="UsernameExistsException">Thrown when the username provided is already in use</exception>
        /// <exception cref="EmailExistsException">Thrown when the email address provided is already in use</exception>
        /// <exception cref="InvalidGroupException">Thrown when the group ID provided is not valid</exception>
        /// <exception cref="NoUsernameOrEmailException">Thrown when no username or email address was provided for the account</exception>
        /// <exception cref="NoPasswordException">Thrown when no password was provided for the account</exception>
        public async Task<Member> CreateMember(CreateMemberRequest request)
        {
            return await SendRequest<Member>(
                $"core/members",
                HttpMethod.Post,
                request);
        }

        /// <summary>
        /// Edit a member
        /// </summary>
        /// <param name="memberID">ID of the member</param>
        /// <param name="request">Request for the call</param>
        /// <exception cref="InvalidIdException">Thrown when the member ID does not exist</exception>
        /// <exception cref="UsernameExistsException">Thrown when the username provided is already in use</exception>
        /// <exception cref="EmailExistsException">Thrown when the email address provided is already in use</exception>
        public async Task<Member> EditMember(int memberID, CreateMemberRequest request)
        {
            return await SendRequest<Member>(
                $"core/members/{memberID.ToString()}",
                HttpMethod.Post,
                request);
        }

        /// <summary>
        /// Deletes a member
        /// </summary>
        /// <param name="memberID">ID of the member to delete</param>
        /// <exception cref="InvalidIdException">Thrown when the member ID does not exist</exception>
        public async Task DeleteMember(int memberID)
        {
            await SendRequest<object>(
                $"core/members/{memberID.ToString()}",
                HttpMethod.Delete,
                null);
        }

        #endregion

        #endregion

        #region Downloads

        #region Categories

        /// <summary>
        /// Get a list of download categories
        /// </summary>
        public PagedResultSet<Category> GetDownloadCategories()
        {
            return new PagedResultSet<Category>(this, "downloads/category", HttpMethod.Get, new SimplePagedRequest(0));
        }

        /// <summary>
        /// Get a specific category
        /// </summary>
        /// <param name="categoryId">ID of the category</param>
        public async Task<Category> GetDownloadCategory(int categoryId)
        {
            return await SendRequest<Category>(
                $"downloads/category/{categoryId.ToString()}",
                HttpMethod.Get,
                null);
        }

        #endregion

        #region Files
        /// <summary>
        /// Get list of files
        /// </summary>
        public PagedResultSet<File> GetFiles(GetContentItemsRequest request)
        {
            return new PagedResultSet<File>(this, "downloads/files", HttpMethod.Get, request);
        }

        /// <summary>
        /// View information about a specific file
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version">If specified, will show a previous version of a file</param>
        public async Task<File> GetFile(int id, int? version = null)
        {
            return await SendRequest<File>(
                $"downloads/files/{id.ToString()}",
                HttpMethod.Get,
                version.HasValue ? new { version = version.Value.ToString() } : null);
        }

        #endregion

        #endregion

        #region Forums
        public PagedResultSet<Forum> GetForums(GetContentItemsRequest request)
        {
            return new PagedResultSet<Forum>(this, "forums/forums", HttpMethod.Get, request);
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
                request);
        }

        public async Task<GetPageResponse> EditPage(int databaseId, int recordId, CreatePageRequest request)
        {
            return await SendRequest<GetPageResponse>(
                $"cms/records/{databaseId.ToString()}/{recordId.ToString()}",
                HttpMethod.Post,
                request);
        }
        #endregion
    }
}
