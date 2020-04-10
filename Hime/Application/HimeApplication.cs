using System.Text;
using Hime.Structures;

namespace Hime.Application
{
    public abstract class HimeApplication
    {
        /// <summary>
        /// Returns a 200 OK response with the specified string. Auto converts from a string to a UTF-8 byte sequence.
        /// </summary>
        /// <param name="content">The body of the response</param>
        /// <returns>The response ActionResult</returns>
        public static ActionResult Ok(string content)
        {
            var bytes = Encoding.UTF8.GetBytes(content);

            return new ActionResult
            {
                Code = 200,
                Content = bytes,
                MIME = "text/html"
            };
        }

        /// <summary>
        /// Returns a 204 No Content response.
        /// </summary>
        /// <returns>The response ActionResult</returns>
        public static ActionResult NoContent()
        {
            return new ActionResult
            {
                Code = 204,
                Content = new byte[] { },
                MIME = "text/plain"
            };
        }

        /// <summary>
        /// Returns a 404 Not Found response. Auto converts from a string to a UTF-8 byte sequence.
        /// </summary>
        /// <param name="content">The body of the response</param>
        /// <returns>The response ActionResult</returns>
        public static ActionResult NotFound(string content)
        {
            var bytes = Encoding.UTF8.GetBytes(content);

            return new ActionResult
            {
                Code = 404,
                Content = bytes,
                MIME = "text/html"
            };
        }

        /// <summary>
        /// Returns a 303 See Other response. Also attaches the appropriate Location header for redirection.
        /// </summary>
        /// <param name="url">The URL to redirect to</param>
        /// <param name="ctx">The HimeContext passed to the route</param>
        /// <returns>The response ActionResult</returns>
        public static ActionResult Redirect(string url, HimeContext ctx)
        {
            ctx.ResponseHeaders.AddHeader("Location", url);

            return new ActionResult
            {
                Code = 303,
                Content = Encoding.UTF8.GetBytes($"<a href=\"{url}\">Redirecting</a>"),
                MIME = "text/html"
            };
        }
    }
}
