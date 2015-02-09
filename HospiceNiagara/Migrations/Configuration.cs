namespace HospiceNiagara.Migrations
{
    using HospiceNiagara.Models;
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
             
        private void SaveChanges(DbContext context)
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
            }
        }
            

        }
    }

