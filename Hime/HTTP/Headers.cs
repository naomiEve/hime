using System.Collections.Generic;

namespace Hime.HTTP
{
    public class Headers
    {
        private Dictionary<string, string> _headers = new Dictionary<string, string>();

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
