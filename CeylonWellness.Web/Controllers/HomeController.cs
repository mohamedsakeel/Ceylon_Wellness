using CeylonWellness.Domain.Models;
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

        public IActionResult Details(string meal)
        {
            var meals = new List<Meals>()
            {
                // Create Meal objects with details for each meal
                new Meals
                  {
                    MealName = "Whey Protein Smoothie with Banana and Peanut Butter",
                    Ingredients = new List<string>() {"1 scoop whey protein powder", "1 large banana", "2 tablespoons peanut butter", "1 cup whole milk", "1 tablespoon honey"},
                    Preparation = "Add all ingredients to a blender. Blend until smooth. Serve immediately.",
                    NutritionFacts = "Protein: 24g, Fat: 20.2g, Carbs: 55.5g",
                    MealType = "Snack 1"
                  },
                  // Add Breakfast meal
                  new Meals
                  {
                    MealName = "Egg Hoppers with Coconut Sambol and Dhal Curry",
                    Ingredients = new List<string>() {"2 egg hoppers", "1/2 cup coconut sambol", "1/2 cup dhal curry"},
                    Preparation = "Prepare egg hoppers using a hopper pan. For coconut sambol, mix grated coconut, chopped onions, green chilies, lime juice, and salt. For dhal curry, cook red lentils with turmeric, onions, tomatoes, and coconut milk. Serve egg hoppers with coconut sambol and dhal curry.",
                    NutritionFacts = "Protein: 24g, Fat: 16.7g, Carbs: 63.5g",
                    MealType = "Breakfast"
                  },
                  // Add Lunch meal
                  new Meals
                  {
                    MealName = "Red Nadu Rice with Chicken Curry, Leeks Curry, Carrot Curry, and Gotukola Mallum",
                    Ingredients = new List<string>() {"1 cup cooked red nadu rice", "200g chicken breast (for chicken curry)", "1 cup leeks curry", "1 cup carrot curry", "1/2 cup gotukola mallum"},
                    Preparation = "Cook red nadu rice as per instructions. For chicken curry, cook chicken with spices, coconut milk, and curry leaves. For leeks curry, sauté leeks with onions, mustard seeds, and turmeric. For carrot curry, cook carrots with onions, green chilies, and coconut milk. For gotukola mallum, mix chopped gotukola with grated coconut, onions, and lime juice. Serve all together.",
                    NutritionFacts = "Protein: 24g, Fat: 25g, Carbs: 107.2g",
                    MealType = "Lunch"
                  },
                  // Add Dinner meal
                  new Meals
                  {
                    MealName = "Baked Fish with Brown Rice, Green Beans, and Pumpkin Curry",
                    Ingredients = new List<string>() {"200g fish fillet (e.g., mackerel)", "1 cup cooked brown rice", "1 cup steamed green beans", "1 cup pumpkin curry"},
                    Preparation = "Season fish fillet with salt, pepper, and lemon juice, then bake at 375°F (190°C) for 20-25 minutes. Cook brown rice as per instructions. Steam green beans until tender. For pumpkin curry, cook pumpkin with onions, green chilies, and coconut milk. Serve baked fish with brown rice, green beans, and pumpkin curry.",
                    NutritionFacts = "Protein: 24g, Fat: 25g, Carbs: 107.2g",
                    MealType = "Dinner"
                  }
                // ... Add similar entries for other meals
            };

            var mealCardViewModels = meals.Select(meal => new MealCardViewModel
            {
                MealName = meal.MealName,
                // No image URL for hardcoded data (optional)
                MealType = meal.MealType, 
                Ingredients = meal.Ingredients, // Join for HTML display
                Preparation = meal.Preparation,
                NutritionFacts = meal.NutritionFacts
                
            });

            return View(mealCardViewModels);
        }
    }
}
