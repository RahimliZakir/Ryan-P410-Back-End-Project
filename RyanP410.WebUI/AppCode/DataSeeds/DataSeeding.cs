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
        public static async Task<IApplicationBuilder> SeedData(this IApplicationBuilder app)
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
