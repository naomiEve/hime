using System;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Hime.Structures;
using Hime.Application;
using Hime.Routing;
using Hime.Enums;
using Hime.HTTP;


namespace Hime
{
    public static class HimeRunner
    {
        internal static Dictionary<string, HttpMethods> nameMethodMap = new Dictionary<string, HttpMethods>()
        {
            { "GET", HttpMethods.Get },
            { "POST", HttpMethods.Post },
            { "DELETE", HttpMethods.Delete },
            { "PATCH", HttpMethods.Patch },
            { "PUT", HttpMethods.Put },
            { "HEAD", HttpMethods.Head },
            { "OPTIONS", HttpMethods.Options }
        };

        internal static bool _running = false;

        internal static Router _router;

        internal static HttpListener _httpListener;

        internal static ServerConfig _cfg = new ServerConfig
        {
            IP = IPAddress.Loopback,
            Port = 12009
        };

        /// <summary>
        /// Sets the Hime Runner settings
        /// </summary>
        /// <param name="cfg">The new settings you want to apply</param>
        public static void Setup(ServerConfig cfg) => _cfg = cfg;

        /// <summary>
        /// Runs the Hime Application.
        /// </summary>
        /// <param name="app">An instance of the application you want to run.</param>
        public static void Run(HimeApplication app)
        {
            Console.WriteLine($"Hime {Constants.Version} taking off.");
            Console.WriteLine("Building router...");

            _router = new Router(app);

            Console.WriteLine("Route builder complete.");
            Console.WriteLine("Setting up the server.");

            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add($"http://{_cfg.IP}:{_cfg.Port}/");

            Console.WriteLine($"Starting Hime server at {_cfg.IP}:{_cfg.Port}");

            _httpListener.Start();
            _running = true;

            HandleIncomingRequests();

        }

        internal static void HandleIncomingRequests()
        {
            while (_running)
            {
                var ctx = _httpListener.BeginGetContext(new AsyncCallback(RequestCallback), _httpListener);
                ctx.AsyncWaitHandle.WaitOne();
            }
        }

        internal static void RequestCallback(IAsyncResult res)
        {
            HttpListener        listener = (HttpListener)res.AsyncState;
            HttpListenerContext context  = listener.EndGetContext(res);

            HttpListenerRequest  req  = context.Request;
            HttpListenerResponse resp = context.Response;
            
            HimeContext ctx = new HimeContext
            {
                Headers = new Headers(),
                QueryString = new UriQuery(req.QueryString),
                Cookies = req.Cookies
            };

            HttpMethods requestedMethod = nameMethodMap[req.HttpMethod];

            string requestedUrl = req.RawUrl;

            if (requestedUrl.Contains("?"))
            {
                string[] tmp = requestedUrl.Split('?');
                requestedUrl = tmp[0];
            }

            RoutingDelegate route = _router.GetRouteFor(requestedUrl, requestedMethod);
            ActionResult routeResult = route(ctx);

            resp.StatusCode = routeResult.Code;
            resp.ContentType = routeResult.MIME;

            foreach (KeyValuePair<string, string> header in ctx.Headers.GetHeaders())
            {
                resp.AddHeader(header.Key, header.Value);
            }

            var buffer = Encoding.UTF8.GetBytes(routeResult.Content);

            resp.ContentLength64 = buffer.Length;
            resp.OutputStream.Write(buffer, 0, buffer.Length);

            Console.WriteLine($"[{DateTime.Now}] {req.HttpMethod} \"{req.RawUrl}\" - {routeResult.Code}");

            resp.OutputStream.Close();
            resp.Close();
        }
    }
}
