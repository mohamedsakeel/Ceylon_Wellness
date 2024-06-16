using CeylonWellness.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CeylonWellness.Web.Extensions;
using CeylonWellness.Domain.Models;
using Microsoft.EntityFrameworkCore;
using CeylonWellness.Web.Data;

namespace CeylonWellness.Web.Controllers
{
    public class FormController : Controller
    {
        private const string SessionKey = "formData";
        private readonly ApplicationDbContext _context;

        public FormController (ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult FormStepPlaceholder()
        {
            InitializeSession();
            return View("FormStepPlaceholder");
        }

        public IActionResult LoadPartial(string partialName)
        {
            var jsonString = HttpContext.Session.GetString(SessionKey);
            var formData = JsonSerializer.Deserialize<MultiStepFormViewModel>(jsonString);

            return PartialView(partialName, formData); // Pass model to partial view
        }

        [HttpPost]
        public IActionResult SaveStepData(MultiStepFormViewModel modelData)
        {
            // Get existing form data from session or initialize if it doesn't exist
            var formData = HttpContext.Session.Get<MultiStepFormViewModel>(SessionKey) ?? new MultiStepFormViewModel();
            // Update formData with modelData values
            if (modelData.Age != 0)
            {
                formData.Age = modelData.Age;
            }
            if (modelData.Gender != null)
            {
                formData.Gender = modelData.Gender;
            }
            if (modelData.Weight != 0)
            {
                formData.Weight = modelData.Weight;
            }
            if (modelData.Height != 0)
            {
                formData.Height = modelData.Height;
            }
            if (modelData.TargetWeight != 0)
            {
                formData.TargetWeight = modelData.TargetWeight;
            }
            if (modelData.goal != null)
            {
                formData.goal = modelData.goal;
            }
            if (modelData.ActivityLevel != null)
            {
                formData.ActivityLevel = modelData.ActivityLevel;
            }
            if (modelData.BMI != 0)
            {
                formData.BMI = modelData.BMI;
            }
            if (modelData.WeeklyGoal != null)
            {
                formData.WeeklyGoal = modelData.WeeklyGoal;
            }
            if (modelData.NoofMealsperDay != 0)
            {
                formData.NoofMealsperDay = modelData.NoofMealsperDay;
            }
            if (modelData.MaintaincalAmount != 0)
            {
                formData.MaintaincalAmount = modelData.MaintaincalAmount;
            }
            if (modelData.MeatType != null)
            {
                formData.MeatType = modelData.MeatType;
            }
            if (modelData.MealPreference != null)
            {
                formData.MealPreference = modelData.MealPreference;
            }

            // Add similar checks for other properties as needed

            // Update session data for the specific property being modified
            HttpContext.Session.Set(SessionKey, formData);

            return Ok();
        }

        [HttpGet,HttpPost]
        public IActionResult SubmitForm(string userid)
        {
            var jsonString = HttpContext.Session.GetString(SessionKey);
            var model = JsonSerializer.Deserialize<MultiStepFormViewModel>(jsonString);

            if (model == null)
            {
                return RedirectToAction("Index");
            }

            UserHealthInfo us = new UserHealthInfo();
            us.UserId = userid;
            us.ActivityLevel = model.ActivityLevel;
            us.Age = model.Age;
            us.Weight = model.Weight;
            us.TargetWeight = model.TargetWeight;
            us.BMI = model.BMI;
            us.NoofMealsperDay = model.NoofMealsperDay;
            us.MealPreference = model.MealPreference;
            us.MeatType = "Any";
            us.MaintaincalAmount = model.MaintaincalAmount;
            us.Height = model.Height;

            _context.userHealthInfos.Add(us);

            DietPlan dp = new DietPlan();
            dp.UserId = userid;
            dp.StartDate = DateOnly.FromDateTime(DateTime.Now.Date);
            dp.GoalId = GetGoalId(model.goal);
            dp.EndDate = model.EndDate;
            dp.Duration = 5;
            dp.PlanType = model.WeeklyGoal;

            _context.DietPlans.Add(dp);

            _context.SaveChanges();

            HttpContext.Session.Remove(SessionKey);

            return RedirectToAction("Index", "Home");
        }

        private Guid GetGoalId (string goalname)
        {
            try
            {
                var goal = _context.Goals.Where(x => x.GoalName == goalname).Select(x => x.Id).FirstOrDefault();

                return goal;
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }

#region Session Initialize
        //Initilize Session
        private void InitializeSession()
        {
            string jsonString = JsonSerializer.Serialize(new MultiStepFormViewModel());
            HttpContext.Session.SetString(SessionKey, jsonString);
        }
#endregion
    }
}
