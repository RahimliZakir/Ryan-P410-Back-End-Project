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
        public DbSet<PricingsPricingDetailsCollection> PricingsPricingDetailsCollections { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<FunFact> FunFacts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        //---ABOUT---

        //---RESUME---
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Coding> Codings { get; set; }
        public DbSet<Knowledge> Knowledges { get; set; }
        //---RESUME---

        //---CONTACT---
        public DbSet<AppInfo> AppInfos { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        //---CONTACT---
    }
}
