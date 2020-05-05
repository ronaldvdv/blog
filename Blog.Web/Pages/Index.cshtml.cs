using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using YamlDotNet.Core.Tokens;

namespace Blog.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PostCollection _posts;

        public IndexModel(ILogger<IndexModel> logger, PostCollection posts)
        {
            _logger = logger;
            _posts = posts;
        }

        public IReadOnlyCollection<PostViewModel> Posts { get; private set; }

        public void OnGet()
        {
            Posts = _posts.Posts.Select(p => new PostViewModel(p)).ToArray();
        }

        public class PostViewModel
        {
            public PostViewModel(Post post)
            {
                Title = post.Title;
                Key = post.Key;
            }

            public object Key { get; }
            public string Title { get; }
        }
    }
}