using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Blog.Shared
{
    public class PostCollection
    {
        private readonly PostReader _reader;
        private Dictionary<string, Post> _posts = new Dictionary<string, Post>();

        public PostCollection(PostReader reader)
        {
            _reader = reader;
            LoadPath("Posts");
        }

        public IReadOnlyCollection<Post> Posts => _posts.Values;

        public Post Get(string key)
        {
            key = Normalize(key);
            return _posts.TryGetValue(key, out var post) ? post : null;
        }

        public void LoadPath(string path)
        {
            var posts = new DirectoryInfo(path)
                .EnumerateDirectories()
                .Select(dir => _reader.Read(Path.Combine(dir.FullName, "index.md")));
            foreach (var post in posts)
            {
                _posts[Normalize(post.Key)] = post;
            }
        }

        private string Normalize(string key)
        {
            return key.ToLowerInvariant();
        }
    }
}