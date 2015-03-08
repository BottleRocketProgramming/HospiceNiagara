using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class UserJobDesc
    {
        [Required]
        public int ID { get; set; }
        
        public virtual IdentityUser IdentityUsers { get; set; }
       
        public virtual JobDescription JobDescriptions { get; set; }

    }
}