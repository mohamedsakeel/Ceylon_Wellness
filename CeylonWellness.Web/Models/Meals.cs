namespace CeylonWellness.Web.Models
{
    public class Meals
    {
        public string MealName { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public string Preparation { get; set; }
        public string NutritionFacts { get; set; }
        public string MealType { get; set; }

    }

    public class MealDetailsViewModel
    {
        public string MealName { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public string NutritionFacts { get; set; }
    }
}
