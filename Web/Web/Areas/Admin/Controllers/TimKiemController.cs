using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TimKiemController : Controller
    {
        private readonly DbDiDongContext _context;

        public TimKiemController(DbDiDongContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult FindProduct(string keyword)
        {
            List<SanPham> ls = new List<SanPham>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("TimKiemPartial", null);
            }
            ls = _context.SanPhams.AsNoTracking()
                                  .Include(a => a.MaLoaiNavigation)
                                  .Where(x => x.TenSp.Contains(keyword))
                                  .OrderByDescending(x => x.TenSp)
                                  .Take(10)
                                  .ToList();
            if (ls == null)
            {
                return PartialView("TimKiemPartial", null);
            }
            else
            {
                return PartialView("TimKiemPartial", ls);
            }
        }
    }
}
