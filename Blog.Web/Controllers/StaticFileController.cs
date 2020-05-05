using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace Blog.Web.Controllers
{
    public class StaticFileController : Controller
    {
        private readonly IContentTypeProvider _contentTypeProvider;
        private readonly IWebHostEnvironment _environment;

        public StaticFileController(IWebHostEnvironment environment, IContentTypeProvider contentTypeProvider)
        {
            _environment = environment;
            _contentTypeProvider = contentTypeProvider;
        }

        [Route("/Posts/{key}/{file}")]
        public IActionResult Get(string key, string file)
        {
            var path = Path.Combine(_environment.ContentRootPath, "Posts", key, file);
            _contentTypeProvider.TryGetContentType(path, out var type);
            return PhysicalFile(path, type);
        }
    }
}