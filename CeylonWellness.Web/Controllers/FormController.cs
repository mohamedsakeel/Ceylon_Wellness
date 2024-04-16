using CeylonWellness.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            var jsonString = HttpContext.Session.GetString(SessionKey);
            var formData = JsonSerializer.Deserialize<MultiStepFormViewModel>(jsonString);

            if (formData == null)
            {
                InitializeSession();
                jsonString = HttpContext.Session.GetString(SessionKey);
                formData = JsonSerializer.Deserialize<MultiStepFormViewModel>(jsonString);
            }

            // Update formData with modelData values (Example)
            formData.Age = modelData.Age;
            //formData.Gender = modelData.Gender;
            // ... add more properties as you add steps

            // Reserialize before storing
            jsonString = JsonSerializer.Serialize(formData);
            HttpContext.Session.SetString(SessionKey, jsonString);

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
