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
    public class CounterItemsController : Controller
    {
        private readonly AppDbContext _context;

        public CounterItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: admin/CounterItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.CounterItems.ToListAsync());
        }

        // GET: admin/CounterItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var counterItem = await _context.CounterItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counterItem == null)
            {
                return NotFound();
            }

            return View(counterItem);
        }

        // GET: admin/CounterItems/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/CounterItems/Create
        
        [HttpPost]
        public async Task<IActionResult> Create(CounterItem counterItem, IFormFile IconUrl)
        {
            if(IconUrl != null && IconUrl.Length > 0)
            {
                //file path
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(IconUrl.FileName);
                //Save path
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await IconUrl.CopyToAsync(stream);
                }
                counterItem.IconUrl = "/uploads/" + fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(counterItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(counterItem);
        }

        // GET: admin/CounterItems/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var counterItem = await _context.CounterItems.FindAsync(id);
            if (counterItem == null)
            {
                return NotFound();
            }
            return View(counterItem);
        }
        // POST: admin/CounterItems/Edit/5
        
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, CounterItem counterItem, IFormFile? IconPath)
        {
            if (id != counterItem.Id)
            {
                return NotFound();
            }

            var existingClient = await _context.CounterItems.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (existingClient == null)
            {
                return NotFound();
            }

            if (IconPath != null && IconPath.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(IconPath.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await IconPath.CopyToAsync(stream);
                }

                counterItem.IconUrl = "/uploads/" + fileName;
            }
            else
            {
                
                counterItem.IconUrl = existingClient.IconUrl;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(counterItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CounterItemExists(counterItem.Id))
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
            return View(counterItem);
        }

        // GET: admin/CounterItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var counterItem = await _context.CounterItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counterItem == null)
            {
                return NotFound();
            }

            return View(counterItem);
        }

        // POST: admin/CounterItems/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var counterItem = await _context.CounterItems.FindAsync(id);
            if (counterItem != null)
            {
                _context.CounterItems.Remove(counterItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CounterItemExists(int id)
        {
            return _context.CounterItems.Any(e => e.Id == id);
        }
    }
}
