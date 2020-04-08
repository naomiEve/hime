using Hime.Enums;
using Hime.Structures;
using System;
using System.Collections.Generic;

namespace Hime.Routing
{
    public delegate ActionResult RoutingDelegate(HimeContext ctx);

    internal class Bucket : Dictionary<string, RoutingDelegate> { }

    public class Buckets
    {
        private Dictionary<HttpMethods, Bucket> _buckets = new Dictionary<HttpMethods, Bucket>();

        /// <summary>
        /// Creates a new routing bucket for the specified method
        /// </summary>
        /// <param name="method">The specified HTTP method</param>
        public void CreateBucketFor(HttpMethods method)
        {
            if (!_buckets.ContainsKey(method))
            {
                Bucket bucket = new Bucket();
                _buckets.Add(method, bucket);
            }
            else
            {
                throw new Exception("Bucket already present");
            }
        }

        /// <summary>
        /// Checks whether a bucker for a specified method exists
        /// </summary>
        /// <param name="method">Specified method</param>
        /// <returns>Whether a bucket like this exists</returns>
        public bool ContainsBucket(HttpMethods method) => _buckets.ContainsKey(method);

        /// <summary>
        /// Tries to get a specific routing delegate from a bucket
        /// </summary>
        /// <param name="method">HTTP method for the bucket</param>
        /// <param name="route">Specified route</param>
        /// <param name="routingDelegate">Returned delegate</param>
        /// <returns>Returns true on found delegate and false on missing</returns>
        public bool TryGetFromBucket(HttpMethods method, string route, out RoutingDelegate routingDelegate)
        {
            Bucket bucket = _buckets[method];

            if (bucket.TryGetValue(route, out routingDelegate))
            {
                return true;
            }
            else
            {
                routingDelegate = null;
                return false;
            }
        }

        /// <summary>
        /// Adds a route / delegate pair to a bucket.
        /// </summary>
        /// <param name="bucket">Specified bucket</param>
        /// <param name="value">The route/delegate value pair</param>
        public void AddToBucket(HttpMethods bucket, KeyValuePair<string, RoutingDelegate> value)
        {
            AddToBucket(bucket, value, false);
        }

        /// <summary>
        /// Adds a route / delegate pair to a bucket. Creates the bucket if createBucket is true.
        /// </summary>
        /// <param name="bucket"Specified bucket></param>
        /// <param name="value">The route/delegate value pair</param>
        /// <param name="createBucket">Should the function create the bucket if it doesn't exist</param>
        public void AddToBucket(HttpMethods bucket, KeyValuePair<string, RoutingDelegate> value, bool createBucket)
        {
            if (createBucket && !_buckets.ContainsKey(bucket))
            {
                CreateBucketFor(bucket);
            }

            Bucket routingBucket = _buckets[bucket];
            routingBucket.Add(value.Key, value.Value);
        }
    }
}
