using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sanity.Linq.CommonTypes;

namespace CeylonWellness.Domain.Models
{
    public class Article : SanityDocument
    {
        public Article()
        {
        }

        [JsonProperty("_id")]
        public string CategoryId { get; set; }
        [JsonProperty("_type")]
        public string DocumentType => "article";
        public string Title { get; set; }
        public SanitySlug Slug { get; set; }
        public string Subtitle { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public object[] Content { get; set; }
    }
   

    public class ContentBlock
    {
        public string _type { get; set; } // To identify the type of content block (e.g., block, image, quiz)
        public string Style { get; set; }
        public List<Span> Children { get; set; } // Children is a list of Span objects
        public SanityImage[] Image { get; set; }
        public Quiz Quiz { get; set; }
    }

    public class Span
    {
        public string _type { get; set; } // Typically "span"
        public string Text { get; set; }
        public List<string> Marks { get; set; }
    }

    public class Quiz
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int? CorrectAnswer { get; set; }
    }

    public class ArticleSummaryModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string slug { get; set; } // Assuming each article has a unique ID
    }

    public class ArticleViewModel
    {
        public List<ArticleSummaryModel> Articles { get; set; }
        public List<ArticleSummaryModel> SleepProblem { get; set; }
        public List<ArticleSummaryModel> LackMotivation { get; set; }
        public List<ArticleSummaryModel> EmotionalEating { get; set; }

    }
}
