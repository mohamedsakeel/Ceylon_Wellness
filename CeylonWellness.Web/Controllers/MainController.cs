using Microsoft.AspNetCore.Mvc;

namespace CeylonWellness.Web.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
