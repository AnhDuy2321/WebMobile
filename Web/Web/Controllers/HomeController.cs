using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Web.Models;
using Web.ModelViews;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DbDiDongContext _context;

        public HomeController(ILogger<HomeController> logger, DbDiDongContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index(string searchString)
        {
            HomeModelViews model = new HomeModelViews();
            if (searchString != null)
            {
                var lsSanPhams = _context.SanPhams.AsNoTracking()
                .Where(x => x.TenSp.Contains(searchString))
                .OrderByDescending(x => x.TenSp)
                .ToList();
                List<SanPhamModelViews> lsProductViews = new List<SanPhamModelViews>();
                var lsLoais = _context.Loais
                    .AsNoTracking()
                    .OrderByDescending(x => x.TenLoai)
                    .ToList();

                foreach (var item in lsLoais)
                {
                    SanPhamModelViews productHome = new SanPhamModelViews();
                    productHome.Loai = item;
                    productHome.lsSanPhams = lsSanPhams.Where(x => x.MaLoai == item.MaLoai).ToList();
                    lsProductViews.Add(productHome);

                    model.SanPhams = lsProductViews;
                    ViewBag.AllProducts = lsSanPhams;
                }
                return View(model);
            }
            else
            {
                var lsSanPhams = _context.SanPhams.AsNoTracking()
                .OrderByDescending(x => x.TenSp)
                .ToList();


                List<SanPhamModelViews> lsProductViews = new List<SanPhamModelViews>();
                var lsLoais = _context.Loais
                    .AsNoTracking()
                    .OrderByDescending(x => x.TenLoai)
                    .ToList();

                foreach (var item in lsLoais)
                {
                    SanPhamModelViews productHome = new SanPhamModelViews();
                    productHome.Loai = item;
                    productHome.lsSanPhams = lsSanPhams.Where(x => x.MaLoai == item.MaLoai).ToList();
                    lsProductViews.Add(productHome);

                    model.SanPhams = lsProductViews;
                    ViewBag.AllProducts = lsSanPhams;
                }
                return View(model);
            }
        }

        //public IActionResult Index()
        //{
        //    HomeModelViews model = new HomeModelViews();

        //    var lsSanPhams = _context.SanPhams.AsNoTracking()
        //        .OrderByDescending(x => x.TenSp)
        //        .ToList();

        //    List<SanPhamModelViews> lsProductViews = new List<SanPhamModelViews>();
        //    var lsLoais = _context.Loais
        //        .AsNoTracking()
        //        .OrderByDescending(x => x.TenLoai)
        //        .ToList();

        //    foreach (var item in lsLoais)
        //    {
        //        SanPhamModelViews productHome = new SanPhamModelViews();
        //        productHome.Loai = item;
        //        productHome.lsSanPhams = lsSanPhams.Where(x => x.MaLoai == item.MaLoai).ToList();
        //        lsProductViews.Add(productHome);

        //        model.SanPhams = lsProductViews;
        //        ViewBag.AllProducts = lsSanPhams;
        //    }
        //    return View(model);
        //}
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}