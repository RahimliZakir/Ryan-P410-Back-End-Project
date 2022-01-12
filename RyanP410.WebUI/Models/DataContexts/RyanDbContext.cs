using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Models.DataContexts
{
    public class RyanDbContext : DbContext
    {
        public RyanDbContext(DbContextOptions options)
               : base(options)
        {

        }

        public DbSet<AppInfo> AppInfos { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
    }
}
