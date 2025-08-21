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
    public class ServiceItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: admin/ServiceItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.Services.ToListAsync());
        }

        // GET: admin/ServiceItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceItem = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceItem == null)
            {
                return NotFound();
            }

            return View(serviceItem);
        }

        // GET: admin/ServiceItems/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/ServiceItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreatePost(ServiceItem serviceItem, IFormFile ImageUrl)
        {
            if (ImageUrl != null && ImageUrl.Length > 0)
            {
                // file path
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);

                // file save
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUrl.CopyToAsync(stream);
                }


                serviceItem.ImageUrl = "/uploads/" + fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(serviceItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceItem);
        }

        // GET: admin/ServiceItems/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceItem = await _context.Services.FindAsync(id);
            if (serviceItem == null)
            {
                return NotFound();
            }
            return View(serviceItem);
        }

        // POST: admin/ServiceItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ServiceItem serviceItem, IFormFile ImageUrl)
        {
            if (id != serviceItem.Id)
            {
                return NotFound();
            }

            var existingItem = await _context.Services.FindAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Title = serviceItem.Title;
            existingItem.Description = serviceItem.Description;

            if (ImageUrl != null && ImageUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUrl.CopyToAsync(stream);
                }

                existingItem.ImageUrl = "/uploads/" + fileName;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(existingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceItemExists(serviceItem.Id))
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
            return View(existingItem);
        }

        // GET: admin/ServiceItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceItem = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceItem == null)
            {
                return NotFound();
            }

            return View(serviceItem);
        }

        // POST: admin/ServiceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceItem = await _context.Services.FindAsync(id);
            if (serviceItem != null)
            {
                _context.Services.Remove(serviceItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceItemExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
