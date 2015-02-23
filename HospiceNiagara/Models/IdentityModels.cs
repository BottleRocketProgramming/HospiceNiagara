using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;

namespace HospiceNiagara.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string UserFName { get; set; }
        public string UserMName { get; set; }
        public string UserLName { get; set; }
        public DateTime? UserDOB { get; set; }
        public string UserAddress { get; set; }
        public virtual ICollection<UserEventRSVP> UserEventRSVPs { get; set; }
        public virtual ICollection<UserJobDesc> UserJobDescs { get; set; }
        public virtual ICollection<UserAnnouncement> UserAnnouncements { get; set; }


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("HospiceCFEntities", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementUserRole> AnnouncementUserRoles { get; set; }
        public DbSet<BoardContact> BoardContacts { get; set; }
        public DbSet<DeathNotice> DeathNotices { get; set; }
        public DbSet<FileStorage> FileStorages { get; set; }
        public DbSet<FileStoreUserRole> FileStoreUserRoles { get; set; }
        public DbSet<FileSortType> FileSortTypes { get; set; }
        public DbSet<JobDescription> JobDescriptions { get; set; }
        public DbSet<MeetingOrEvent> MeetingOrEvents { get; set; }
        public DbSet<MeetingOrEventUserRole> MeetingOrEventUserRoles { get; set; }
        public DbSet<UserEventRSVP> UserEventRSVPs { get; set; }
        public DbSet<UserJobDesc> UserJobDesc { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
