using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Blog.Shared
{
    public class PostReader
    {
        private readonly IDeserializer _yamlDeserializer;

        public PostReader()
        {
            _yamlDeserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
        }

        public Post Read(string path)
        {
            Post post;
            var text = File.ReadAllText(path);
            using (var input = new StringReader(text))
            {
                var parser = new Parser(input);
                parser.Consume<StreamStart>();
                parser.Consume<DocumentStart>();
                post = _yamlDeserializer.Deserialize<Post>(parser);
                post.Key = new FileInfo(path).Directory.Name;
                parser.Consume<DocumentEnd>();
                post.Markdown = input.ReadToEnd();
            }
            return post;
        }
    }
}