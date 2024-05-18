using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeylonWellness.Domain.Models
{
    public class Meal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string MealName { get; set; }
        public float Calorie { get; set; }
        [ForeignKey("DietPlan")]
        public Guid PlanId { get; set; }
        public virtual DietPlan DietPlan { get; set; }
        public ICollection<NutritionQuantity> NutritionQuantities { get; set; }
    }
}
