using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web.Areas.Admin.Models;
using Web.Extension;
using Web.Helpper;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly DbDiDongContext _context;
        public INotyfService _notyfService { get; }
        public AccountsController(DbDiDongContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        [Route("login.html", Name = "Login")]
        public IActionResult AdminLogin(string returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("MaTaiKhoan");
            if (taikhoanID != null) return RedirectToAction("Index", "Home", new { Area = "Admin" });
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login.html", Name = "Login")]
        public async Task<IActionResult> AdminLogin(AdminLoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TaiKhoan kh = _context.TaiKhoans
                    .Include(p => p.MaVaiTroNavigation)
                    .SingleOrDefault(p => p.Email.ToLower().Trim() == model.UserName.ToLower().Trim());

                    if (kh == null)
                    {
                        ViewBag.Error = "Thông tin đăng nhập chưa chính xác";
                    }
                    string pass = (model.Password);
                    // + kh.Salt.Trim()
                    if (kh.MatKhau.Trim() != pass)
                    {
                        ViewBag.Error = "Thông tin đăng nhập chưa chính xác";
                        return View(model);
                    }
                    //đăng nhập thành công

                    await _context.SaveChangesAsync();


                    var taikhoanID = HttpContext.Session.GetString("MaTaiKhoan");
                    //identity
                    //luuw seccion Makh
                    HttpContext.Session.SetString("MaTaiKhoan", kh.MaTaiKhoan.ToString());

                    //identity
                    var userClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, kh.Email),
                        new Claim("MaTaiKhoan", kh.MaTaiKhoan.ToString()),
                        new Claim("MaVaiTro", kh.MaVaiTro.ToString()),
                        new Claim(ClaimTypes.Role, kh.MaVaiTroNavigation.VaiTro1.ToString().Trim())
                    };
                    var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");
                    var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                    await HttpContext.SignInAsync(userPrincipal);
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
            }
            catch
            {
                return RedirectToAction("AdminLogin", "Accounts", new { Area = "Admin" });
            }
            return RedirectToAction("AdminLogin", "Accounts", new { Area = "Admin" });
        }

        [Route("logout.html", Name = "Logout")]
        public IActionResult AdminLogout()
        {
            try
            {
                HttpContext.SignOutAsync();
                HttpContext.Session.Remove("MaTaiKhoan");
                return RedirectToAction("AdminLogin", "Accounts", new { Area = "Admin" });
            }
            catch
            {
                return RedirectToAction("AdminLogin", "Accounts", new { Area = "Admin" });
            }
        }

        [HttpGet("AccessDenied")]
        [Authorize]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
