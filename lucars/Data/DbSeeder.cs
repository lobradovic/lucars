using lucars.Constants;
using lucars.Models;
using Microsoft.AspNetCore.Identity;

namespace lucars.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var user = new ApplicationUser
            {
                UserName = "admin@lucars.com",
                Email = "admin@lucars.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user, "Admin123.");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }


            var mod = new ApplicationUser
            {
                UserName = "moderator@lucars.com",
                Email = "moderator@lucars.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var modInDb = await userManager.FindByEmailAsync(mod.Email);

            if (modInDb == null)
            {
                await userManager.CreateAsync(mod, "Moderator123.");
                await userManager.AddToRoleAsync(mod, Roles.Moderator.ToString());
            }

        }


        public static async Task SeedMarkasAsync(ApplicationDbContext context)
        {
            if (!context.Markas.Any())
            {
                var marke = new List<Marka>()
                {
                    //glavne kategorije
                    new Marka() {nazivMarke = "Audi"},
                    new Marka() {nazivMarke = "BMW"},
                    new Marka() {nazivMarke = "Volkswagen"},
                    new Marka() {nazivMarke = "Mercedes-Benz"}
                };
                await context.AddRangeAsync(marke);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedKlasasAsync(ApplicationDbContext context)
        {
            if (!context.Klasas.Any())
            {
                var klase = new List<Klasa>()
                {
                    new Klasa(){nazivKlase="Mikro"},
                    new Klasa(){nazivKlase="Mini"},
                    new Klasa(){nazivKlase="Bussiness"},
                    new Klasa(){nazivKlase="Luxury"},
                    new Klasa(){nazivKlase="Family"}

                };
                await context.AddRangeAsync(klase);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedModelsAsync(ApplicationDbContext context)
        {
            if (!context.Models.Any())
            {
                var modeli = new List<Model>()
                {
                    new Model(){nazivModela="A4",idMarka=1},
                    new Model(){nazivModela="A6",idMarka=1},
                    new Model(){nazivModela="320d",idMarka=2},
                    new Model(){nazivModela="530d",idMarka=2},
                    new Model(){nazivModela="Golf 6",idMarka=3},
                    new Model(){nazivModela="Passat B8",idMarka=3},
                    new Model(){nazivModela="C 220",idMarka=4},
                    new Model(){nazivModela="CLS 350",idMarka=4},
                    new Model(){nazivModela="UP!",idMarka=3}
                };
                await context.AddRangeAsync(modeli);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedAutomobilsAsync(ApplicationDbContext context)
        {
            if (!context.Automobils.Any())
            {
                var automobili = new List<Automobil>()
                {
                    new Automobil(){godinaProizvodnje=2018,registrovanDo=DateTime.SpecifyKind(DateTime.Parse("2025-07-08"),DateTimeKind.Utc),cena=80.00m,idModel=6,idKlasa=5},
                    new Automobil(){godinaProizvodnje=2015,registrovanDo=DateTime.SpecifyKind(DateTime.Parse("2025-08-13"),DateTimeKind.Utc),cena=160.00m,idModel=8,idKlasa=4},
                    new Automobil(){godinaProizvodnje=2009,registrovanDo=DateTime.SpecifyKind(DateTime.Parse("2025-05-22"),DateTimeKind.Utc),cena=70.00m,idModel=1,idKlasa=5},
                    new Automobil(){godinaProizvodnje=2020,registrovanDo=DateTime.SpecifyKind(DateTime.Parse("2025-04-05"),DateTimeKind.Utc),cena=80.00m,idModel=2,idKlasa=3},
                    new Automobil(){godinaProizvodnje=2021,registrovanDo=DateTime.SpecifyKind(DateTime.Parse("2025-09-13"),DateTimeKind.Utc),cena=120.00m,idModel=4,idKlasa=3},
                    new Automobil(){godinaProizvodnje=2014,registrovanDo=DateTime.SpecifyKind(DateTime.Parse("2025-08-12"),DateTimeKind.Utc),cena=40.00m,idModel=9,idKlasa=1},
                    new Automobil(){godinaProizvodnje=2011,registrovanDo=DateTime.SpecifyKind(DateTime.Parse("2025-06-07"),DateTimeKind.Utc),cena=60.00m,idModel=5,idKlasa=2},
                    new Automobil(){godinaProizvodnje=2017,registrovanDo=DateTime.SpecifyKind(DateTime.Parse("2025-05-22"),DateTimeKind.Utc),cena=80.00m,idModel=3,idKlasa=5},
                    new Automobil(){godinaProizvodnje=2013,registrovanDo=DateTime.SpecifyKind(DateTime.Parse("2025-10-21"),DateTimeKind.Utc),cena=70.00m,idModel=7,idKlasa=5}
                };
                await context.AddRangeAsync(automobili);
                await context.SaveChangesAsync();
            }
        }
    }
}