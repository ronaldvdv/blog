using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Blog.Shared
{
    public class PostCollection
    {
        public PostCollection(string path)
        {
            var reader = new PostReader();
            Posts = new DirectoryInfo(path)
                .EnumerateDirectories()
                .Select(dir => reader.Read(Path.Combine(dir.FullName, "index.md")))
                .ToArray();
        }

        public IReadOnlyCollection<Post> Posts { get; }
    }
}