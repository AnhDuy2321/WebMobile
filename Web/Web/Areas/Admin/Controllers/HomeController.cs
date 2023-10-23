using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles = "Admin,Staff")]
        public IActionResult Index()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                return View("Index");
            }
            return RedirectToAction("AdminLogin", "Accounts", new { Area = "Admin" });
        }
    }
}
