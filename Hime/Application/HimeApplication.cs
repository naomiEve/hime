using Hime.Structures;

namespace Hime.Application
{
    public abstract class HimeApplication
    {
        public static ActionResult Ok(string content)
        {
            return new ActionResult
            {
                Code = 200,
                Content = content,
                MIME = "text/html"
            };
        }

        public static ActionResult NoContent()
        {
            return new ActionResult
            {
                Code = 204,
                Content = "",
                MIME = "text/plain"
            };
        }
    }
}
