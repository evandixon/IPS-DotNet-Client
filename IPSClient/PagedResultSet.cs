using IPSClient.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IPSClient
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="R">The type of the response</typeparam>
    /// <typeparam name="T">The type of each item in the response</typeparam>
    public class PagedResultSet<T> : ICollection, IEnumerable<T>, IEnumerator<T>
        where T : class
    {
        public PagedResultSet(ApiClient client, string endpoint, HttpMethod verb, IPagedRequest request, IPagedResultResponse<T> response)
        {
            _client = client;
            _endpoint = endpoint;
            _verb = verb;
            _request = request;
            _pageSize = response.perPage;
            _totalSize = response.totalResults;
            _totalPages = response.totalPages;
            _responseType = response.GetType();

            CurrentIndex = (response.page - 1) * response.perPage;

            // Add results to proper index
            Results = new List<T>(CurrentIndex + response.results.Count);
            for (int i = 0; i < CurrentIndex; i++)
            {
                Results.Add(null);
            }
            Results.AddRange(response.results);
        }

        private ApiClient _client;
        private string _endpoint;
        private HttpMethod _verb;
        private IPagedRequest _request;
        private Type _responseType;
        private int _pageSize;
        private int _totalSize;
        private int _totalPages;

        private int CurrentIndex { get; set; }
        private List<T> Results { get; set; }

        private void RequestPage(int pageIndex)
        {
            _request.page = pageIndex + 1;
            var response = _client.SendRequest(_endpoint, _verb, _client.BuildParameterDictionary(_request), _responseType).Result as IPagedResultResponse<T>;
            if (response.totalResults != _totalSize)
            {
                throw new InvalidOperationException("The collection was modified after the enumerator was created.");
            }

            int start = (response.page - 1) * response.perPage;
            for (int i = 0; i < response.results.Count; i++)
            {
                if (Results.Count > i + start)
                {
                    Results[i + start] = response.results[i];
                }
                else
                {
                    // Add filler entries as needed
                    for (int j = Results.Count; j < i; j++)
                    {
                        Results.Add(null);
                    }

                    // Add the current item
                    Results.Add(response.results[i]);
                }
            }
        }

        #region IEnumerable Support
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
        #endregion

        #region IEnumerator Support
        public T Current => Results[CurrentIndex];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            // Do nothing
        }

        public bool MoveNext()
        {
            if (CurrentIndex + 1 < _totalSize)
            {
                // Advance position
                CurrentIndex += 1;

                // Request additional data if necessary
                if (Results.Count <= CurrentIndex || Current == null)
                {
                    RequestPage((int)Math.Floor((double)CurrentIndex / _pageSize));
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            CurrentIndex = -1;
        }
        #endregion

        #region ICollection Support
        public int Count => _totalSize;

        public bool IsSynchronized => false;

        public object SyncRoot => new object();

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if (array.Rank > 1)
            {
                throw new ArgumentException("Array should not be multidimensional");
            }

            var current = CurrentIndex;
            CurrentIndex = 0;
            for (int i = index;i<_totalSize;i++)
            {
                array.SetValue(Current, i);
                if (!MoveNext())
                {
                    break;
                }
            }
            CurrentIndex = current;
        }
        #endregion

    }
}
