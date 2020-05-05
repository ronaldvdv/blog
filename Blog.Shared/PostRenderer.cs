using Markdig;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.SyntaxHighlighting;
using System;
using System.IO;

namespace Blog.Shared
{
    public class PostRenderer
    {
        private readonly MarkdownPipeline _pipeline;

        public PostRenderer()
        {
            _pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseMediaLinks(new Markdig.Extensions.MediaLinks.MediaOptions { })
                .UseSyntaxHighlighting()
                .Build();
        }

        public void Render(Post post, TextWriter writer)
        {
            var renderer = new HtmlRenderer(writer);
            renderer.LinkRewriter = RewriteLinks;
            _pipeline.Setup(renderer);
            var document = MarkdownParser.Parse(post.Markdown, _pipeline);
            renderer.Render(document);
        }

        private string RewriteLinks(string arg)
        {
            return arg;
        }
    }
}