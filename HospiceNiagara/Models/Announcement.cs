using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class Announcement
    {
        [Required]
        public int ID { get; set; }

        [Display(Name= "Announcement")]
        [Required(ErrorMessage = "Announcement can not be left blank")]
        [StringLength(1020, ErrorMessage = "Announcement can not exceed 1020 characters")]
        public string AnnounceText { get; set; }

        [Display(Name = "Start Date")]
        //[DisplayFormat(DataFormatString = "{0:MM-dd-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? AnnounceStartDate { get; set; }
        
        [Display(Name= "End Date")]
        [Required(ErrorMessage = "Announcement end date is required")]
        //[DisplayFormat(DataFormatString = "{0:MM-dd-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime AnnounceEndDate { get; set; }



        [Required]
        public bool IsEvent { get; set; }

        public virtual ICollection<RoleList> RolesLists { get; set; }

        public virtual ICollection<IdentityUser> UserAnnouncements { get; set; }

        public virtual ICollection<FileStorage> FileStorages { get; set; }

       
    }
}