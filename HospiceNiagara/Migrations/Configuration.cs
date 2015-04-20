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

            if (!context.Roles.Any(r => r.Name == "Staff"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Staff"));
            }

            if (!context.Roles.Any(r => r.Name == "Leadership"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Leadership"));
            }

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Admin"));
            }

            if (!context.Roles.Any(r => r.Name == "Community"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Community"));
            }

            if (!context.Roles.Any(r => r.Name == "Outreach"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Outreach"));
            }

            if (!context.Roles.Any(r => r.Name == "Residential"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Residential"));
            }

            if (!context.Roles.Any(r => r.Name == "New Staff"))
            {
                var roleresult = roleManager.Create(new IdentityRole("New Staff"));
            }

            if (!context.Roles.Any(r => r.Name == "Board"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Board"));
            }

            if (!context.Roles.Any(r => r.Name == "Audit & Finance Committee"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Audit & Finance Committee"));
            }

            if (!context.Roles.Any(r => r.Name == "Community Relations Commitee"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Community Relations Committee"));
            }

            if (!context.Roles.Any(r => r.Name == "Operations & Quality Improvement Committee"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Operations & Quality Improvement Committee"));
            }

            if (!context.Roles.Any(r => r.Name == "New Board Members"))
            {
                var roleresult = roleManager.Create(new IdentityRole("New Board Members"));
            }

            if (!context.Roles.Any(r => r.Name == "Voluteers"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Volunteers"));
            }

            if (!context.Roles.Any(r => r.Name == "Bereavement"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Bereavement"));
            }

            if (!context.Roles.Any(r => r.Name == "Community"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Community"));
            }

            if (!context.Roles.Any(r => r.Name == "Day Hospice"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Day Hospice"));
            }

            if (!context.Roles.Any(r => r.Name == "Residential"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Residential"));
            }

            if (!context.Roles.Any(r => r.Name == "Welcome Desk"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Welcome Desk"));
            }

            if (!context.Roles.Any(r => r.Name == "Non-Client Event"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Non-Client Event"));
            }

            if (!context.Roles.Any(r => r.Name == "Non-Client Admin"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Non-Client Admin"));
            }

            if (!context.Roles.Any(r => r.Name == "New Volunteers"))
            {
                var roleresult = roleManager.Create(new IdentityRole("New Volunteers"));
            }

            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));


            //Now the Admin user named admin1 with password password
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

            var staffuser = new ApplicationUser
            {
                UserName = "staff1@outlook.com",
                Email = "staff1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "staff1@outlook.com"))
            {
                manager.Create(staffuser, "Password1");
                manager.AddToRole(staffuser.Id, "Staff");
            }

            var leadershipuser = new ApplicationUser
            {
                UserName = "leadership1@outlook.com",
                Email = "leadership1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "leadership1@outlook.com"))
            {
                manager.Create(leadershipuser, "Password1");
                manager.AddToRole(leadershipuser.Id, "Leadership");
            }

            var adminuser = new ApplicationUser
            {
                UserName = "admin1@outlook.com",
                Email = "admin1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "admin1@outlook.com"))
            {
                manager.Create(adminuser, "Password1");
                manager.AddToRole(adminuser.Id, "Admin");
            }

            var communityuser = new ApplicationUser
            {
                UserName = "community1@outlook.com",
                Email = "community1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "community1@outlook.com"))
            {
                manager.Create(communityuser, "Password1");
                manager.AddToRole(communityuser.Id, "Community");
            }

            var outreachuser = new ApplicationUser
            {
                UserName = "outreach1@outlook.com",
                Email = "outreach1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "outreach1@outlook.com"))
            {
                manager.Create(outreachuser, "Password1");
                manager.AddToRole(outreachuser.Id, "Outreach");
            }

            var residentialuser = new ApplicationUser
            {
                UserName = "residential1@outlook.com",
                Email = "residential1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "residential1@outlook.com"))
            {
                manager.Create(residentialuser, "Password1");
                manager.AddToRole(residentialuser.Id, "Residential");
            }

            var newstaffuser = new ApplicationUser
            {
                UserName = "newstaffuser1@outlook.com",
                Email = "newstaffuser1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "newstaffuser1@outlook.com"))
            {
                manager.Create(newstaffuser, "Password1");
                manager.AddToRole(newstaffuser.Id, "New Staff");
            }

            var welcomedeskuser = new ApplicationUser
            {
                UserName = "welcomedesk1@outlook.com",
                Email = "welcomedesk1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "welcomedesk1@outlook.com"))
            {
                manager.Create(welcomedeskuser, "Password1");
                manager.AddToRole(welcomedeskuser.Id, "Welcome Desk");
            }

            var boarduser = new ApplicationUser
            {
                UserName = "board1@outlook.com",
                Email = "board1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "board1@outlook.com"))
            {
                manager.Create(boarduser, "Password1");
                manager.AddToRole(boarduser.Id, "Board");
            }

            var auditfinanceuser = new ApplicationUser
            {
                UserName = "auditfinance1@outlook.com",
                Email = "auditfinance1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "auditfinance1@outlook.com"))
            {
                manager.Create(auditfinanceuser, "Password1");
                manager.AddToRole(auditfinanceuser.Id, "Audit & Finance Committee");
            }

            var communityrelationsuser = new ApplicationUser
            {
                UserName = "communityrelations1@outlook.com",
                Email = "communityrelations1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "communityrelations1@outlook.com"))
            {
                manager.Create(communityrelationsuser, "Password1");
                manager.AddToRole(communityrelationsuser.Id, "Community Relations Committee");
            }

            var operationsqualityuser = new ApplicationUser
            {
                UserName = "operationsquality1@outlook.com",
                Email = "operationsquality1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "operationsquality1@outlook.com"))
            {
                manager.Create(operationsqualityuser, "Password1");
                manager.AddToRole(operationsqualityuser.Id, "Operations & Quality Improvement Committee");
            }

            var newboardmemberuser = new ApplicationUser
            {
                UserName = "newboardmember1@outlook.com",
                Email = "newboardmember1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "newboardmember1@outlook.com"))
            {
                manager.Create(newboardmemberuser, "Password1");
                manager.AddToRole(newboardmemberuser.Id, "New Board Members");
            }

            var volunteeruser = new ApplicationUser
            {
                UserName = "volunteer1@outlook.com",
                Email = "volunteer1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "volunteer1@outlook.com"))
            {
                manager.Create(volunteeruser, "Password1");
                manager.AddToRole(volunteeruser.Id, "Volunteers");
            }

            var bereavementuser = new ApplicationUser
            {
                UserName = "bereavement1@outlook.com",
                Email = "bereavement1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "bereavement1@outlook.com"))
            {
                manager.Create(bereavementuser, "Password1");
                manager.AddToRole(bereavementuser.Id, "Bereavement");
            }

            var dayhospiceuser = new ApplicationUser
            {
                UserName = "dayhospice1@outlook.com",
                Email = "dayhospice1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "dayhospice1@outlook.com"))
            {
                manager.Create(dayhospiceuser, "Password1");
                manager.AddToRole(dayhospiceuser.Id, "Day Hospice");
            }

            var nonclientuser = new ApplicationUser
            {
                UserName = "nonclient1@outlook.com",
                Email = "nonclient1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "nonclient1@outlook.com"))
            {
                manager.Create(nonclientuser, "Password1");
                manager.AddToRole(nonclientuser.Id, "Non-Client Event");
            }

            var nonadminuser = new ApplicationUser
            {
                UserName = "nonadmin1@outlook.com",
                Email = "nonadmin1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "nonadmin1@outlook.com"))
            {
                manager.Create(nonadminuser, "Password1");
                manager.AddToRole(nonadminuser.Id, "Non-Client Admin");
            }

            var newvolunteeruser = new ApplicationUser
            {
                UserName = "newvolunteer1@outlook.com",
                Email = "newvolunteer1@outlook.com",
                UserDOB = DateTime.Today
            };
            if (!context.Users.Any(u => u.UserName == "newvolunteer1@outlook.com"))
            {
                manager.Create(newvolunteeruser, "Password1");
                manager.AddToRole(newvolunteeruser.Id, "New Volunteers");
            }

            var rs = new List<RoleList>
            {
                new RoleList { RoleName = "Community" },
                new RoleList { RoleName = "New Board Members" },
                new RoleList { RoleName = "Welcome Desk" },
                new RoleList { RoleName = "Outreach" },
                new RoleList { RoleName = "Non-Client Event" },
                new RoleList { RoleName = "Community Relations Committee" },
                new RoleList { RoleName = "New Volunteers" },
                new RoleList { RoleName = "Admin" },
                new RoleList { RoleName = "Day Hospice" },
                new RoleList { RoleName = "Staff" },
                new RoleList { RoleName = "Audit & Finance Committee" },
                new RoleList { RoleName = "Bereavement" },
                new RoleList { RoleName = "Board" },
                new RoleList { RoleName = "New Staff" },
                new RoleList { RoleName = "Administrator" },
                new RoleList { RoleName = "Non-Client Admin" },
                new RoleList { RoleName = "Operations & Quality Improvement Committee" },
                new RoleList { RoleName = "Leadership" },
                new RoleList { RoleName = "Residential" },
                new RoleList { RoleName = "Volunteers" },
            };
            rs.ForEach(r => context.RoleLists.AddOrUpdate(rn => rn.RoleName, r));
            context.SaveChanges();

        }
    }
}

