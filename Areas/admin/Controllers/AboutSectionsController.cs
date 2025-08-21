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
    public class AboutSectionsController : Controller
    {
        private readonly AppDbContext _context;

        public AboutSectionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: admin/AboutSections
        public async Task<IActionResult> Index()
        {
            return View(await _context.AboutSections.ToListAsync());
        }

        // GET: admin/AboutSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutSection = await _context.AboutSections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutSection == null)
            {
                return NotFound();
            }

            return View(aboutSection);
        }

        // GET: admin/AboutSections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/AboutSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(AboutSection aboutSection, IFormFile LogoUrl)
        {
            
                if (LogoUrl != null && LogoUrl.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(LogoUrl.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await LogoUrl.CopyToAsync(stream);
                    }

                    aboutSection.LogoUrl = "/uploads/" + fileName;
                    Console.WriteLine($"File saved at: {filePath}"); // للـ Debugging
                }
                else
                {
                    aboutSection.LogoUrl = "";
                    Console.WriteLine("No file uploaded.");
                }

                if (string.IsNullOrEmpty(aboutSection.ReadMoreLink))
                {
                    aboutSection.ReadMoreLink = null;
                }

                _context.Add(aboutSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            

            
            return View(aboutSection);
        }

        // POST: admin/AboutSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // GET: Admin/AboutSections/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var aboutSection = await _context.AboutSections.FindAsync(id);
            if (aboutSection == null)
            {
                return NotFound();
            }
            return View(aboutSection);
        }
        [HttpPost]
        
        public async Task<IActionResult> Edit(int id, [Bind("Id,LogoUrl,Description,ReadMoreLink")] AboutSection aboutSection)
        {
            if (id != aboutSection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aboutSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutSectionExists(aboutSection.Id))
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
            return View(aboutSection);
        }

        // GET: admin/AboutSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutSection = await _context.AboutSections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutSection == null)
            {
                return NotFound();
            }

            return View(aboutSection);
        }

        // POST: admin/AboutSections/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aboutSection = await _context.AboutSections.FindAsync(id);
            if (aboutSection != null)
            {
                _context.AboutSections.Remove(aboutSection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutSectionExists(int id)
        {
            return _context.AboutSections.Any(e => e.Id == id);
        }
    }
}
