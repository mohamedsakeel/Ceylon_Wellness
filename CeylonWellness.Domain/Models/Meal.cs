using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeylonWellness.Domain.Models
{
    public class Meal
    {
        public Guid Id { get; set; }
        public string MealName { get; set; }
        public float Calorie { get; set; }
    }
}
