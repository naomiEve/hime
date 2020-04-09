using Hime.Enums;
using System.Collections.Generic;

namespace Hime
{
    public static class Constants
    {
        // This maps the method names given back by HttpListener to the method enums
        internal static readonly Dictionary<string, HttpMethods> nameMethodMap = new Dictionary<string, HttpMethods>()
        {
            { "GET",     HttpMethods.Get },
            { "POST",    HttpMethods.Post },
            { "DELETE",  HttpMethods.Delete },
            { "PATCH",   HttpMethods.Patch },
            { "PUT",     HttpMethods.Put },
            { "HEAD",    HttpMethods.Head },
            { "OPTIONS", HttpMethods.Options }
        };

        public const string Version = "1.0.0";
    }
}
