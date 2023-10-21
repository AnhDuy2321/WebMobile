using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Extension;
using Web.Helpper;
using Web.Models;
using Web.ModelViews;

namespace Web.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly DbDiDongContext _context;
        public INotyfService _notyfService { get; }
        public CheckOutController(DbDiDongContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }
        [Route("checkout.html", Name = "Checkout")]
        public IActionResult Index(string returnUrl = null)
        {
            //Lay gio hang ra de xu ly
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var taikhoanID = HttpContext.Session.GetString("MaKh");
            MuaHangVM model = new MuaHangVM();
            if (taikhoanID != null)
            {
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKh == Convert.ToInt32(taikhoanID));
                model.MaKh = khachhang.MaKh;
                model.TenKh = khachhang.TenKh;
                model.Email = khachhang.Email;
                model.SDT = khachhang.Sdt;
                model.DiaChi = khachhang.DiaChi;
            }
            ViewBag.GioHang = cart;
            return View(model);
        }
        [HttpPost]
        [Route("checkout.html", Name = "Checkout")]
        public IActionResult Index(MuaHangVM muaHang)
        {
            //Lay ra gio hang de xu ly
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var taikhoanID = HttpContext.Session.GetString("MaKh");
            MuaHangVM model = new MuaHangVM();
            if (taikhoanID != null)
            {
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKh == Convert.ToInt32(taikhoanID));
                model.MaKh = khachhang.MaKh;
                model.TenKh = khachhang.TenKh;
                model.Email = khachhang.Email;
                model.SDT = khachhang.Sdt;
                model.DiaChi = khachhang.DiaChi;
                _context.Update(khachhang);
                _context.SaveChanges();
            }
            try
            {
                //if (ModelState.IsValid)
                //{
                //Khoi tao don hang
                HoaDon donhang = new HoaDon();
                donhang.MaKh = model.MaKh;

                donhang.NgayLap = DateTime.Now;
                donhang.Tong = Convert.ToInt32(cart.Sum(x => x.TotalMoney));
                _context.Add(donhang);
                _context.SaveChanges();
                //tao danh sach don hang

                foreach (var item in cart)
                {
                    ChiTietHoaDon orderDetail = new ChiTietHoaDon();
                    orderDetail.MaHd = donhang.MaHd;
                    orderDetail.MaSp = item.sanpham.MaSp;
                    orderDetail.SoLuong = item.amount;
                    orderDetail.ThanhTien = donhang.Tong;
                    orderDetail.DonGia = item.sanpham.Gia;
                    _context.Add(orderDetail);
                }
                _context.SaveChanges();
                //clear gio hang
                HttpContext.Session.Remove("GioHang");
                //Xuat thong bao
                _notyfService.Success("Đơn hàng đặt thành công");
                //cap nhat thong tin khach hang
                return RedirectToAction("Success");


                //}
            }
            catch
            {

                ViewBag.GioHang = cart;
                return View(model);
            }

           // ViewBag.GioHang = cart;
            //return View(model);
        }
        [Route("dat-hang-thanh-cong.html", Name = "Success")]
        public IActionResult Success()
        {
            try
            {
                var taikhoanID = HttpContext.Session.GetString("MaKh");
                if (string.IsNullOrEmpty(taikhoanID))
                {
                    return RedirectToAction("Login", "Accounts", new { returnUrl = "/dat-hang-thanh-cong.html" });
                }
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKh == Convert.ToInt32(taikhoanID));
                var donhang = _context.HoaDons
                    .Where(x => x.MaKh == Convert.ToInt32(taikhoanID))
                    .OrderByDescending(x => x.NgayLap)
                    .FirstOrDefault();
                MuaHangSuccessVM successVM = new MuaHangSuccessVM();
                successVM.TenKh = khachhang.TenKh;
                successVM.MaHd = donhang.MaHd;
                successVM.SDT = khachhang.Sdt;
                successVM.DiaChi = khachhang.DiaChi;
                return View(successVM);
            }
            catch
            {
                return View();
            }
        }
    }
}
