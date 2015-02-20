using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class UserEventRSVP
    {
        [Required]
        public int ID { get; set; }
        
        public virtual JobDescription JobDescriptions { get; set; }
        
        public virtual IdentityUser IdentityUsers { get; set; }
       
        public bool HasRSVPed { get; set; }
    }
}