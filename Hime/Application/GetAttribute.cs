using System;

namespace Hime.Application
{
    public class GetAttribute : Attribute
    {
        public string Route { get; set; }

        public GetAttribute(string route)
        {
            Route = route;
        }
    }
}
