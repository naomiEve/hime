using System;
using System.IO;
using Hime.Structures;

namespace Hime.HTTP
{
    public class StaticFiles
    {
        private readonly string pathPrefix;

        /// <summary>
        /// Constructor that sets the static file path to the desired path
        /// </summary>
        /// <param name="path">The desired path</param>
        public StaticFiles(string path)
        {
            pathPrefix = Environment.CurrentDirectory + path;
        }

        /// <summary>
        /// Gets the desired static file
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>The ActionResult containing the file response</returns>
        public ActionResult GetFile(string path)
        {
            string fileExtension = Path.GetExtension(path);

            // Checks whether the MIME mapper has the desired file extension,
            // otherwise it falls back to the default "application/octet-stream"
            string mime = Constants.mimeMappings.ContainsKey(fileExtension) ?
                          Constants.mimeMappings[fileExtension] :
                          "application/octet-stream";

            byte[] buffer;

            buffer = File.ReadAllBytes(pathPrefix + path);

            return new ActionResult
            {
                Code = 200,
                Content = buffer,
                MIME = mime
            };
        }

        /// <summary>
        /// Checks whether a static file exists
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>True or false</returns>
        public bool Exists(string path)
        {
            return File.Exists(pathPrefix + path);
        }
    }
}
