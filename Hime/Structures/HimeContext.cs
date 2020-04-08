using System.Collections.Specialized;
using System.Net;
using Hime.HTTP;

namespace Hime.Structures
{
    public class HimeContext
    {
        public Headers Headers { get; set; }
        public UriQuery QueryString { get; set; }
        public CookieCollection Cookies { get; set; }
    }
}
