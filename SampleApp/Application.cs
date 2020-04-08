using Hime.Application;
using Hime.Structures;

namespace SampleApp
{
    public class Application : HimeApplication
    {
        [Get("/")]
        public ActionResult Index(HimeContext ctx)
        {
            return Ok("<h1>Hello!</h1>");
        }

        [Get("/helloworld")]
        public ActionResult HelloWorld(HimeContext ctx)
        {
            ctx.Headers.AddHeader("X-HelloWorld", "Hi!");
            return Ok("<form action=\"/helloworld\" method=POST><input type=\"submit\"></form>");
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

        [Post("/helloworld")]
        public ActionResult HelloWorldPost(HimeContext ctx)
        {
            return Ok("You've posted!");
        }
    }
}
