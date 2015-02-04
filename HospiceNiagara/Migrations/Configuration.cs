namespace HospiceNiagara.Migrations
{
    using HospiceNiagara.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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

            var boardContacts = new List<BoardContact>
            {
                new BoardContact{BoardContPosition = "Manager", BoardContHomeAddy = "1111 Homeland", BoardContWorkAddy = "1234 Work Address", BoardContHomePhone = "905-905-9005", BoardContWorkPhone = "905-111-2222", BoardContCellPhone = "905-222-3333", BoardContFaxNum = "905-444-2323", BoardContPartnerName = "Partner McWellington"},
                new BoardContact{BoardContPosition = "Admin", BoardContHomeAddy = "3452 Addressville", BoardContWorkAddy = "1010 Work Address", BoardContHomePhone = "905-666-3232", BoardContWorkPhone = "905-242-8177", BoardContCellPhone = "905-992-8282", BoardContFaxNum = "905-257-2325", BoardContPartnerName = "Patrick McPartnername"},
            };

            var deathnotices = new List<DeathNotice>
            {
                new DeathNotice{DnFirstName = "Steve", DnMiddleName = "Travis", DnLastName = "Deadguy", DnDate = DateTime.Parse("2014-03-22"), DnLocation = "Niagara Falls", DnNotes = "Heart attack"},
                new DeathNotice{DnFirstName = "Wally", DnMiddleName = "Megan", DnLastName = "Passedaway", DnDate = DateTime.Parse("2015-02-12"), DnLocation = "Niagara Falls", DnNotes = "Trampled by elephants"},
                new DeathNotice{DnFirstName = "Melinda", DnMiddleName = "Jorda", DnLastName = "Stabbedintheface", DnDate = DateTime.Parse("2013-12-13"), DnLocation = "Niagara Falls", DnNotes = "Ran into a jet engine"},
            };

            var fileSortTypes = new List<FileSortType>
            {
                new FileSortType{FileSrtDefintion = "To be sorted"},
                new FileSortType{FileSrtDefintion = "Don't worry about this one"},
            };

            var filestorages = new List<FileStorage>
            {
                new FileStorage{MimeType = "mimes", FileName = "Important files"},
                new FileStorage{MimeType = "mimes", FileName = "More important files"},
            };

            var jobDescriptions = new List<JobDescription>
            {
                new JobDescription{JobTitle = "Manager", JobDescpt = "Managing and maintaining work projects"},
                new JobDescription{JobTitle = "Admin", JobDescpt = "Administrating the Hospice Niagara network"},
                new JobDescription{JobTitle = "Volunteer", JobDescpt = "Selflessly spending your own time to aid the soon to depart"},
            };

            var meetingOrEvents = new List<MeetingOrEvent>
            {
                new MeetingOrEvent{EventTitle = "Office Bonanza", EventDiscription = "Celebrating your hard work over the years", EventStart = DateTime.Parse("2015-03-11"), EventEnd = DateTime.Parse("2015-03-12"), EventLocation = "Home office", EventRequirments = "Casual wear", EventLinks = "toteslegitwebsite.ca" },
                 new MeetingOrEvent{EventTitle = "Woodview Visitation", EventDiscription = "Assisting the elderly patron of the Woodview Retirement Home", EventStart = DateTime.Parse("2015-12-11"), EventEnd = DateTime.Parse("2015-12-11"), EventLocation = "Woodview Retirement Home", EventRequirments = "Formal wear", EventLinks = "toteslegitwebsite.ca" },
            };

            

        }
    }
}
