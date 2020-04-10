using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Hime.HTTP
{
    public class PostData
    {
        private readonly string dataType;

        private Dictionary<string, string> urlEncodedMappings;

        /// <summary>
        /// Parse the given application/x-www-form-urlencoded body stream
        /// </summary>
        /// <param name="input">The input stream to parse from</param>
        private void ParseUrlEncoded(Stream input)
        {
            urlEncodedMappings = new Dictionary<string, string>();

            using (StreamReader inputStreamReader = new StreamReader(input))
            {
                string buffer = inputStreamReader.ReadToEnd();

                string[] items = buffer.Split('&');

                foreach (string item in items)
                {
                    string[] keyvalue = item.Split('=');

                    urlEncodedMappings.Add(keyvalue[0], keyvalue[1]);
                }
            }
        }

        /// <summary>
        /// Create a new PostData instance
        /// </summary>
        /// <param name="inputStream">The input data stream</param>
        /// <param name="type">The encoding type of the POST data</param>
        public PostData(Stream inputStream, string type)
        {
            dataType = type;

            switch (type)
            {
                case "application/x-www-form-urlencoded":
                    ParseUrlEncoded(inputStream);
                    break;

                case "multipart/form-data":
                    throw new Exception("WIP");

                default:
                    throw new ArgumentException($"{type} isn't a valid form data type");
            }
        }

        /// <summary>
        /// Gets a value from the PostData store
        /// </summary>
        /// <param name="key">The value key</param>
        /// <returns>The requested value</returns>
        public string Get(string key)
        {
            switch (dataType)
            {
                case "application/x-www-form-urlencoded":
                    return urlEncodedMappings[key];

                case "multipart/form-data":
                    return "";

                default:
                    throw new Exception("The PostData type is not supported for some reason.");
            }
        }
    }
}
