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
        public int NoofMealsperDay { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
