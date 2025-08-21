using Microsoft.EntityFrameworkCore;
using task.Models;

namespace task.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SliderItem> Sliders { get; set; }
        public DbSet<AboutSection> AboutSections { get; set; }
        public DbSet<CounterItem> CounterItems { get; set; }
        public DbSet<ServiceItem> Services { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<FooterInfo> FooterInfos { get; set; }
    }

}
