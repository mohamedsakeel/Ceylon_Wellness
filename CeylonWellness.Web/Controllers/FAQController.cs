using CeylonWellness.Repositories.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CeylonWellness.Web.Controllers
{
    public class FAQController : Controller
    {
        private readonly HttpClient _httpClient;

        private readonly IFAQRepository _faqrepository;
        public FAQController(IFAQRepository faqrepository)
        {
            _faqrepository = faqrepository;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.openai.com");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer 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");
        }
        public IActionResult Index()
        {
            var faq = new[]
            {
                new { Question = "What is this app about?", Answer = "This app provides dieting meal plan suggestions based on the user's goals." },
                new { Question = "How does the app address the challenge of high-cost and limited availability of nutritious food in Sri Lanka?", Answer = "The app provides users with valuable information about affordable and nutritious food options available locally in Sri Lanka. It offers tips, recipes, and guidance to help users make healthier choices within their budget." },
                new { Question = "What features does the app offer to assist users in health dieting and accessing local food options?", Answer = "The app offers personalized diet plans, nutritional information for local foods, meal suggestions, and grocery shopping assistance. It also includes features for tracking dietary intake, setting health goals, and receiving personalized recommendations." },
                new { Question = "How does the app incorporate personalized parameters and caloric calculations into dietary planning?", Answer = "By considering factors such as age, weight, height, activity level, and health goals, the app calculates personalized caloric and nutritional needs. It tailors diet plans accordingly, ensuring users receive recommendations that align with their individual requirements." },
                new { Question = "Can the app accommodate varied schedules and unique situations for users with different lifestyles?", Answer = "Yes, the app offers flexibility in meal planning to accommodate diverse schedules and lifestyles. Users can customize their diet plans based on their daily routines, preferences, and dietary restrictions, ensuring practicality and adherence to their health goals." },
                new { Question = "What role does domain knowledge, particularly in biochemistry and protein consumption, play in the app's development?", Answer = "Domain knowledge in biochemistry informs the app's understanding of nutritional principles and their impact on health. It helps ensure that the app provides accurate information and recommendations regarding protein consumption, essential nutrients, and balanced dieting." },
                new { Question = "How were insights from interviews with stakeholders integrated into the app's design?", Answer = "Insights from stakeholders, including medical professionals and experts, were carefully considered during the app's development. Their feedback influenced features such as personalized diet plans, educational content, and user-friendly interface design, making the app relevant and beneficial to its users." },
                new { Question = "What makes the app user-friendly and tailored to the Sri Lankan context?", Answer = "The app's user-friendly interface, localized content, and culturally relevant recommendations make it accessible and engaging for users in Sri Lanka. It addresses the specific challenges and preferences of the Sri Lankan population, ensuring that users feel comfortable and empowered to make healthy choices." },
                new { Question = "How does the app educate users about health goals and provide practical solutions for achieving them?", Answer = "Through informative articles, tips, and interactive features, the app educates users about the importance of health goals and provides practical solutions for achieving them. It offers guidance on balanced nutrition, exercise, stress management, and other aspects of holistic health and wellness." },
                new { Question = "What strategies does the app employ to ensure user acceptance and relevance in the Sri Lankan context?", Answer = "The app prioritizes user feedback, continually updating and improving its features based on user preferences and evolving health trends in Sri Lanka. It also collaborates with local experts and organizations to ensure that its content and recommendations remain relevant and impactful to the Sri Lankan community." },
                new { Question = "Can users expect regular updates and improvements to the app based on feedback and evolving health trends in Sri Lanka?", Answer = "Yes, the app is committed to providing regular updates and improvements based on user feedback and emerging health trends in Sri Lanka. Users can expect new features, content updates, and optimizations to enhance their experience and support their health journey effectively." }

            };

            ViewBag.FAQ = faq;
            return View();
        }

        public async Task<ActionResult> Query(string question)
        {
            try
            {
                var answer = await _faqrepository.QueryFAQ(question);
                return Json(new { success = true, answer = answer });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        
    }
}
