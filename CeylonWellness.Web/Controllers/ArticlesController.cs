using CeylonWellness.Domain.Models;
using CeylonWellness.Web.Extensions;
using CeylonWellness.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sanity.Linq;
using Sanity.Linq.BlockContent;

namespace CeylonWellness.Web.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly SanityDataContext _Sanitycontext;
        private readonly SanityService _sanityService;

        public List<Article> PublishedArticles { get; set; }
        public List<string> ArticleHtmlContents { get; set; }

        public ArticlesController(SanityService sanityservice)
        {
            _Sanitycontext = sanityservice.GetContext();
            _sanityService = sanityservice;
        }
        public async Task<IActionResult> Index()
        {
            var publishedarticles = await _Sanitycontext.DocumentSet<Article>().ToListAsync();

            var options = new SanityOptions
            {
                ProjectId = "s3ly8pgl",
                Dataset = "production",
                Token = "skRfl9c8zxRWbCPqgX7fucHvoFqj44yaEWb5N7X4zeV12LN0p3Gm8VWJIKUyJgqRogH2XY3VPwfw0N4VRO5PYQZvDpXQ1VFxrOSvVYMspNgPCf4YtmIcWKTuy71HVPWC7SS632WS7g8ywUtBD2l84e2Z0CstZ1HYTfL98iT9rzG5XBosSotJ",
                UseCdn = false,
                ApiVersion = "v2022-03-07",

            };

            var htmlBuilder = new SanityHtmlBuilder(options);

            var articlesummary = publishedarticles.Select(article => new ArticleSummaryModel
            {
                slug = article.Slug.Current,
                Title = article.Title,
                Subtitle = article.Subtitle,
            }).ToList();

            htmlBuilder.AddSerializer("quiz", _sanityService.QuizSerializeAsync);

            ArticleHtmlContents = new List<string>();
            foreach (var article in publishedarticles)
            {
                var htmlContent = await htmlBuilder.BuildAsync(article.Content);
                ArticleHtmlContents.Add(htmlContent);
            }

            var viewModel = new ArticleViewModel
            {
                Articles = articlesummary
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(string slug)
        {
            var article = await _Sanitycontext.DocumentSet<Article>().Where(a => a.Slug.Current == slug).FirstOrDefaultAsync();

            
            if (article == null)
            {
                return NotFound();
            }

            var options = new SanityOptions
            {
                ProjectId = "s3ly8pgl",
                Dataset = "production",
                Token = "skRfl9c8zxRWbCPqgX7fucHvoFqj44yaEWb5N7X4zeV12LN0p3Gm8VWJIKUyJgqRogH2XY3VPwfw0N4VRO5PYQZvDpXQ1VFxrOSvVYMspNgPCf4YtmIcWKTuy71HVPWC7SS632WS7g8ywUtBD2l84e2Z0CstZ1HYTfL98iT9rzG5XBosSotJ",
                UseCdn = false,
                ApiVersion = "v2022-03-07"
            };

            var htmlBuilder = new SanityHtmlBuilder(options);
            htmlBuilder.AddSerializer("quiz", _sanityService.QuizSerializeAsync);

            var htmlContent = await htmlBuilder.BuildAsync(article.Content);

            return View("Details", htmlContent);
        }
    }
}
