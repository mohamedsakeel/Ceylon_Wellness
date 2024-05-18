using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeylonWellness.Domain.Models
{
    public class DietPlan
    {
        public Guid Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string PlanType { get; set; }
        public int Duration { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Goal")]
        public Guid GoalId { get; set; }
        public virtual Goal Goal { get; set; }

        public ICollection<Meal> Meals { get; set; }
    }
}
