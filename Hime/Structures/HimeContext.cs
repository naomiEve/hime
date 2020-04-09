using System.Collections.Specialized;
using System.Net;
using Hime.HTTP;

namespace Hime.Structures
{
    public class HimeContext
    {
        public Headers RequestHeaders { get; set; }
        public Headers ResponseHeaders { get; set; }
        public UriQuery QueryString { get; set; }
        public CookieCollection Cookies { get; set; }
        public string[] AcceptTypes { get; set; }
        public IPEndPoint RemoteAddress { get; set; }
    }
}
