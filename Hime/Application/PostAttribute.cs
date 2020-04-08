using System;

namespace Hime.Application
{
    public class PostAttribute : Attribute
    {
        public string Route { get; set; }

        public PostAttribute(string route)
        {
            Route = route;
        }
    }
}