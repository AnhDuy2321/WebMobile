using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Web.Models;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbDiDongContext _context;
        
        public ProductController(DbDiDongContext context)
        {
            _context = context;
        }
        [Route("shop.html", Name = ("ShopProduct"))]
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 6;
                var lsSanPhams = _context.SanPhams.AsNoTracking()
                    .OrderByDescending(x => x.MaSp);
                PagedList<SanPham> models = new PagedList<SanPham>(lsSanPhams, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //[Route("ProductList")]
        [Route("danhmuc/{TenLoai}", Name = ("ListProduct"))]
        public IActionResult List(string TenLoai, int page=1)
        {
            try
            {
                var pageSize = 10;
                var danhmuc = _context.Loais.AsNoTracking().SingleOrDefault(x => x.TenLoai == TenLoai);
                var lsSanPhams = _context.SanPhams.AsNoTracking()
                    .Where(x => x.MaLoai == danhmuc.MaLoai)
                    .OrderByDescending(x => x.MaSp);
                PagedList<SanPham> models = new PagedList<SanPham>(lsSanPhams, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentMaLoai = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index","Home");
            }           
        }
        //[Route("ProductDetails")]
        [Route("/{TenSp}-{id}.html", Name = ("ProductDetails"))]
        public IActionResult Details(int id)
        {
            try
            {
                var sp = _context.SanPhams.Include(x => x.MaLoaiNavigation).FirstOrDefault(x => x.MaSp == id);
                if (sp == null)
                {
                    return RedirectToAction("Index");
                }
                var lssp = _context.SanPhams
                    .AsNoTracking()
                    .Where(x => x.MaLoai == sp.MaLoai && x.MaSp != id )
                    .Take(2)
                    .OrderByDescending(x => x.MaSp)
                    .ToList();
                ViewBag.SanPham = lssp;
                return View(sp);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }         
        }
    }
}
