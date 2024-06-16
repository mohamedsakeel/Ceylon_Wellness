using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeylonWellness.Domain.Models
{
    public class UserHealthInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int Age { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string ActivityLevel { get; set; }
        public int MaintaincalAmount { get; set; }
        public float TargetWeight { get; set; }
        public string MealPreference { get; set; }
        public string MeatType { get; set; }
        public double BMI { get; set; }
        public int NoofMealsperDay { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public double Neck { get; set; }
        public double Waist { get; set; }
        public double Hip { get; set; }

        public string Dairy { get; set; }
        public string Eggs { get; set; }

        public string MacroPreference { get; set; }

        public string wheyproteinpref { get; set; }
        public string mealintakepref { get; set; }
        public string macropref { get; set; }
    }
}
