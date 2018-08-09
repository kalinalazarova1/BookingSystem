using BookingSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Data
{
    public class Seeder
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        private BookingContext ctx;
        private Guid[] userIds;

        public Seeder(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, BookingContext ctx)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.ctx = ctx;
            this.userIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
        }

        public async Task Seed()
        {
            await SeedUser("kalina.lazarova@gmail.com", 0);
            await SeedUser("peter.ivanov@gmail.com", 1);
            await SeedSites();
        }

        private async Task SeedUser(string email, int index)
        {
            var user = await userManager.FindByNameAsync(email);

            if (user == null)
            {
                if (!(await roleManager.RoleExistsAsync("Admin")))
                {
                    var role = new IdentityRole("Admin");
                    await roleManager.CreateAsync(role);
                }

                user = new AppUser()
                {
                    UserName = email,
                    Email = email,
                    Id = this.userIds[index].ToString()
                };

                var userResult = await userManager.CreateAsync(user, "Kalina1@");
                var roleResult = await userManager.AddToRoleAsync(user, "Admin");

                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }
        }

        private async Task SeedSites()
        {
            var countries = new List<Country>
                {
                    new Country { Name = "Spain" },
                    new Country { Name = "Italy" },
                    new Country { Name = "USA" },
                    new Country { Name = "United Kingdom" },
                };

            if (!ctx.Countries.Any())
            {
                ctx.AddRange(new List<Currency>
                {
                    new Currency { Name = "Pound Sterling", Code = "GBP", Symbol = "£" },
                    new Currency { Name = "Euro", Code = "EUR", Symbol = "€" },
                    new Currency { Name = "US Dollar", Code = "USD", Symbol = "$" }
                });

                ctx.AddRange(countries);

                await ctx.SaveChangesAsync();
            }

            if (!ctx.Sites.Any())
            {
                var spain = ctx.Countries.First(c => c.Name == countries[0].Name);
                var uk = ctx.Countries.First(c => c.Name == countries[3].Name);
                var usa = ctx.Countries.First(c => c.Name == countries[2].Name);

                var sites = new List<Site>
                {
                    new Site
                    {
                        OwnerId = this.userIds[1].ToString(),
                        Location = new Location { Address = "Somewhere hot", Country = spain, SiteId = 1, TownCity = "Malaga" }
                    },
                    new Site
                    {
                        OwnerId = this.userIds[0].ToString(),
                        Location = new Location { Address = "10 Downing Street", Country = uk, SiteId = 2, TownCity = "London" }
                    },
                    new Site
                    {
                        OwnerId = this.userIds[0].ToString(),
                        Location = new Location { Address = "79 Arthur Street", Country = uk, SiteId = 3, TownCity = "Kenilworth" }
                    },
                    new Site
                    {
                        OwnerId = this.userIds[1].ToString(),
                        Location = new Location { Address = "1234 Sunset Boulevard", Country = usa, SiteId = 4, TownCity = "Los Angelos" }
                    }
                };

                ctx.AddRange(sites);

                await ctx.SaveChangesAsync();
            }
        }
    }
}
