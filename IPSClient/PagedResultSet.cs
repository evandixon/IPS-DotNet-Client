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
    /// <typeparam name="T">The type of each item in the response</typeparam>
    public class PagedResultSet<T> : ICollection, IEnumerable<T>, IEnumerator<T>
        where T : class
    {
        public PagedResultSet(ApiClient client, string endpoint, HttpMethod verb, IPagedRequest request)
        {
            _client = client;
            _endpoint = endpoint;
            _verb = verb;
            _request = request;
            CurrentIndex = -1;
        }

        private ApiClient _client;
        private string _endpoint;
        private HttpMethod _verb;
        private IPagedRequest _request;
        private int? _pageSize;
        private int? _totalSize;
        private int? _totalPages;

        private int CurrentIndex { get; set; }
        private List<T> Results { get; set; }

        private int PageSize
        {
            get
            {
                if (_pageSize.HasValue)
                {
                    return _pageSize.Value;
                }
                else
                {
                    RequestPage(0);
                    return _pageSize.Value;
                }
            }
        }

        private void RequestPage(int? pageIndex)
        {
            if (pageIndex.HasValue)
            {
                _request.page = pageIndex + 1;
            }          
            else
            {
                _request.page = null;
            }
            var response = _client.SendRequest(_endpoint, _verb, _request, typeof(PagedResponse<T>)).Result as PagedResponse<T>;

            // Check to see if this is the first request
            if (!_pageSize.HasValue)
            {
                _pageSize = response.perPage;
                _totalSize = response.totalResults;
                _totalPages = response.totalPages;

                CurrentIndex = (response.page - 1) * response.perPage;
                Results = new List<T>(CurrentIndex + response.results.Count);
            }

            // Check to see if the enumeration changed
            if (response.totalResults != _totalSize)
            {
                throw new InvalidOperationException("The collection was modified after the enumerator was created.");
            }

            // Store the results
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
            Reset();
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region IEnumerator Support
        public T Current
        {
            get
            {
                // Request additional data if necessary
                if (Results.Count <= CurrentIndex || Results[CurrentIndex] == null)
                {
                    RequestPage((int)Math.Floor((double)CurrentIndex / PageSize));
                }

                // Return the current item
                return Results[CurrentIndex];
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            // Do nothing
        }

        public bool MoveNext()
        {
            CurrentIndex += 1;
            return (CurrentIndex < Count && Current != null);
        }

        public void Reset()
        {
            CurrentIndex = -1;
        }
        #endregion

        #region ICollection Support
        public int Count
        {
            get
            {
                if (_totalSize.HasValue)
                {
                    return _totalSize.Value;
                }
                else
                {
                    RequestPage(0);
                    return _totalSize.Value;
                }
            }
        }


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
            for (int i = index; i < _totalSize; i++)
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
