using CeylonWellness.Domain.Models;

namespace CeylonWellness.Web.Models
{
    public class MultiStepFormViewModel
    {
        //public UserHealthInfo UserHealInfo { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string ActivityLevel { get; set; }
        public int MaintaincalAmount { get; set; }
        public float TargetWeight { get; set; }
        public string MealPreference { get; set; }
        public string MeatType { get; set; }
        public string WeeklyGoal { get; set; }

        public int NoofMealsperDay { get; set; }
        public string goal {  get; set; }
    }
}
