using System.Collections.Generic;
using System.Collections.Specialized;

namespace Hime.HTTP
{
    public class Headers
    {
        private Dictionary<string, string> _headers;

        public Headers()
        {
            _headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Headers constructor that can accept the headers NameValueCollection from the HttpListener
        /// </summary>
        /// <param name="headers">The header NameValueCollection</param>
        public Headers(NameValueCollection headers)
        {
            _headers = new Dictionary<string, string>();

            foreach (string headerKey in headers)
            {
                _headers.Add(headerKey, headers[headerKey]);
            }
        }

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="header">Header</param>
        /// <param name="value">Value for the header</param>
        public void AddHeader(string header, string value)
        {
            _headers.Add(header, value);
        }

        /// <summary>
        /// Enumerates over every header
        /// </summary>
        /// <returns>A KeyValuePair of strings</returns>
        public IEnumerable<KeyValuePair<string, string>> GetHeaders()
        {
            foreach (KeyValuePair<string, string> header in _headers)
            {
                yield return header;
            }
            yield break;
        }
    }
}
