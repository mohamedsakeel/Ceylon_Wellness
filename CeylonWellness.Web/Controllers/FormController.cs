using CeylonWellness.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CeylonWellness.Web.Extensions;

namespace CeylonWellness.Web.Controllers
{
    public class FormController : Controller
    {
        private const string SessionKey = "formData";
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

            // Add similar checks for other properties as needed

            // Update session data for the specific property being modified
            HttpContext.Session.Set(SessionKey, formData);

            return Ok();
        }

        [HttpPost]
        public IActionResult SubmitForm()
        {
            var jsonString = HttpContext.Session.GetString(SessionKey);
            var model = JsonSerializer.Deserialize<MultiStepFormViewModel>(jsonString);

            if (model == null)
            {
                return RedirectToAction("Index");
            }

            // Process Final Submission (Save to database, etc.)
            // ... your processing logic here

            HttpContext.Session.Remove(SessionKey);
            return RedirectToAction("Success");
        }

        //Initilize Session
        private void InitializeSession()
        {
            string jsonString = JsonSerializer.Serialize(new MultiStepFormViewModel());
            HttpContext.Session.SetString(SessionKey, jsonString);
        }
    }
}
