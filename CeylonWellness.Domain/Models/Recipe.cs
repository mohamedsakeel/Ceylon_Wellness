using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeylonWellness.Domain.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string CookingMethod { get; set; }
        public string RecipeDescription { get; set; }
    }
}
