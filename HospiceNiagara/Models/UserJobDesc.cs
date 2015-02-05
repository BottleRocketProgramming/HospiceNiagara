using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class UserJobDesc
    {
        public int ID { get; set; }
        public virtual IdentityUser IdentityUsers { get; set; }
        public virtual JobDescription JobDescriptions { get; set; }
    }
}