using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class RoleList 
    {
        public int ID { get; set; }

        [Required(ErrorMessage="Role name is required")]
        [StringLength(50, ErrorMessage = "Role name can not exceed 50 characters")]
        public string RoleName { get; set; }
        
        public virtual ICollection<Announcement> Announcements { get; set; }

        public virtual ICollection<FileStorage> FileStores { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }

        public virtual ICollection<SchedType> ScheduleTypes { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }

        //public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        

        
    }
}