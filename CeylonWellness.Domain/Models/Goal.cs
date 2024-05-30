using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeylonWellness.Domain.Models
{
    public class Goal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string GoalName { get; set; }
        public ICollection<DietPlan>? DietPlans { get; set; }

    }
}
