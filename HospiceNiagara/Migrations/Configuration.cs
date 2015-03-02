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

            //for (int i = 1; i <= 4; i++)
            //{
            //    var user = new ApplicationUser
            //    {
            //        UserName = string.Format("user{0}@outlook.com", i.ToString()),
            //        Email = string.Format("user{0}@outlook.com", i.ToString())
            //    };
            //    if (!context.Users.Any(u => u.UserName == user.UserName))
            //        manager.Create(user, "password");
            //}

           
            //Now the Admin user named admin1 with password Password1
            var administratoruser = new ApplicationUser
            {
                UserName = "administrator1@outlook.com",
                Email = "administrator1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "administrator1@outlook.com"))
            {
                manager.Create(administratoruser, "password");
                manager.AddToRole(administratoruser.Id, "Administrator");
            }

            var staffuser = new ApplicationUser
            {
                UserName = "staff1@outlook.com",
                Email = "staff1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "staff1@outlook.com"))
            {
                manager.Create(staffuser, "password");
                manager.AddToRole(staffuser.Id, "Staff");
            }

            var leadershipuser = new ApplicationUser
            {
                UserName = "leadership1@outlook.com",
                Email = "leadership1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "leadership1@outlook.com"))
            {
                manager.Create(leadershipuser, "password");
                manager.AddToRole(leadershipuser.Id, "Leadership");
            }

            var adminuser = new ApplicationUser
            {
                UserName = "admin1@outlook.com",
                Email = "admin1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "admin1@outlook.com"))
            {
                manager.Create(adminuser, "password");
                manager.AddToRole(adminuser.Id, "Admin");
            }

            var communityuser = new ApplicationUser
            {
                UserName = "community1@outlook.com",
                Email = "community1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "community1@outlook.com"))
            {
                manager.Create(communityuser, "password");
                manager.AddToRole(communityuser.Id, "Community");
            }

            var outreachuser = new ApplicationUser
            {
                UserName = "outreach1@outlook.com",
                Email = "outreach1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "outreach1@outlook.com"))
            {
                manager.Create(outreachuser, "password");
                manager.AddToRole(outreachuser.Id, "Outreach");
            }

            var residentialuser = new ApplicationUser
            {
                UserName = "residential1@outlook.com",
                Email = "residential1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "residential1@outlook.com"))
            {
                manager.Create(residentialuser, "password");
                manager.AddToRole(residentialuser.Id, "Residential");
            }

            var newstaffuser = new ApplicationUser
            {
                UserName = "newstaffuser1@outlook.com",
                Email = "newstaffuser1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "newstaffuser1@outlook.com"))
            {
                manager.Create(newstaffuser, "password");
                manager.AddToRole(newstaffuser.Id, "New Staff");
            }

            var welcomedeskuser = new ApplicationUser
            {
                UserName = "welcomedesk1@outlook.com",
                Email = "welcomedesk1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "welcomedesk1@outlook.com"))
            {
                manager.Create(welcomedeskuser, "password");
                manager.AddToRole(welcomedeskuser.Id, "Welcome Desk");
            }

            var boarduser = new ApplicationUser
            {
                UserName = "board1@outlook.com",
                Email = "board1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "board1@outlook.com"))
            {
                manager.Create(boarduser, "password");
                manager.AddToRole(boarduser.Id, "Board");
            }

            var auditfinanceuser = new ApplicationUser
            {
                UserName = "auditfinance1@outlook.com",
                Email = "auditfinance1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "auditfinance1@outlook.com"))
            {
                manager.Create(auditfinanceuser, "password");
                manager.AddToRole(auditfinanceuser.Id, "Audit & Finance Committee");
            }

            var communityrelationsuser = new ApplicationUser
            {
                UserName = "communityrelations1@outlook.com",
                Email = "communityrelations1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "communityrelations1@outlook.com"))
            {
                manager.Create(communityrelationsuser, "password");
                manager.AddToRole(communityrelationsuser.Id, "Community Relations Committee");
            }

            var operationsqualityuser = new ApplicationUser
            {
                UserName = "operationsquality1@outlook.com",
                Email = "operationsquality1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "operationsquality1@outlook.com"))
            {
                manager.Create(operationsqualityuser, "password");
                manager.AddToRole(operationsqualityuser.Id, "Operations & Quality Improvement Committee");
            }

            var newboardmemberuser = new ApplicationUser
            {
                UserName = "newboardmember1@outlook.com",
                Email = "newboardmember1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "newboardmember1@outlook.com"))
            {
                manager.Create(newboardmemberuser, "password");
                manager.AddToRole(newboardmemberuser.Id, "New Board Members");
            }

            var volunteeruser = new ApplicationUser
            {
                UserName = "volunteer1@outlook.com",
                Email = "volunteer1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "volunteer1@outlook.com"))
            {
                manager.Create(volunteeruser, "password");
                manager.AddToRole(volunteeruser.Id, "Volunteers");
            }

            var bereavementuser = new ApplicationUser
            {
                UserName = "bereavement1@outlook.com",
                Email = "bereavement1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "bereavement1@outlook.com"))
            {
                manager.Create(bereavementuser, "password");
                manager.AddToRole(bereavementuser.Id, "Bereavement");
            }

            var dayhospiceuser = new ApplicationUser
            {
                UserName = "dayhospice1@outlook.com",
                Email = "dayhospice1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "dayhospice1@outlook.com"))
            {
                manager.Create(dayhospiceuser, "password");
                manager.AddToRole(dayhospiceuser.Id, "Day Hospice");
            }

            var nonclientuser = new ApplicationUser
            {
                UserName = "nonclient1@outlook.com",
                Email = "nonclient1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "nonclient1@outlook.com"))
            {
                manager.Create(nonclientuser, "password");
                manager.AddToRole(nonclientuser.Id, "Non-Client Event");
            }

            var nonadminuser = new ApplicationUser
            {
                UserName = "nonadmin1@outlook.com",
                Email = "nonadmin1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "nonadmin1@outlook.com"))
            {
                manager.Create(nonadminuser, "password");
                manager.AddToRole(nonadminuser.Id, "Non-Client Admin");
            }

            var newvolunteeruser = new ApplicationUser
            {
                UserName = "newvolunteer1@outlook.com",
                Email = "newvolunteer1@outlook.com"
            };
            if (!context.Users.Any(u => u.UserName == "newvolunteer1@outlook.com"))
            {
                manager.Create(newvolunteeruser, "password");
                manager.AddToRole(newvolunteeruser.Id, "New Volunteers");
            }
            

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
           /*
            var announcements = new List<Announcement>
            {
                new Announcement{AnnounceText = "This is the 1st test", AnnounceEndDate = DateTime.Parse("2014-12-31")},
                new Announcement{AnnounceText = "This is the 2nd test", AnnounceEndDate = DateTime.Parse("2028-12-31")},
            };
            announcements.ForEach(a => context.Announcements.AddOrUpdate(aa => aa.AnnounceText));
            SaveChanges(context);

            var boardContacts = new List<BoardContact>
            {
                new BoardContact{BoardContPosition = "Manager", BoardContHomeAddy = "1111 Homeland", BoardContWorkAddy = "1234 Work Address", BoardContHomePhone = "905-905-9005", BoardContWorkPhone = "905-111-2222", BoardContCellPhone = "905-222-3333", BoardContFaxNum = "905-444-2323", BoardContPartnerName = "Partner McWellington"},
                new BoardContact{BoardContPosition = "Admin", BoardContHomeAddy = "3452 Addressville", BoardContWorkAddy = "1010 Work Address", BoardContHomePhone = "905-666-3232", BoardContWorkPhone = "905-242-8177", BoardContCellPhone = "905-992-8282", BoardContFaxNum = "905-257-2325", BoardContPartnerName = "Patrick McPartnername"},
            };
            boardContacts.ForEach(bc => context.BoardContacts.AddOrUpdate(b => b.BoardContPosition));
            SaveChanges(context);

            var deathnotices = new List<DeathNotice>
            {
                new DeathNotice{DnFirstName = "Steve", DnMiddleName = "Travis", DnLastName = "Doe", DnDate = DateTime.Parse("2014-03-22"), DnLocation = "Niagara Falls", DnNotes = "Heart attack"},
                new DeathNotice{DnFirstName = "Wally", DnMiddleName = "Megan", DnLastName = "Johnson", DnDate = DateTime.Parse("2015-02-12"), DnLocation = "Niagara Falls", DnNotes = "Room 4"},
                new DeathNotice{DnFirstName = "Melinda", DnMiddleName = "Jorda", DnLastName = "Smith", DnDate = DateTime.Parse("2013-12-13"), DnLocation = "Niagara Falls", DnNotes = ""},
            };
            deathnotices.ForEach(dn => context.DeathNotices.AddOrUpdate(d => d.DnFirstName));
            SaveChanges(context);

            var fileSortTypes = new List<FileSortType>
            {
                new FileSortType{FileSrtDefintion = "To be sorted"},
                new FileSortType{FileSrtDefintion = "Don't worry about this one"},
            };
            fileSortTypes.ForEach(fst => context.FileSortTypes.AddOrUpdate(fs => fs.FileSrtDefintion));
            SaveChanges(context);

            var filestorages = new List<FileStorage>
            {
                new FileStorage{MimeType = "mimes", FileName = "Important files"},
                new FileStorage{MimeType = "mimes", FileName = "More important files"},
            };
            filestorages.ForEach(fs => context.FileStorages.AddOrUpdate(f => f.FileName));
            SaveChanges(context);

            var jobDescriptions = new List<JobDescription>
            {
                new JobDescription{JobTitle = "Manager", JobDescpt = "Managing and maintaining work projects"},
                new JobDescription{JobTitle = "Admin", JobDescpt = "Administrating the Hospice Niagara network"},
                new JobDescription{JobTitle = "Volunteer", JobDescpt = "Selflessly spending your own time to aid the soon to depart"},
            };
            jobDescriptions.ForEach(jd => context.JobDescriptions.AddOrUpdate(j => j.JobTitle));
            SaveChanges(context);

            var meetingOrEvents = new List<MeetingOrEvent>
            {
                new MeetingOrEvent{EventTitle = "Office Bonanza", EventDiscription = "Celebrating your hard work over the years", EventStart = DateTime.Parse("2015-03-11"), EventEnd = DateTime.Parse("2015-03-12"), EventLocation = "Home office", EventRequirments = "Casual wear", EventLinks = "toteslegitwebsite.ca" },
                 new MeetingOrEvent{EventTitle = "Woodview Visitation", EventDiscription = "Assisting the elderly patron of the Woodview Retirement Home", EventStart = DateTime.Parse("2015-12-11"), EventEnd = DateTime.Parse("2015-12-11"), EventLocation = "Woodview Retirement Home", EventRequirments = "Formal wear", EventLinks = "toteslegitwebsite.ca" },
            };
            meetingOrEvents.ForEach(me => context.MeetingOrEvents.AddOrUpdate(m => m.EventTitle));
            SaveChanges(context);
        }
         */    
/*private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
            catch (Exception e)
            {
                throw new Exception(
                     "Seed Failed - errors follow:\n" +
                     e.InnerException.InnerException.Message.ToString(), e
                 ); // Add the original exception as the innerException
            }*/
        }
            

        } 
    }

