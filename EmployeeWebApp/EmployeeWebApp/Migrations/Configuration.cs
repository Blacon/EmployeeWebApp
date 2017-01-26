namespace EmployeeWebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Services;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeWebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "EmployeeWebApp.Models.ApplicationDbContext";
        }

        protected override void Seed(EmployeeWebApp.Models.ApplicationDbContext context)
        {
            //Cheating here a bit. Adding admin and manager users.
            //Admin login = potato@gmail.com and passwors = jerryispotato
            //Manager login = carrot@gmail.com and passwors = jamesiscarrot
            if (!context.Users.Any(u => u.UserName == "potato@gmail.com"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var service = new EmployeeService(context);

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Administrator" });
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Manager" });
                context.SaveChanges();


                var user = new ApplicationUser { UserName = "potato@gmail.com", Email = "potato@gmail.com" };
                userManager.Create(user, "jerryispotato");
                service.CreateEmployee("Jerry", "Potato", user.Id, null);
                userManager.AddToRole(user.Id, "Administrator");

                var user1 = new ApplicationUser { UserName = "carrot@gmail.com", Email = "carrot@gmail.com" };
                userManager.Create(user1, "jamesiscarrot");
                service.CreateEmployee("James", "Carrot", user1.Id, null);
                userManager.AddToRole(user1.Id, "Manager");
            }
        }
    }
}
