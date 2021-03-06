﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;

//Paul Boyko Feb 2015

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
        [Display(Name = "First Name")]
        public string UserFName { get; set; }

        [Display(Name = "Middle Name")]
        public string UserMName { get; set; }

        [Display(Name = "Last Name")]
        public string UserLName { get; set; }

        [Display(Name="Name")]
        public string UserFullName
        {
            get
            {
                return this.UserFName + (string.IsNullOrEmpty(this.UserMName) ? " " : (" " + (char?)this.UserMName[0] + ". ").ToUpper()) + this.UserLName;
            }
        }

        [Display(Name = "Date of Birth")]
        public Nullable<DateTime> UserDOB { get; set; }

        [Display(Name = "Start Date")]
        public Nullable<DateTime> StartDate { get; set; }

        [Display(Name = "Last Login")]
        public Nullable<DateTime> LastLogin { get; set; }

        [Display(Name = "Date of Birth")]
        public Nullable<DateTime> LoginTime
        {
            get
            {
                if (this.LastLogin.HasValue)
                {
                    return this.LastLogin.Value.AddHours(3);
                }
                else return null;

            }
        }

        [Display(Name = "Date of Birth")]
        public string UserDOBString
        {
            get
            {
                if (this.UserDOB.HasValue)
                {
                    return this.UserDOB.Value.ToLongDateString();
                }
                else return null;
            }
        }

        [Display(Name = "Address")]
        public string UserAddress { get; set; }
        //public virtual ICollection<JobDescription> JobDescriptions { get; set; }
        //public virtual ICollection<Announcement> UserAnnouncements { get; set; }
        //public virtual ICollection<RoleList> RoleLists { get; set; }

        [Display(Name = "Password Changed From Default")]
        public bool PasswordChanged { get; set; }

        public virtual ICollection<MeetingUserRSVP> MeetingUserRSVPs { get; set; }


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
        public DbSet<DeathNotice> DeathNotices { get; set; }
        public DbSet<FileStorage> FileStorages { get; set; }       
        public DbSet<Meeting> Meetings { get; set; }      
        public DbSet<RoleList> RoleLists { get; set; }
        public DbSet<FileCat> FileCats { get; set; }
        public DbSet<FileSubCat> FileSubCats { get; set; }
        public DbSet<SchedType> SchedTypes { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<IdentityUserRole> IdentUserRoles { get; set; }
        
        
        
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ApplicationUser>().ToTable("IdentityUser");
        }

        public System.Data.Entity.DbSet<HospiceNiagara.Models.Schedule> Schedules { get; set; }

        public System.Data.Entity.DbSet<HospiceNiagara.Models.MeetingUserRSVP> MeetingUserRSVPs { get; set; }


        public System.Data.Entity.DbSet<HospiceNiagara.Models.DeathNoticePoems> DeathNoticePoems { get; set; }

        public System.Data.Entity.DbSet<HospiceNiagara.Models.WelcomeNotice> WelcomeNotices { get; set; }

        //public System.Data.Entity.DbSet<HospiceNiagara.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<HospiceNiagara.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<HospiceNiagara.Models.ApplicationUser> ApplicationUsers { get; set; }

      

              
    }
}
