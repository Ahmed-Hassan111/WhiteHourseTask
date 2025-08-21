using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using task.Data;
using task.Models;

namespace task.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Sliders = _context.Sliders.ToList(),
                About = _context.AboutSections.FirstOrDefault() ?? new AboutSection(),
                Counters = _context.CounterItems.ToList(), // ??? ?? ??? CounterItems
                Services = _context.Services.ToList(),
                Clients = _context.Clients.ToList(),
                Footer = _context.FooterInfos.FirstOrDefault() ?? new FooterInfo()
            };
            return View(model);
        }
    }
}

