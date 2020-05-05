using ColorCode.Styling;

namespace Markdig.SyntaxHighlighting
{
    public static class SyntaxHighlightingExtensions
    {
        public static MarkdownPipelineBuilder UseSyntaxHighlighting(this MarkdownPipelineBuilder pipeline, StyleDictionary customCss = null)
        {
            pipeline.Extensions.Add(new SyntaxHighlightingExtension(customCss));
            return pipeline;
        }
    }
}