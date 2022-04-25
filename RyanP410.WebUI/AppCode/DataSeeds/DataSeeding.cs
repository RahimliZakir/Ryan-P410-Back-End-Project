using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using RyanP410.WebUI.Models.Entities.Membership;
using RyanP410.WebUI.Models.Entities.Membership.Credentials;

namespace RyanP410.WebUI.AppCode.DataSeeds
{
    public static class DataSeeding
    {
        async public static Task<IApplicationBuilder> SeedData(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                RyanDbContext db = scope.ServiceProvider.GetRequiredService<RyanDbContext>();
                RoleManager<RyanRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RyanRole>>();
                UserManager<RyanUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<RyanUser>>();

                db.Database.Migrate();

                RyanRole roleResult = roleManager.FindByNameAsync("Admin").Result;

                //---IDENTITY---
                if (roleResult == null)
                {
                    roleResult = new RyanRole
                    {
                        Name = "Admin"
                    };

                    IdentityResult roleResponse = roleManager.CreateAsync(roleResult).Result;

                    if (roleResponse.Succeeded)
                    {
                        RyanUser userResult = userManager.FindByNameAsync("rahimlizakir").Result;

                        if (userResult == null)
                        {
                            userResult = new RyanUser
                            {
                                UserName = "rahimlizakir",
                                Email = "zakirer@code.edu.az"
                            };

                            IdentityResult userResponse = userManager.CreateAsync(userResult, AdminCredential.Pick()).Result;

                            if (userResponse.Succeeded)
                            {
                                var roleUserResult = userManager.AddToRoleAsync(userResult, roleResult.Name).Result;
                            }
                        }
                        else
                        {
                            var roleUserResult = userManager.AddToRoleAsync(userResult, roleResult.Name).Result;
                        }
                    }
                }
                else
                {
                    RyanUser userResult = userManager.FindByNameAsync("rahimlizakir").Result;

                    if (userResult == null)
                    {
                        userResult = new RyanUser
                        {
                            UserName = "rahimlizakir",
                            Email = "zakirer@code.edu.az"
                        };

                        IdentityResult userResponse = userManager.CreateAsync(userResult, AdminCredential.Pick()).Result;

                        if (userResponse.Succeeded)
                        {
                            var roleUserResult = userManager.AddToRoleAsync(userResult, roleResult.Name).Result;
                        }
                    }
                    else
                    {
                        var roleUserResult = userManager.AddToRoleAsync(userResult, roleResult.Name).Result;
                    }
                }
                //---IDENTITY---

                //---ABOUT---
                if (!db.Persons.Any())
                {
                    await db.Persons.AddAsync(new Person
                    {
                        Name = "Ryan",
                        Surname = "Adlard",
                        ImagePath = "person-11896eb65e6f4e6c8ea6bea40dd83253.jpg",
                        CvResumePath = "resume.pdf",
                        Description = "I am Ryan Adlard, web designer from USA, California. I have rich experience in web site design and building and customization, also I am good at wordpress. I love to talk with you about our unique. ",
                        Age = 23,
                        Residence = "USA",
                        Freelance = true,
                        Address = "California, USA"
                    });

                    await db.SaveChangesAsync();
                }

                if (!db.Skills.Any())
                {
                    await db.Skills.AddAsync(new Skill
                    {
                        Name = "Web Designer"
                    });

                    await db.Skills.AddAsync(new Skill
                    {
                        Name = "Developer"
                    });

                    await db.Skills.AddAsync(new Skill
                    {
                        Name = "Freelancer"
                    });

                    await db.SaveChangesAsync();
                }

                if (!db.Pricings.Any())
                {
                    await db.Pricings.AddAsync(new Pricing
                    {
                        Title = "Basic",
                        Icon = "<i class='fas fa-battery-half'></i>",
                        Price = 22,
                        Per = "hour"
                    });

                    await db.Pricings.AddAsync(new Pricing
                    {
                        Title = "Pro",
                        Icon = "<i class='fas fa-battery-full'></i>",
                        Price = 48,
                        Per = "hour"
                    });

                    await db.SaveChangesAsync();
                }

                if (db.PricingDetails.Any() == false)
                {
                    await db.PricingDetails.AddAsync(new PricingDetail
                    {
                        Name = "Web Development"
                    });

                    await db.PricingDetails.AddAsync(new PricingDetail
                    {
                        Name = "Advetising"
                    });

                    await db.PricingDetails.AddAsync(new PricingDetail
                    {
                        Name = "Game Development"
                    });

                    await db.PricingDetails.AddAsync(new PricingDetail
                    {
                        Name = "Music Writing"
                    });

                    await db.PricingDetails.AddAsync(new PricingDetail
                    {
                        Name = "Photography"
                    });

                    await db.SaveChangesAsync();
                }

                if (db.PricingsPricingDetailsCollections.Any() != true)
                {
                    await db.PricingsPricingDetailsCollections.AddAsync(new PricingsPricingDetailsCollection
                    {
                        PricingId = 1,
                        PricingDetailId = 1,
                        Exists = true,
                        New = false
                    });

                    await db.PricingsPricingDetailsCollections.AddAsync(new PricingsPricingDetailsCollection
                    {
                        PricingId = 1,
                        PricingDetailId = 2,
                        Exists = true,
                        New = false
                    });

                    await db.PricingsPricingDetailsCollections.AddAsync(new PricingsPricingDetailsCollection
                    {
                        PricingId = 1,
                        PricingDetailId = 3,
                        Exists = true,
                        New = false
                    });

                    await db.PricingsPricingDetailsCollections.AddAsync(new PricingsPricingDetailsCollection
                    {
                        PricingId = 1,
                        PricingDetailId = 5,
                        Exists = false,
                        New = true
                    });

                    await db.PricingsPricingDetailsCollections.AddAsync(new PricingsPricingDetailsCollection
                    {
                        PricingId = 2,
                        PricingDetailId = 2,
                        Exists = false,
                        New = false
                    });

                    await db.PricingsPricingDetailsCollections.AddAsync(new PricingsPricingDetailsCollection
                    {
                        PricingId = 2,
                        PricingDetailId = 3,
                        Exists = false,
                        New = false
                    });

                    await db.PricingsPricingDetailsCollections.AddAsync(new PricingsPricingDetailsCollection
                    {
                        PricingId = 2,
                        PricingDetailId = 4,
                        Exists = false,
                        New = false
                    });

                    await db.PricingsPricingDetailsCollections.AddAsync(new PricingsPricingDetailsCollection
                    {
                        PricingId = 2,
                        PricingDetailId = 5,
                        Exists = true,
                        New = true
                    });

                    await db.SaveChangesAsync();
                }

                if (!db.Services.Any())
                {
                    await db.Services.AddAsync(new Service
                    {
                        Icon = "<i class='fas fa-code'></i>",
                        Title = "Web Development",
                        Description = "Modern and mobile-ready website that will help you reach all of your marketing."
                    });

                    await db.Services.AddAsync(new Service
                    {
                        Icon = "<i class='fas fa-music'></i>",
                        Title = "Music Writing",
                        Description = "Music copying, writing, creating, transcription, arranging and composition services."
                    });

                    await db.Services.AddAsync(new Service
                    {
                        Icon = "<i class='fab fa-penny-arcade'></i>",
                        Title = "Advetising",
                        Description = "Advertising services include television, radio, print, mail, and web apps."
                    });

                    await db.Services.AddAsync(new Service
                    {
                        Icon = "<i class='fas fa-gamepad'></i>",
                        Title = "Game Development",
                        Description = "Developing memorable and unique mobile android, ios and video games."
                    });

                    await db.SaveChangesAsync();
                }

                if (!db.FunFacts.Any())
                {
                    await db.FunFacts.AddAsync(new FunFact
                    {
                        Icon = "<i class='fas fa-music'></i>",
                        Name = "80 Albumes Listened"
                    });

                    await db.FunFacts.AddAsync(new FunFact
                    {
                        Icon = "<i class='fas fa-trophy'></i>",
                        Name = "15 Awards Won"
                    });

                    await db.FunFacts.AddAsync(new FunFact
                    {
                        Icon = "<i class='fas fa-mug-hot'></i>",
                        Name = "1 000 Cups of coffee"
                    });

                    await db.FunFacts.AddAsync(new FunFact
                    {
                        Icon = "<i class='fas fa-flag'></i>",
                        Name = "10 Countries Visited"
                    });

                    await db.SaveChangesAsync();
                }

                if (!db.Clients.Any())
                {
                    await db.Clients.AddAsync(new Client
                    {
                        ImagePath = "client-0d8c0e7e3bc94cfe9269e9e726ac2be0.png"
                    });

                    await db.Clients.AddAsync(new Client
                    {
                        ImagePath = "client-9b5905a0c9f742a7a2c1f2253e3e4361.png"
                    });

                    await db.Clients.AddAsync(new Client
                    {
                        ImagePath = "client-b66df26656224b6fad8e89f41ebbce6f.png"
                    });

                    await db.Clients.AddAsync(new Client
                    {
                        ImagePath = "client-1a323fd241cc4a2ca8ccd6aa8ec2219d.png"
                    });

                    await db.SaveChangesAsync();
                }

                if (!db.Quotes.Any())
                {
                    await db.Quotes.AddAsync(new Quote
                    {
                        FullName = "Ayaz Arabaçı",
                        Profession = "Şair",
                        Content = "<p>Sən məni ürəyində bir azca yaxşı qoru,</p><p>Yaralarım sağalıb üzünə duran deyil.&nbsp;</p><p>Kür kimi,Araz kimi təzədən qovuşmasaq,</p><p>Mənə elə gəlir ki,ömür qurtaran deyil..</p><p></p>",
                        ImagePath = "quote-9897b9dab27249acb56db91067d34114.jpg"
                    });

                    await db.SaveChangesAsync();
                }
                //---ABOUT---

                //---RESUME---
                if (!db.Testimonials.Any())
                {
                    await db.Testimonials.AddAsync(new Testimonial
                    {
                        FullName = "Helen Floyd",
                        Profession = "Art Director",
                        Content = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>",
                        ImagePath = "testimonial-a9354e963c8f40448bd0430be6611348.jpg"
                    });

                    await db.Testimonials.AddAsync(new Testimonial
                    {
                        FullName = "Robert Chase",
                        Profession = "CEO",
                        Content = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit.&nbsp;</p>",
                        ImagePath = "testimonial-35fd0c911004450fafa9198b03734ff0.jpg"
                    });

                    await db.Testimonials.AddAsync(new Testimonial
                    {
                        FullName = "Helen Floyd",
                        Profession = "Art Director",
                        Content = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit.&nbsp;</p>",
                        ImagePath = "testimonial-dd22702a80cf4f7985a3f70e31ca6c29.jpg"
                    });

                    await db.SaveChangesAsync();
                }

                if (db.Experiences.Any() == false)
                {
                    await db.Experiences.AddAsync(new Experience
                    {
                        Position = "Art Director",
                        Place = "Facebook Inc.",
                        Description = "Collaborate with creative and development teams on the execution of ideas.",
                        BeginYear = 2013
                    });

                    await db.Experiences.AddAsync(new Experience
                    {
                        Position = "Front-end Developer",
                        Place = "Google Inc.",
                        Description = "Monitored technical aspects of the front-end delivery for several projects.",
                        BeginYear = 2011,
                        EndYear = 2012
                    });

                    await db.Experiences.AddAsync(new Experience
                    {
                        Position = "Senior Developer",
                        Place = "Abc Inc.",
                        Description = "Optimize website performance using latest technology.",
                        BeginYear = 2009,
                        EndYear = 2010
                    });

                    await db.SaveChangesAsync();
                }

                if (db.Educations.Any() == false)
                {
                    await db.Educations.AddAsync(new Education
                    {
                        Position = "Art University",
                        Place = "New York",
                        Description = "Bachelor's Degree in Computer Science ABC Technical Institute, Jefferson, Missouri",
                        BeginYear = 2006,
                        EndYear = 2008
                    });

                    await db.Educations.AddAsync(new Education
                    {
                        Position = "Programming Course",
                        Place = "Paris",
                        Description = "Coursework - Git, WordPress, Javascript, iOS, Android.",
                        BeginYear = 2005,
                        EndYear = 2006
                    });

                    await db.Educations.AddAsync(new Education
                    {
                        Position = "Web Design Course",
                        Place = "London",
                        Description = "Converted Photoshop layouts to web pages using HTML, CSS, and JavaScript",
                        BeginYear = 2004,
                        EndYear = 2005
                    });

                    await db.SaveChangesAsync();
                }

                if (!db.Designs.Any())
                {
                    await db.Designs.AddAsync(new Design
                    {
                        Name = "Web Design",
                        Percent = 90
                    });

                    await db.Designs.AddAsync(new Design
                    {
                        Name = "Write Music",
                        Percent = 65
                    });

                    await db.Designs.AddAsync(new Design
                    {
                        Name = "Photoshop",
                        Percent = 75
                    });

                    await db.Designs.AddAsync(new Design
                    {
                        Name = "Graphic Design",
                        Percent = 85
                    });

                    await db.SaveChangesAsync();
                }

                if (!db.Languages.Any())
                {
                    await db.Languages.AddAsync(new Language
                    {
                        Name = "English",
                        Percent = 90
                    });

                    await db.Languages.AddAsync(new Language
                    {
                        Name = "German",
                        Percent = 60
                    });

                    await db.Languages.AddAsync(new Language
                    {
                        Name = "Italian",
                        Percent = 30
                    });

                    await db.Languages.AddAsync(new Language
                    {
                        Name = "French",
                        Percent = 70
                    });

                    await db.SaveChangesAsync();
                }

                if (db.Codings.Any() == false)
                {
                    await db.Codings.AddAsync(new Coding
                    {
                        Name = "WordPress",
                        Percent = 90
                    });

                    await db.Codings.AddAsync(new Coding
                    {
                        Name = "PHP / MYSQL",
                        Percent = 75
                    });

                    await db.Codings.AddAsync(new Coding
                    {
                        Name = "Angular / JavaScript",
                        Percent = 85
                    });

                    await db.Codings.AddAsync(new Coding
                    {
                        Name = "HTML / CSS",
                        Percent = 95
                    });

                    await db.SaveChangesAsync();
                }

                if (!db.Knowledges.Any())
                {
                    await db.Knowledges.AddAsync(new Knowledge
                    {
                        Name = "Website hosting"
                    });

                    await db.Knowledges.AddAsync(new Knowledge
                    {
                        Name = "iOS and android apps"
                    });

                    await db.Knowledges.AddAsync(new Knowledge
                    {
                        Name = "Create logo design"
                    });

                    await db.Knowledges.AddAsync(new Knowledge
                    {
                        Name = "Design for print"
                    });

                    await db.Knowledges.AddAsync(new Knowledge
                    {
                        Name = "Modern and mobile-ready"
                    });

                    await db.Knowledges.AddAsync(new Knowledge
                    {
                        Name = "Advertising services include"
                    });

                    await db.Knowledges.AddAsync(new Knowledge
                    {
                        Name = "Graphics and animations"
                    });

                    await db.Knowledges.AddAsync(new Knowledge
                    {
                        Name = "Search engine marketing"
                    });

                    await db.SaveChangesAsync();
                }
                //---RESUME---

                //---WORKS---
                if (db.Categories.Any() == false)
                {
                    await db.Categories.AddAsync(new Category
                    {
                        Name = "Front-End"
                    });

                    await db.Categories.AddAsync(new Category
                    {
                        Name = "Back-End"
                    });

                    await db.Categories.AddAsync(new Category
                    {
                        Name = "Web Design"
                    });

                    await db.Categories.AddAsync(new Category
                    {
                        Name = "Graphic Design"
                    });

                    await db.SaveChangesAsync();
                }

                if (db.Works.Any() == false)
                {
                    await db.Works.AddAsync(new Work
                    {
                        Title = "To-Do List",
                        ImagePath = "work-c052a6038bc24672b64c4ed5e8c160db.jpg",
                        CategoryId = 1
                    });

                    await db.Works.AddAsync(new Work
                    {
                        Title = "Cab Management System",
                        ImagePath = "work-586f627b3ea44ed18619d55c2d592f7d.png",
                        CategoryId = 2
                    });

                    await db.Works.AddAsync(new Work
                    {
                        Title = "Waves",
                        ImagePath = "work-a0189587e48345d69a65eca78f08a644.jpg",
                        CategoryId = 3
                    });

                    await db.Works.AddAsync(new Work
                    {
                        Title = "Mindful Joint",
                        ImagePath = "work-e4db03c808b24739b294ee35abc938dc.jpg",
                        CategoryId = 4
                    });

                    await db.Works.AddAsync(new Work
                    {
                        Title = "Steele",
                        ImagePath = "work-8870ab3ea0764aff80cbfc6f9ad05c85.png",
                        CategoryId = 3
                    });

                    await db.Works.AddAsync(new Work
                    {
                        Title = "Joystick",
                        ImagePath = "work-002cb7e2e67f4b7d8ef8de29cc18536a.jpg",
                        CategoryId = 4
                    });

                    await db.SaveChangesAsync();
                }
                //---WORKS---

                //---BLOG---
                if (!db.Tags.Any())
                {
                    await db.Tags.AddAsync(new Tag
                    {
                        Name = "Code"
                    });

                    await db.Tags.AddAsync(new Tag
                    {
                        Name = "HTML"
                    });

                    await db.Tags.AddAsync(new Tag
                    {
                        Name = "Plugin"
                    });

                    await db.Tags.AddAsync(new Tag
                    {
                        Name = "WordPress"
                    });

                    await db.SaveChangesAsync();
                }

                if (db.BlogCategories.Any() == false)
                {
                    await db.BlogCategories.AddAsync(new BlogCategory
                    {
                        Name = "Design"
                    });

                    await db.BlogCategories.AddAsync(new BlogCategory
                    {
                        Name = "Music"
                    });

                    await db.SaveChangesAsync();
                }

                if (!db.Blogs.Any())
                {
                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Code Academy Haqqında",
                        Description = "<p><strong>Code Academy</strong>-də keçirilən Proqramlaşdırma üzrə tədris proqramı sektorun ehtiyaclarını nəzərə alaraq hazırlanmışdır. Təhsilin 85%-i praktiki iş üzərində qurulmuşdur. Məqsəd sadəcə Proqramlaşdırmanın incəliklərini öyrətmək deyil, eyni zamanda müəssisələrin veb proqramlaşdırmaya olan ehtiyaclarını professional şəkildə qarşılayacaq mütəxəssislər yetişdirməkdir.</p>",
                        ImagePath = "blog-2989eb839d5d4179bfc0c0d5f81b9d2a.jpg"
                    });

                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Proqramlaşdırma",
                        Description = "<p><strong>Proqramlaşdırma</strong> – proqram yaratmaqla bağlı nəzəri və praktiki yaradıcılıq sahəsidir.</p><p>Proqramlarım hazırlanması ilə məşğul olan mütəxəssislərin əsas hissəsini proqramçılar təşkil edir. Proqramçılar yaradıcılıq xarakterinə və kvalifikasiya səviyyəsinə görə müxtəlifdir.</p>",
                        ImagePath = "blog-c4c384e5bfcd4023aad3d60e0da976a8.jpg"
                    });

                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Code Academy Haqqında",
                        Description = "<p><strong>Code Academy</strong>-də keçirilən Proqramlaşdırma üzrə tədris proqramı sektorun ehtiyaclarını nəzərə alaraq hazırlanmışdır. Təhsilin 85%-i praktiki iş üzərində qurulmuşdur. Məqsəd sadəcə Proqramlaşdırmanın incəliklərini öyrətmək deyil, eyni zamanda müəssisələrin veb proqramlaşdırmaya olan ehtiyaclarını professional şəkildə qarşılayacaq mütəxəssislər yetişdirməkdir.</p>",
                        ImagePath = "blog-2989eb839d5d4179bfc0c0d5f81b9d2a.jpg"
                    });

                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Proqramlaşdırma",
                        Description = "<p><strong>Proqramlaşdırma</strong> – proqram yaratmaqla bağlı nəzəri və praktiki yaradıcılıq sahəsidir.</p><p>Proqramlarım hazırlanması ilə məşğul olan mütəxəssislərin əsas hissəsini proqramçılar təşkil edir. Proqramçılar yaradıcılıq xarakterinə və kvalifikasiya səviyyəsinə görə müxtəlifdir.</p>",
                        ImagePath = "blog-c4c384e5bfcd4023aad3d60e0da976a8.jpg"
                    });

                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Code Academy Haqqında",
                        Description = "<p><strong>Code Academy</strong>-də keçirilən Proqramlaşdırma üzrə tədris proqramı sektorun ehtiyaclarını nəzərə alaraq hazırlanmışdır. Təhsilin 85%-i praktiki iş üzərində qurulmuşdur. Məqsəd sadəcə Proqramlaşdırmanın incəliklərini öyrətmək deyil, eyni zamanda müəssisələrin veb proqramlaşdırmaya olan ehtiyaclarını professional şəkildə qarşılayacaq mütəxəssislər yetişdirməkdir.</p>",
                        ImagePath = "blog-2989eb839d5d4179bfc0c0d5f81b9d2a.jpg"
                    });

                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Proqramlaşdırma",
                        Description = "<p><strong>Proqramlaşdırma</strong> – proqram yaratmaqla bağlı nəzəri və praktiki yaradıcılıq sahəsidir.</p><p>Proqramlarım hazırlanması ilə məşğul olan mütəxəssislərin əsas hissəsini proqramçılar təşkil edir. Proqramçılar yaradıcılıq xarakterinə və kvalifikasiya səviyyəsinə görə müxtəlifdir.</p>",
                        ImagePath = "blog-c4c384e5bfcd4023aad3d60e0da976a8.jpg"
                    });

                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Code Academy Haqqında",
                        Description = "<p><strong>Code Academy</strong>-də keçirilən Proqramlaşdırma üzrə tədris proqramı sektorun ehtiyaclarını nəzərə alaraq hazırlanmışdır. Təhsilin 85%-i praktiki iş üzərində qurulmuşdur. Məqsəd sadəcə Proqramlaşdırmanın incəliklərini öyrətmək deyil, eyni zamanda müəssisələrin veb proqramlaşdırmaya olan ehtiyaclarını professional şəkildə qarşılayacaq mütəxəssislər yetişdirməkdir.</p>",
                        ImagePath = "blog-2989eb839d5d4179bfc0c0d5f81b9d2a.jpg"
                    });

                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Proqramlaşdırma",
                        Description = "<p><strong>Proqramlaşdırma</strong> – proqram yaratmaqla bağlı nəzəri və praktiki yaradıcılıq sahəsidir.</p><p>Proqramlarım hazırlanması ilə məşğul olan mütəxəssislərin əsas hissəsini proqramçılar təşkil edir. Proqramçılar yaradıcılıq xarakterinə və kvalifikasiya səviyyəsinə görə müxtəlifdir.</p>",
                        ImagePath = "blog-c4c384e5bfcd4023aad3d60e0da976a8.jpg"
                    });

                    await db.SaveChangesAsync();
                }

                if (!db.BlogTagCategoryCollections.Any())
                {
                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 1,
                        TagId = 1,
                        BlogCategoryId = 1,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 1,
                        TagId = 2,
                        BlogCategoryId = 2,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 2,
                        TagId = 3,
                        BlogCategoryId = 1,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 2,
                        TagId = 3,
                        BlogCategoryId = 2,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 3,
                        TagId = 1,
                        BlogCategoryId = 1,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 3,
                        TagId = 2,
                        BlogCategoryId = 2,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 4,
                        TagId = 3,
                        BlogCategoryId = 1,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 4,
                        TagId = 4,
                        BlogCategoryId = 2,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 5,
                        TagId = 1,
                        BlogCategoryId = 1,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 5,
                        TagId = 2,
                        BlogCategoryId = 2,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 6,
                        TagId = 3,
                        BlogCategoryId = 1,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 6,
                        TagId = 4,
                        BlogCategoryId = 2,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 7,
                        TagId = 1,
                        BlogCategoryId = 1,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 7,
                        TagId = 2,
                        BlogCategoryId = 2,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 8,
                        TagId = 3,
                        BlogCategoryId = 1,
                        CreatedByUserId = 1
                    });

                    await db.BlogTagCategoryCollections.AddAsync(new BlogTagCategoryCollection
                    {
                        BlogId = 8,
                        TagId = 4,
                        BlogCategoryId = 2,
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                //---BLOG---

                //---CONTACT---
                if (!db.AppInfos.Any())
                {
                    await db.AppInfos.AddAsync(new AppInfo
                    {
                        Map = "<iframe src='https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d49288.475208603864!2d48.51164386995773!3d39.4573605698764!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x403cbb4ccea75261%3A0xeabe549d3a0adf85!2zQmlsyZlzdXZhciwgQXrJmXJiYXljYW4!5e0!3m2!1saz!2s!4v1641720936114!5m2!1saz!2s' width='600' height='450' style='border:0;' allowfullscreen='' loading='lazy'></iframe>",
                        Address = "Baku, Azerbaijan",
                        Email = "zakirer@code.edu.az",
                        PhoneNumber = "+994708911300",
                        IsFreelance = true
                    });

                    await db.SaveChangesAsync();
                }
                //---CONTACT---
            }

            return app;
        }
    }
}
