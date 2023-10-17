using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTaiKhoansController : Controller
    {
        private readonly DbDiDongContext _context;

        public AdminTaiKhoansController(DbDiDongContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminTaiKhoans
        public async Task<IActionResult> Index()
        {
            ViewData["QuyenTruyCap"] = new SelectList(_context.VaiTros, "MaVaiTro", "Description");

            var dbDiDongContext = _context.TaiKhoans.Include(t => t.MaVaiTroNavigation);
            return View(await dbDiDongContext.ToListAsync());
        }

        // GET: Admin/AdminTaiKhoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaiKhoans == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.MaVaiTroNavigation)
                .FirstOrDefaultAsync(m => m.MaTaiKhoan == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // GET: Admin/AdminTaiKhoans/Create
        public IActionResult Create()
        {
            ViewData["MaVaiTro"] = new SelectList(_context.VaiTros, "MaVaiTro", "MaVaiTro");
            return View();
        }

        // POST: Admin/AdminTaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTaiKhoan,Sdt,Email,MatKhau,MaVaiTro,Salt")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taiKhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaVaiTro"] = new SelectList(_context.VaiTros, "MaVaiTro", "MaVaiTro", taiKhoan.MaVaiTro);
            return View(taiKhoan);
        }

        // GET: Admin/AdminTaiKhoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaiKhoans == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            ViewData["MaVaiTro"] = new SelectList(_context.VaiTros, "MaVaiTro", "MaVaiTro", taiKhoan.MaVaiTro);
            return View(taiKhoan);
        }

        // POST: Admin/AdminTaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTaiKhoan,Sdt,Email,MatKhau,MaVaiTro,Salt")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.MaTaiKhoan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.MaTaiKhoan))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaVaiTro"] = new SelectList(_context.VaiTros, "MaVaiTro", "MaVaiTro", taiKhoan.MaVaiTro);
            return View(taiKhoan);
        }

        // GET: Admin/AdminTaiKhoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaiKhoans == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.MaVaiTroNavigation)
                .FirstOrDefaultAsync(m => m.MaTaiKhoan == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: Admin/AdminTaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaiKhoans == null)
            {
                return Problem("Entity set 'DbDiDongContext.TaiKhoans'  is null.");
            }
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan != null)
            {
                _context.TaiKhoans.Remove(taiKhoan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiKhoanExists(int id)
        {
          return (_context.TaiKhoans?.Any(e => e.MaTaiKhoan == id)).GetValueOrDefault();
        }
    }
}
