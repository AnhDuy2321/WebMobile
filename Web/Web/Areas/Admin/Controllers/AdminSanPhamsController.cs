using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Web.Helpper;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles= "Admin,Staff")]
    public class AdminSanPhamsController : Controller
    {
        private readonly DbDiDongContext _context;
        public INotyfService _notyfService { get; }
        public AdminSanPhamsController(DbDiDongContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminSanPhams
        public IActionResult Index(int page = 1, int MaLoai = 0)
        {
            var pageNumber = page ;
            var pageSize = 4;
            List<SanPham> lsSanPhams = new List<SanPham>();
            if (MaLoai != 0)
            {
                lsSanPhams = _context.SanPhams
                .AsNoTracking()
                .Where(s=>s.MaLoai== MaLoai)
                .Include(s => s.MaKhoNavigation)
                .Include(s => s.MaLoaiNavigation)
                .Include(s => s.MaNsxNavigation)
                .OrderByDescending(x => x.MaSp).ToList();
            }
            else
            {
                lsSanPhams = _context.SanPhams
                .AsNoTracking()
                .Include(s => s.MaKhoNavigation)
                .Include(s => s.MaLoaiNavigation)
                .Include(s => s.MaNsxNavigation)
                .OrderByDescending(x => x.MaSp).ToList();
            }   
            PagedList<SanPham> models = new PagedList<SanPham>(lsSanPhams.AsQueryable(), pageNumber, pageSize);
            ViewBag.CurrentCateID = MaLoai;
            ViewBag.CurrentPage = pageNumber;
            ViewData["DanhMuc"] = new SelectList(_context.Loais, "MaLoai", "TenLoai");
            return View(models);
        }
        public IActionResult Filtter(int MaLoai = 0)
        {
            var url = $"/Admin/AdminSanPhams?MaLoai={MaLoai}";
            if (MaLoai == 0)
            {
                url = $"/Admin/AdminSanPhams";
            }
            return Json(new { status = "success", redirectUrl = url });
        }
        // GET: Admin/AdminSanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SanPhams == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaKhoNavigation)
                .Include(s => s.MaLoaiNavigation)
                .Include(s => s.MaNsxNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: Admin/AdminSanPhams/Create
        public IActionResult Create()
        {
            ViewData["MaKho"] = new SelectList(_context.Khos, "MaKho", "MaKho");
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "MaLoai");
            ViewData["MaNsx"] = new SelectList(_context.NhaSanXuats, "MaNsx", "MaNsx");
            return View();
        }

        // POST: Admin/AdminSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,SoLuong,Gia,Hinh,MaLoai,MaNsx,MaKho,MoTa")] SanPham sanPham, Microsoft.AspNetCore.Http.IFormFile hinh)
        {
            if (ModelState.IsValid)
            {
                sanPham.TenSp = Utilities.ToTitleCase(sanPham.TenSp);
                if (hinh != null)
                {
                    string extension = Path.GetExtension(hinh.FileName);
                    string image = Utilities.SEOUrl(sanPham.TenSp) + extension;
                    sanPham.Hinh = await Utilities.UploadFile(hinh, @"sanpham", image.ToLower());
                }

                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                _notyfService.Success("Tạo mới thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKho"] = new SelectList(_context.Khos, "MaKho", "MaKho", sanPham.MaKho);
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "MaLoai", sanPham.MaLoai);
            ViewData["MaNsx"] = new SelectList(_context.NhaSanXuats, "MaNsx", "MaNsx", sanPham.MaNsx);
            return View(sanPham);
        }

        // GET: Admin/AdminSanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SanPhams == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["MaKho"] = new SelectList(_context.Khos, "MaKho", "MaKho", sanPham.MaKho);
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "MaLoai", sanPham.MaLoai);
            ViewData["MaNsx"] = new SelectList(_context.NhaSanXuats, "MaNsx", "MaNsx", sanPham.MaNsx);
            return View(sanPham);
        }

        // POST: Admin/AdminSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSp,TenSp,SoLuong,Gia,Hinh,MaLoai,MaNsx,MaKho,MoTa")] SanPham sanPham, Microsoft.AspNetCore.Http.IFormFile hinh)
        {
            if (id != sanPham.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sanPham.TenSp = Utilities.ToTitleCase(sanPham.TenSp);
                    if (hinh != null)
                    {
                        string extension = Path.GetExtension(hinh.FileName);
                        string image = Utilities.SEOUrl(sanPham.TenSp) + extension;
                        sanPham.Hinh = await Utilities.UploadFile(hinh, @"sanpham", image.ToLower());
                    }

                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Cập nhật thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.MaSp))
                    {
                        _notyfService.Success("Có lỗi xảy ra");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKho"] = new SelectList(_context.Khos, "MaKho", "MaKho", sanPham.MaKho);
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "MaLoai", sanPham.MaLoai);
            ViewData["MaNsx"] = new SelectList(_context.NhaSanXuats, "MaNsx", "MaNsx", sanPham.MaNsx);
            return View(sanPham);
        }

        // GET: Admin/AdminSanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SanPhams == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaKhoNavigation)
                .Include(s => s.MaLoaiNavigation)
                .Include(s => s.MaNsxNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Admin/AdminSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SanPhams == null)
            {
                return Problem("Entity set 'DbDiDongContext.SanPhams'  is null.");
            }
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
            }
            
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
          return (_context.SanPhams?.Any(e => e.MaSp == id)).GetValueOrDefault();
        }
    }
}
