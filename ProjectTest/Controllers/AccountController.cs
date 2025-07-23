using Microsoft.AspNetCore.Mvc;

namespace ProjectTest.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
}
