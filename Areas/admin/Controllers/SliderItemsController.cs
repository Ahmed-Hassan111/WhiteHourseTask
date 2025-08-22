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
    public class SliderItemsController : Controller
    {
        private readonly AppDbContext _context;

        public SliderItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: admin/SliderItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }

        // GET: admin/SliderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderItem = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sliderItem == null)
            {
                return NotFound();
            }

            return View(sliderItem);
        }

        // GET: admin/SliderItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/SliderItems/Create
        
        [HttpPost]
        public async Task<IActionResult> Create(SliderItem sliderItem, IFormFile ImagePath)
        {
            if (ImagePath != null && ImagePath.Length > 0)
            {
                // file path
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath.FileName);

                // file save
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagePath.CopyToAsync(stream);
                }

                
                sliderItem.ImageUrl = "/uploads/" + fileName;
            }

            if (ModelState.IsValid)
            {
                _context.Add(sliderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sliderItem);
        }

        // GET: admin/SliderItems/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderItem = await _context.Sliders.FindAsync(id);
            if (sliderItem == null)
            {
                return NotFound();
            }

            return View(sliderItem);
        }

        // POST: admin/SliderItems/Edit/5
        
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id, SliderItem sliderItem, IFormFile? ImagePath)
        {
            if (id != sliderItem.Id)
            {
                return NotFound();
            }
            var existingClient = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (existingClient == null)
            {
                return NotFound();
            }

            if (ImagePath != null && ImagePath.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagePath.CopyToAsync(stream);
                }

                sliderItem.ImageUrl = "/uploads/" + fileName;
            }
            else
            {
                
                sliderItem.ImageUrl = existingClient.ImageUrl;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sliderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Sliders.Any(e => e.Id == sliderItem.Id))
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
            return View(sliderItem);
        }


        // GET: admin/SliderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderItem = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sliderItem == null)
            {
                return NotFound();
            }

            return View(sliderItem);
        }

        // POST: admin/SliderItems/Delete/5
        [HttpPost, ActionName("Delete")]       
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sliderItem = await _context.Sliders.FindAsync(id);
            if (sliderItem != null)
            {
                _context.Sliders.Remove(sliderItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderItemExists(int id)
        {
            return _context.Sliders.Any(e => e.Id == id);
        }
    }
}
