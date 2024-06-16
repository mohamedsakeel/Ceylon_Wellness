using CeylonWellness.Web.Data;
using CeylonWellness.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace CeylonWellness.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbcontext;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbcontext)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _dbcontext = dbcontext;
        }

        public async Task<IActionResult> Index()
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;

            if (username != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var userhealthinfo = await (from uh in _dbcontext.userHealthInfos
                                            join diat in _dbcontext.DietPlans on uh.UserId equals diat.UserId
                                            join goal in _dbcontext.Goals on diat.GoalId equals goal.Id

                                            where uh.UserId == userId
                                            select new 
                                            {
                                                GoalName = goal.GoalName,
                                                TargetWeight = uh.TargetWeight,
                                                WeeklyGoal = diat.PlanType,
                                                Calorie = uh.MaintaincalAmount

                                            }
                                            ).ToListAsync();

                

                HomeViewModel VM = new HomeViewModel();

                VM.GoalName = userhealthinfo.First().GoalName;
                VM.TargetWeight = userhealthinfo.First().TargetWeight;
                VM.Calorie = userhealthinfo.First().Calorie;
                VM.WeeklyGoal = userhealthinfo.First().WeeklyGoal;

                if (userhealthinfo.First().WeeklyGoal == "Relaxed")
                {
                    VM.goaldetail = "0.25 Kg (0.5 lb) per week";
                }
                else if (userhealthinfo.First().WeeklyGoal == "Normal")
                {
                    VM.goaldetail = "0.5 Kg (1.1 lb) per week";
                }
                else if (userhealthinfo.First().WeeklyGoal == "Strict")
                {
                    VM.goaldetail = "1 Kg (2.2 lb) per week";
                }


                    return View(VM);
            }
            else
            {
                // Handle the case where the user is not authenticated
                return RedirectToAction("Login");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
