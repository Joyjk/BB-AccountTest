using Microsoft.AspNetCore.Mvc;
using ProjectTest.Models;
using System.Diagnostics;

namespace ProjectTest.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EkycFirstPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitApplication(string accountType)
        {
            // Process the account application based on the selected accountType
            // You can add your business logic here

            // For demonstration, we'll just redirect to a confirmation page
            return RedirectToAction("ApplicationSubmitted", new { type = accountType });
        }
        public IActionResult ApplicationSubmitted(string type)
        {
            ViewBag.AccountType = type;
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Step1_IdentityVerification()
        {
            return View();
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



        public IActionResult Step2_ProductSelection()
        {
            return View();
        }
    }
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
