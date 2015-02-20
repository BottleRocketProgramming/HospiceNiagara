using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HospiceNiagara.Models
{
    public class AnnouncementUserRole
    {
        public int ID { get; set; }
       
        public virtual IdentityRole IdentityRoles { get; set; }
       
        public virtual Announcement Announcements { get; set; }
        
        public bool WasViewed { get; set; }
    }
}