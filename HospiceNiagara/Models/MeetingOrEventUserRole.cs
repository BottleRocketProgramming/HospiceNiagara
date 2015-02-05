using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class MeetingOrEventUserRole
    {
        public int ID { get; set; }
        public virtual MeetingOrEvent MettingOrEvents { get; set; }
        public virtual IdentityRole IdentityRoles { get; set; }
    }
}