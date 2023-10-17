    using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web.Extension;
using Web.Helpper;
using Web.Models;
using Web.ModelViews;

namespace Web.Controllers
{
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
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone(string SDT)
        {
            try
            {
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.Sdt.ToLower() == SDT.ToLower());
                if (khachhang != null)
                    return Json(data: "Số điện thoại : " + SDT + "đã được sử dụng");

                return Json(data: true);

            }
            catch
            {
                return Json(data: true);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string Email)
        {
            try
            {
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
                if (khachhang != null)
                    return Json(data: "Email : " + Email + " đã được sử dụng");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }
        [Route("tai-khoan-cua-toi.html", Name = "Dashboard")]
        public IActionResult Dashboard()
        {
            var taikhoanID = HttpContext.Session.GetString("MaKh");
            if (taikhoanID != null)
            {
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKh == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    var lsHoaDon = _context.HoaDons
                        .AsNoTracking()
                        .Where(x => x.MaKh == khachhang.MaKh)
                        .ToList();
                    ViewBag.HoaDon = lsHoaDon;
                    return View(khachhang);
                }

            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public IActionResult DangkyTaiKhoan()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public async Task<IActionResult> DangkyTaiKhoan(RegisterViewModel taikhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string salt = Utilities.GetRandomKey();
                    KhachHang khachhang = new KhachHang
                    {
                        TenKh = taikhoan.TenKh,
                        Sdt = taikhoan.Sdt.Trim().ToLower(),
                        Email = taikhoan.Email.Trim().ToLower(),
                        MatKhau = (taikhoan.MatKhau + salt.Trim()).ToMD5(),
                        Salt = salt,
                    };
                    try
                    {
                        _context.Add(khachhang);
                        await _context.SaveChangesAsync();
                        //Lưu Session MaKh
                        HttpContext.Session.SetString("MaKh", khachhang.MaKh.ToString());
                        var taikhoanID = HttpContext.Session.GetString("MaKh");

                        //Identity
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,khachhang.TenKh),
                            new Claim("MaKh", khachhang.MaKh.ToString())
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        _notyfService.Success("Đăng ký thành công");
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                    catch
                    {
                        return RedirectToAction("DangkyTaiKhoan", "Accounts");
                    }
                }
                else
                {
                    return View(taikhoan);
                }
            }
            catch
            {
                return View(taikhoan);
            }
        }
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public IActionResult Login(string returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("MaKh");
            if (taikhoanID != null)
            {
                return RedirectToAction("Dashboard", "Accounts");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> Login(LoginViewModel kh)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isEmail = Utilities.IsValidEmail(kh.UserName);
                    if (!isEmail) return View(kh);

                    var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == kh.UserName);

                    if (khachhang == null) return RedirectToAction("DangkyTaiKhoan");
                    string pass = (kh.Password + khachhang.Salt.Trim()).ToMD5();
                    if (khachhang.MatKhau != pass)
                    {
                        _notyfService.Success("Thông tin đăng nhập chưa chính xác");
                        return View(kh);
                    }
                    //Luu Session MaKh
                    HttpContext.Session.SetString("MaKh", khachhang.MaKh.ToString());
                    var taikhoanID = HttpContext.Session.GetString("MaKh");

                    //Identity 
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, khachhang.TenKh),
                        new Claim("MaKh", khachhang.MaKh.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    _notyfService.Success("Đăng nhập thành công");
                    return RedirectToAction("Dashboard", "Accounts");
                }
            }
            catch
            {
                return RedirectToAction("DangkyTaiKhoan", "Accounts");
            }
            return View(kh);
        }
        [HttpGet]
        [Route("dang-xuat.html", Name = "DangXuat")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("MaKh");
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult DoiMatKhau(DoiMatKhauModelView model)
        {
            try
            {
                var taikhoanID = HttpContext.Session.GetString("MaKh");
                if (taikhoanID == null)
                {
                    return RedirectToAction("Login", "Accounts");
                }
                if (ModelState.IsValid)
                {
                    var taikhoan = _context.KhachHangs.Find(Convert.ToInt32(taikhoanID));
                    if (taikhoan == null) return RedirectToAction("Login", "Accounts");
                    var pass = (model.MatKhauHienTai.Trim() + taikhoan.Salt.Trim()).ToMD5();
                    {
                        string passnew = (model.MatKhau.Trim() + taikhoan.Salt.Trim()).ToMD5();
                        taikhoan.MatKhau = passnew;
                        _context.Update(taikhoan);  
                        _context.SaveChanges();
                        _notyfService.Success("Đổi mật khẩu thành công");
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                }
            }
            catch
            {
                _notyfService.Success("Thay đổi mật khẩu không thành công");
                return RedirectToAction("Dashboard", "Accounts");
            }
            _notyfService.Success("Thay đổi mật khẩu không thành công");
            return RedirectToAction("Dashboard", "Accounts");
        }
    }
}
