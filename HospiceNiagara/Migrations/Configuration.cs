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

            //Create Role Staff if it does not exist
            if (!context.Roles.Any(r => r.Name == "Staff"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Staff"));
            }

            //Create Role Volunteer if it does not exist
            if (!context.Roles.Any(r => r.Name == "Volunteer"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Volunteer"));
            }

            //Create Role Board if it does not exist
            if (!context.Roles.Any(r => r.Name == "Board"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Board"));
            }

            //Create Perm Remove Records if it does not exist
            if (!context.Roles.Any(r => r.Name == "Remove Records"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Remove Records"));
            }

            //Create Perm Remove Records if it does not exist
            if (!context.Roles.Any(r => r.Name == "Create/Modify Death Notices"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Create/Modify Death Notices"));
            }
            
            //Create Perm Create/Modify Meetings or Events if it does not exist
            if (!context.Roles.Any(r => r.Name == "Create/Modify Meetings or Events"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Create/Modify Meetings or Events"));
            }

            //Create Perm Create/Modify Contacts if it does not exist
            if (!context.Roles.Any(r => r.Name == "Create/Modify Contacts"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Create/Modify Contacts"));
            }

            //Create Perm View Admin Info if it does not exist
            if (!context.Roles.Any(r => r.Name == "View Admin Info"))
            {
                var roleresult = roleManager.Create(new IdentityRole("View Admin Info"));
            }

            //Create Perm Manage Users if it does not exist
            if (!context.Roles.Any(r => r.Name == "Manage Users"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Manage Users"));
            }

            //Create Perm Create/Modify Announcements if it does not exist
            if (!context.Roles.Any(r => r.Name == "Create/Modify Announcements"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Create/Modify Announcements"));
            }

            //Create Perm Upload Resources if it does not exist
            if (!context.Roles.Any(r => r.Name == "Upload Resources"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Upload Resources"));
            }

            //Create Perm Create/Modify Schedules if it does not exist
            if (!context.Roles.Any(r => r.Name == "Create/Modify Schedules"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Create/Modify Schedules"));
            }

            //Create Perm Manage Invitations if it does not exist
            if (!context.Roles.Any(r => r.Name == "Manage Invitations"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Manage Invitations"));
            }

            //Create Assign User Permissions if it does not exist
            if (!context.Roles.Any(r => r.Name == "Assign User Permissions"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Assign User Permissions"));
            }

            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            //Add an administrator User when the system is created
            var administratoruser = new ApplicationUser
            {
                UserName = "administrator1@outlook.com",
                Email = "administrator1@outlook.com",
                UserDOB = DateTime.Today
            };

            if (!context.Users.Any(u => u.UserName == "administrator1@outlook.com"))
            {
                manager.Create(administratoruser, "Password1");
                manager.AddToRole(administratoruser.Id, "Administrator");
            }
            context.SaveChanges();
        }
    }
}

