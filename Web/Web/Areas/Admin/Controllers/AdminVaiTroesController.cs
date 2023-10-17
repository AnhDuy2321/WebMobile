using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminVaiTroesController : Controller
    {
        private readonly DbDiDongContext _context;
        public INotyfService _notyfService { get; }

        public AdminVaiTroesController(DbDiDongContext context,INotyfService notyfService)
        {
            _context = context;
            _notyfService= notyfService;
        }

        // GET: Admin/AdminVaiTroes
        public async Task<IActionResult> Index()
        {
              return _context.VaiTros != null ? 
                          View(await _context.VaiTros.ToListAsync()) :
                          Problem("Entity set 'DbDiDongContext.VaiTros'  is null.");
        }

        // GET: Admin/AdminVaiTroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VaiTros == null)
            {
                return NotFound();
            }

            var vaiTro = await _context.VaiTros
                .FirstOrDefaultAsync(m => m.MaVaiTro == id);
            if (vaiTro == null)
            {
                return NotFound();
            }

            return View(vaiTro);
        }

        // GET: Admin/AdminVaiTroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminVaiTroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaVaiTro,VaiTro1,Description")] VaiTro vaiTro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaiTro);
                await _context.SaveChangesAsync();
                _notyfService.Success("Tạo mới thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(vaiTro);
        }

        // GET: Admin/AdminVaiTroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VaiTros == null)
            {
                return NotFound();
            }

            var vaiTro = await _context.VaiTros.FindAsync(id);
            if (vaiTro == null)
            {
                return NotFound();
            }
            return View(vaiTro);
        }

        // POST: Admin/AdminVaiTroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaVaiTro,VaiTro1,Description")] VaiTro vaiTro)
        {
            if (id != vaiTro.MaVaiTro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaiTro);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Cập nhật thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaiTroExists(vaiTro.MaVaiTro))
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
            return View(vaiTro);
        }

        // GET: Admin/AdminVaiTroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VaiTros == null)
            {
                return NotFound();
            }

            var vaiTro = await _context.VaiTros
                .FirstOrDefaultAsync(m => m.MaVaiTro == id);
            if (vaiTro == null)
            {
                return NotFound();
            }

            return View(vaiTro);
        }

        // POST: Admin/AdminVaiTroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VaiTros == null)
            {
                return Problem("Entity set 'DbDiDongContext.VaiTros'  is null.");
            }
            var vaiTro = await _context.VaiTros.FindAsync(id);
            if (vaiTro != null)
            {
                _context.VaiTros.Remove(vaiTro);
            }
            
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool VaiTroExists(int id)
        {
          return (_context.VaiTros?.Any(e => e.MaVaiTro == id)).GetValueOrDefault();
        }
    }
}
