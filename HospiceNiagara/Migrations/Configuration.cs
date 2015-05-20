namespace HospiceNiagara.Migrations
{
    using HospiceNiagara.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //the Admin role
            var roleManager = new RoleManager<IdentityRole>(new
                                          RoleStore<IdentityRole>(context));
            //Create Role Admin if it does not exist
            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Administrator"));

            }

            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            context.SaveChanges();

        }
    }
}

