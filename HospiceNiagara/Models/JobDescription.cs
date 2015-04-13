using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class JobDescription
    {
        [Required]
        public int ID { get; set; }

        [Display(Name="Job Title")]
        [Required(ErrorMessage = "Job title can not be left empty")]
        [StringLength(100, ErrorMessage = "Job title can not exceed 100 characters")]
        public string JobTitle { get; set; }

        [Display(Name="Job Description")]
        [Required(ErrorMessage = "Job description can not be left empty")]
        [StringLength(510, ErrorMessage = "Job  description can not exceed 510 characters")]
        public string JobDescpt { get; set; }

        //public virtual ICollection<ApplicationUser> IdentityUsers { get; set; }

        //public virtual ICollection<BoardContact> BoardContacts { get; set; }

        public virtual ICollection<StaffContact> StaffContacts { get; set; }
    }
}