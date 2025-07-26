using Microsoft.AspNetCore.Mvc;
using ProjectTest.Models;

namespace ProjectTest.Controllers
{
    public class AccountOpeningController : Controller
    {
        private const int TotalSteps = 7;

        public IActionResult Index()
        {
            var model = new AccountOpeningModel();
            return View("Step1", model); // This will look for Views/AccountOpening/Step1.cshtml
        }

        [HttpPost]
        public IActionResult NextStep(AccountOpeningModel model, int currentStep)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View($"Step{currentStep}", model);
            //}

            model.CurrentStep = currentStep < TotalSteps ? currentStep + 1 : currentStep;
            return View($"Step{model.CurrentStep}", model);
        }

        [HttpPost]
        public IActionResult PreviousStep(AccountOpeningModel model, int currentStep)
        {
            model.CurrentStep = currentStep > 1 ? currentStep - 1 : currentStep;
            return View($"Step{model.CurrentStep}", model);
        }

        [HttpPost]
        public IActionResult Submit(AccountOpeningModel model)
        {
            if (!ModelState.IsValid)
            {
                return View($"Step{TotalSteps}", model);
            }

            // Process the complete application
            // Save to database, etc.

            return View("Confirmation");
        }


        [HttpPost]
        public async Task<IActionResult> Step1_IdentityVerification(IFormFile nidFront, IFormFile nidBack)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            if (nidFront != null && nidFront.Length > 0)
            {
                var path = Path.Combine(uploadPath, "nidFront_" + Guid.NewGuid() + Path.GetExtension(nidFront.FileName));
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await nidFront.CopyToAsync(stream);
                }
            }

            if (nidBack != null && nidBack.Length > 0)
            {
                var path = Path.Combine(uploadPath, "nidBack_" + Guid.NewGuid() + Path.GetExtension(nidBack.FileName));
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await nidBack.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Step2_ProductSelection");
        }
    }
}
