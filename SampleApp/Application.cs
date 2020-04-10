using Hime.Application;
using Hime.Structures;

namespace SampleApp
{
    public class Application : HimeApplication
    {
        [Get("/")]
        public ActionResult Index(HimeContext ctx)
        {
            return Ok("<img src=\"hime.png\"><br><h1>Hime</h1>");
        }

        [Get("/helloworld")]
        public ActionResult HelloWorld(HimeContext ctx)
        {
            ctx.ResponseHeaders.AddHeader("X-HelloWorld", "Hi!");
            return Ok("<form action=\"/helloworld\" method=POST><input type=\"text\" name=\"name\"><input type=\"submit\"></form>");
        }

        [Get("/hello")]
        public ActionResult Hello(HimeContext ctx)
        {
            if (ctx.QueryString.Contains("name"))
                return Ok($"Hello {ctx.QueryString.Get("name")}!");

            return Ok("I can't greet you without knowing who you are!");
        }

        [Get("/nothing")]
        public ActionResult Nothing(HimeContext ctx)
        {
            return NoContent();
        }

        [Get("/redirect")]
        public ActionResult Redirection(HimeContext ctx)
        {
            return Redirect("/", ctx);
        }

        [Get("/headers")]
        public ActionResult HeadersTest(HimeContext ctx)
        {
            string tmp = "";
            foreach (System.Collections.Generic.KeyValuePair<string, string> header in ctx.RequestHeaders.GetHeaders())
            {
                tmp += $"<b>{header.Key}</b> - {header.Value}<br>";
            }

            return Ok(tmp);
        }

        [Post("/helloworld")]
        public ActionResult HelloWorldPost(HimeContext ctx)
        {
            return Ok($"Hello {ctx.PostData.Get("name")}");
        }
    }
}
