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
        public string Diatpref { get; set; }
        public string MeatType { get; set; }
        public string WeeklyGoal { get; set; }
        public double BMI { get; set; }
        public int NoofMealsperDay { get; set; }
        public Guid goalid {  get; set; }
        public string goal {  get; set; }

        public double Neck { get; set; }
        public double Waist { get; set; }
        public double Hip { get; set; }

        public string Dairy { get; set; }
        public string Eggs { get; set; }

        public string MacroPreference { get; set; }

        public string wheyproteinpref { get; set; }
        public string mealintakepref { get; set; }
        public string macropref { get; set; }
        public string MealPreference { get; set; }
        public DateOnly EndDate { get; set; }

    }
}
