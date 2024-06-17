using Microsoft.AspNetCore.Mvc;

namespace CeylonWellness.Web.Controllers
{
    public class MealController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
