using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeylonWellness.Domain.Models
{
    public class NutritionQuantity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Calories { get; set; }

        [ForeignKey("Meal")]
        public Guid MealId { get; set; }
        public virtual Meal Meal { get; set; }

    }
}
