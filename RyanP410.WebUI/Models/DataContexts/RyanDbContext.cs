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

        //---ABOUT---
        public DbSet<Person> Persons { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<PricingDetail> PricingDetails { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<FunFact> FunFacts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        //---ABOUT---

        public DbSet<AppInfo> AppInfos { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
    }
}
