using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.Entities;
using RyanP410.WebUI.Models.Entities.Membership;

namespace RyanP410.WebUI.Models.DataContexts
{
    public class RyanDbContext :
        IdentityDbContext<RyanUser, RyanRole, int, RyanUserClaim, RyanUserRole, RyanUserLogin, RyanRoleClaim, RyanUserToken>
    {
        public RyanDbContext(DbContextOptions options)
               : base(options)
        {

        }

        //---IDENTITY---
        public DbSet<RyanUser> Users { get; set; }
        public DbSet<RyanRole> Roles { get; set; }
        public DbSet<RyanUserClaim> UserClaims { get; set; }
        public DbSet<RyanUserRole> UserRoles { get; set; }
        public DbSet<RyanUserLogin> UserLogins { get; set; }
        public DbSet<RyanRoleClaim> RoleClaims { get; set; }
        public DbSet<RyanUserToken> UserTokens { get; set; }
        //---IDENTITY---

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

        //---WORKS---
        public DbSet<Category> Categories { get; set; }
        public DbSet<Work> Works { get; set; }
        //---WORKS---

        //---CONTACT---
        public DbSet<AppInfo> AppInfos { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        //---CONTACT---

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RyanUser>(e =>
            {
                e.ToTable("Users", "Membership");
            });

            builder.Entity<RyanRole>(e =>
            {
                e.ToTable("Roles", "Membership");
            });

            builder.Entity<RyanUserRole>(e =>
            {
                e.ToTable("UserRoles", "Membership");
            });

            builder.Entity<RyanUserClaim>(e =>
            {
                e.ToTable("UserClaims", "Membership");
            });

            builder.Entity<RyanRoleClaim>(e =>
            {
                e.ToTable("RoleClaims", "Membership");
            });

            builder.Entity<RyanUserToken>(e =>
            {
                e.ToTable("UserTokens", "Membership");
            });

            builder.Entity<RyanUserLogin>(e =>
            {
                e.ToTable("UserLogins", "Membership");
            });
        }
    }
}
