using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using task.Data;
using task.Models;

namespace task.Areas.admin.Controllers
{
    [Area("admin")]
    public class FooterInfoesController : Controller
    {
        private readonly AppDbContext _context;

        public FooterInfoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: admin/FooterInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.FooterInfos.ToListAsync());
        }

        // GET: admin/FooterInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footerInfo = await _context.FooterInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (footerInfo == null)
            {
                return NotFound();
            }

            return View(footerInfo);
        }

        // GET: admin/FooterInfoes/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/FooterInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(FooterInfo footerInfo, IFormFile LogoUrl)
        {
            if (LogoUrl != null && LogoUrl.Length > 0)
            {
                //file path
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(LogoUrl.FileName);
                //Save path
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await LogoUrl.CopyToAsync(stream);
                }
                footerInfo.LogoUrl = "/uploads/" + fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(footerInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(footerInfo);
        }

        // GET: admin/FooterInfoes/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footerInfo = await _context.FooterInfos.FindAsync(id);
            if (footerInfo == null)
            {
                return NotFound();
            }
            return View(footerInfo);
        }

        // POST: admin/FooterInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id, FooterInfo footerInfo, IFormFile LogoUrl)
        {
            if (id != footerInfo.Id)
            {
                return NotFound();
            }
            var existingClient = await _context.FooterInfos.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (existingClient == null)
            {
                return NotFound();
            }

            if (LogoUrl != null && LogoUrl.Length > 0)
            {
                //file path
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(LogoUrl.FileName);
                //Save path
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await LogoUrl.CopyToAsync(stream);
                }
                footerInfo.LogoUrl = "/uploads/" + fileName;
            }
            else
            {
                // خليه يحتفظ بالصورة القديمة
                footerInfo.LogoUrl = existingClient.LogoUrl;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(footerInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FooterInfoExists(footerInfo.Id))
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
            return View(footerInfo);
        }

        // GET: admin/FooterInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footerInfo = await _context.FooterInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (footerInfo == null)
            {
                return NotFound();
            }

            return View(footerInfo);
        }

        // POST: admin/FooterInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var footerInfo = await _context.FooterInfos.FindAsync(id);
            if (footerInfo != null)
            {
                _context.FooterInfos.Remove(footerInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FooterInfoExists(int id)
        {
            return _context.FooterInfos.Any(e => e.Id == id);
        }
    }
}
