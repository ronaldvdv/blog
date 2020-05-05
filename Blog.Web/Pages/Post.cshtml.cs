using Blog.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace Blog.Web.Pages
{
    public class PostModel : PageModel
    {
        private readonly PostCollection _posts;
        private readonly PostRenderer _renderer;

        public PostModel(PostCollection posts, PostRenderer renderer)
        {
            _posts = posts;
            _renderer = renderer;
        }

        public string Body { get; private set; }

        public string Title { get; private set; }

        public IActionResult OnGet(string key)
        {
            var post = _posts.Get(key);
            if (post == null)
            {
                return NotFound();
            }
            Title = post.Title;
            using (var sw = new StringWriter())
            {
                _renderer.Render(post, sw);
                Body = sw.ToString();
            }
            return Page();
        }
    }
}