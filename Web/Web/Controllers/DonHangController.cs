using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.ModelViews;

namespace Web.Controllers
{
    public class DonHangController : Controller
    {
        private readonly DbDiDongContext _context;
        public INotyfService _notyfService { get; }
        public DonHangController(DbDiDongContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        [HttpPost]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var taikhoanID = HttpContext.Session.GetString("MaKh");
                if (string.IsNullOrEmpty(taikhoanID)) return RedirectToAction("Login", "Accounts");
                var khachhang = _context.KhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKh == Convert.ToInt32(taikhoanID));
                if (khachhang == null) return NotFound();
                var donhang = await _context.HoaDons
                    //.Include(x => x.TinhTrang)
                    .FirstOrDefaultAsync(m => m.MaHd == id && Convert.ToInt32(taikhoanID) == m.MaKh);
                if (donhang == null) return NotFound();

                var chitietdonhang = _context.ChiTietHoaDons
                    .Include(x => x.MaSpNavigation)
                    .AsNoTracking()
                    .Where(x => x.MaHd == id)
                    .OrderBy(x => x.MaCthd)
                    .ToList();
                XemDonHang donHang = new XemDonHang();
                donHang.hoadon = donhang;
                donHang.ChiTietDonHang = chitietdonhang;
                return PartialView("Details", donHang);

            }
            catch
            {
                return NotFound();
            }
        }
    }
}
