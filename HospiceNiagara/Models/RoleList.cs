using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class RoleList 
    {
        public int ID { get; set; }

        public string RoleName { get; set; }
        
        public virtual ICollection<Announcement> Announcements { get; set; }

        public virtual ICollection<FileStorage> FileStores { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }

        
    }
}