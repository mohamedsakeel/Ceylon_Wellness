using CeylonWellness.Domain.Models;
using Sanity.Linq;

namespace CeylonWellness.Web.Services
{
    public class ArticleService
    {
        private readonly SanityDataContext _Sanitycontext;

        public ArticleService(SanityDataContext sanityContext)
        {
            _Sanitycontext = sanityContext;
        }

        public async Task<List<Article>> GetPublishedArticlesAsync()
        {
            var articles = await _Sanitycontext.DocumentSet<Article>().Where(p => p.CategoryId.StartsWith("drafts.")).ToListAsync();

            return articles;
        }
    }
}
