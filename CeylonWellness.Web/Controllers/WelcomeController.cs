using Microsoft.AspNetCore.Mvc;

namespace CeylonWellness.Web.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
