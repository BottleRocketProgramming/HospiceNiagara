using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class UserAnnouncement
    {
        [Required]
        public int ID { get; set; }

        public bool WasViewed { get; set; }

        public virtual IdentityUser IdentityUsers { get; set; }

        public virtual Announcement Announcements { get; set; }
    }
}