using LearnProgrammingTogether.Data.Enum;
using LearnProgrammingTogether.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace LearnProgrammingTogether.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Groups.Any())
                {
                    context.Groups.AddRange(new List<Group>()
                    {
                        new Group()
                        {
                            Title = "Group 1",
                            Image = "https://evecalls.com/img/downloads/imgonline-com-ua-resize-rte1tmufkkj.jpg",
                            Description = "This is the description of the first group",
                            GroupCategory = GroupCategory.TakeJob,
                            Adress = new Adress()
                            {
                                Street = "Тверская",
                                City = "Moscow",
                                Region = "MO",
                                Country = "Russia"
                            }
                         },
                        new Group()
                        {
                            Title = "Group 2",
                            Image = "https://miro.medium.com/max/1400/0*qQDxuFahBv3CWrdn.png",
                            Description = "This is the description of the second group",
                            GroupCategory = GroupCategory.Mentors,
                            Adress = new Adress()
                            {
                                Street = "Tverskaya",
                                City = "Moscow",
                                Region = "MO",
                                Country = "Russia"
                            }
                        },
                        new Group()
                        {
                            Title = "Group 3",
                            Image = "https://d1m75rqqgidzqn.cloudfront.net/wp-data/2018/02/11152216/blog-2.png",
                            Description = "This is the description of the third group",
                            GroupCategory = GroupCategory.StartLearn,
                            Adress = new Adress()
                            {
                                Street = "Tverskaya",
                                City = "Tver",
                                Region = "TO",
                                Country = "Russia"
                            }
                        },
                        new Group()
                        {
                            Title = "Group 3",
                            Image = "https://d1m75rqqgidzqn.cloudfront.net/wp-data/2018/02/11152216/blog-2.png",
                            Description = "This is the description of the third club",
                            GroupCategory = GroupCategory.StartLearn,
                            Adress = new Adress()
                            {
                                Street = "Tverskaya",
                                City = "Tver",
                                Region = "TC",
                                Country = "Russia"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //TechnologyController
                if (!context.Technologies.Any())
                {
                    context.Technologies.AddRange(new List<Technology>()
                    {
                        new Technology()
                        {
                            Title = "TechnologyController 1",
                            Image = "https://joprblob.azureedge.net/site/blog/22b4b589-fecd-40d9-a810-2bd491448e89/e4c77aee-8214-418b-a420-2a9284aa9212.png",
                            Description = "This is the description of the first TechnologyController",
                            TechnologyCategory = TechnologyCategory.Linq,
                            Adress = new Adress()
                            {
                                Street = "Tverskaya",
                                City = "Moscow",
                                Region = "MO",
                                Country = "Russia"
                            }
                        },
                        new Technology()
                        {
                            Title = "TechnologyController 2",
                            Image = "https://www.educationmesd.com/wp-content/uploads/2021/01/aspnet-featured.png",
                            Description = "This is the description of the second TechnologyController",
                            TechnologyCategory = TechnologyCategory.ASP,
                            AdressId = 5,
                            Adress = new Adress()
                            {
                                Street = "Tverskaya",
                                City = "Moscow",
                                Region = "MO",
                                Country = "Russia"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "santiniel@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        NickName = "santiniel",
                        StudyLang = "C#",
                        TypeFramework = ".Net 6.0",
                        Level = "Profi",
                        UserName = "Pavel",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Adress = new Adress()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            Region = "NC",
                            Country = "Россия"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        NickName = "app-user",
                        UserName = "app-user",
                        StudyLang = "C#",
                        TypeFramework = ".Net 6.0",
                        Level = "Noob",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Adress = new Adress()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            Region = "NC",
                            Country = "Russia"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
