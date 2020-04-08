using System.Collections.Generic;
using System.Collections.Specialized;

namespace Hime.HTTP
{
    public struct UriQuery
    {
        private readonly Dictionary<string, string> uriQueries;

        public UriQuery(NameValueCollection queryCol)
        {
            uriQueries = new Dictionary<string, string>();

            foreach (string queryKey in queryCol)
            {
                uriQueries.Add(queryKey, queryCol[queryKey]);
            }
        }

        /// <summary>
        /// Get a URI query
        /// </summary>
        /// <param name="queryString">The requested query string</param>
        /// <returns>Value associated with the query string</returns>
        public string Get(string queryString)
        {
            return uriQueries[queryString];
        }

        /// <summary>
        /// Checks if there's a URI query with that name
        /// </summary>
        /// <param name="queryString">The requested query string</param>
        /// <returns>True or false</returns>
        public bool Contains(string queryString)
        {
            return uriQueries.ContainsKey(queryString);
        }
    }
}
