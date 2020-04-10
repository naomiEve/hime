using System;
using System.Collections.Generic;
using System.Reflection;
using Hime.Application;
using Hime.Enums;

namespace Hime.Routing
{
    public class Router
    {
        private const string notFoundBody = "<h1>Not Found.</h1>";
        private readonly Buckets routingBuckets = new Buckets();

        internal RoutingDelegate CreateRoutingDelegate(MethodInfo methodInfo, HimeApplication app)
        {
            return (RoutingDelegate)Delegate.CreateDelegate(typeof(RoutingDelegate), app, methodInfo);
        }

        internal void WalkApplication(HimeApplication app)
        {
            Type applicationType = app.GetType();

            MethodInfo[] methods = applicationType.GetMethods();

            foreach (MethodInfo appMethod in methods)
            {
                object[] propertyAttributes = appMethod.GetCustomAttributes(false);

                foreach (object attribute in propertyAttributes)
                {
                    switch (attribute)
                    {
                        case GetAttribute getAttrib:
                            Console.WriteLine($"GET => {getAttrib.Route}");
                            routingBuckets.AddToBucket(HttpMethods.Get, new KeyValuePair<string, RoutingDelegate> (getAttrib.Route, CreateRoutingDelegate(appMethod, app)), true);
                            break;

                        case PostAttribute postAttrib:
                            Console.WriteLine($"POST => {postAttrib.Route}");
                            routingBuckets.AddToBucket(HttpMethods.Post, new KeyValuePair<string, RoutingDelegate> (postAttrib.Route, CreateRoutingDelegate(appMethod, app)), true);
                            break;

                        default:
                            continue;
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new Hime Router, walking the specified Hime Application gathering all the route attributes
        /// </summary>
        /// <param name="baseApp">The Hime Application class you want to walk</param>
        public Router(HimeApplication baseApp)
        {
            WalkApplication(baseApp);
        }

        /// <summary>
        /// Returns a delegate for a given route and method
        /// </summary>
        /// <param name="route">The route you want to get</param>
        /// <param name="method">The HTTP method you wish for</param>
        /// <returns>Returns the delegate handling the specified route under the specified method</returns>
        public RoutingDelegate GetRouteFor(string route, HttpMethods method)
        {
            if (routingBuckets.TryGetFromBucket(method, route, out RoutingDelegate routingDelegate))
            {
                return routingDelegate;
            }

            return new RoutingDelegate(ctx => HimeApplication.NotFound(notFoundBody));
        }
    }
}
