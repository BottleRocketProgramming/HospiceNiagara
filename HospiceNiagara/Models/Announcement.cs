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
        [DataType(DataType.MultilineText)]
        public string AnnounceText { get; set; }

        [Display(Name= "End Date")]
        [Required(ErrorMessage = "Announcement end date is required")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime AnnounceEndDate { get; set; }

        [Display(Name="End Date")]
        public string AnnounceEndDateString
        {
            get
            {
                return this.AnnounceEndDate.ToLongDateString();
            }
        }

        public virtual ICollection<RoleList> RolesLists { get; set; }

        //public virtual ICollection<ApplicationUser> UserAnnouncements { get; set; }

        public virtual ICollection<FileStorage> FileStorages { get; set; }

       
    }
}