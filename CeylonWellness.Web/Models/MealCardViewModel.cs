namespace CeylonWellness.Web.Models
{
    public class MealCardViewModel
    {
        public string MealName { get; set; }
        public string ImageUrl { get; set; } // Optional, for displaying meal image
        public string MealType { get; set; } // Breakfast, Lunch, Dinner
        public List<string> Ingredients { get; set; } // Joined string for display
        public string Preparation { get; set; }
        public string NutritionFacts { get; set; }
    }
}
