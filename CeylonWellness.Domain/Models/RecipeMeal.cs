using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeylonWellness.Domain.Models
{
    public class RecipeMeal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid RecipeId { get; set; }

        public Guid MealId { get; set; }
    }
}
