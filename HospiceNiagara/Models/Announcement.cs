using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

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

        [Display(Name= "Removal Date")]
        [Required(ErrorMessage = "Announcement end date is required")]
        [DataType(DataType.DateTime)]
        public DateTime AnnounceEndDate { get; set; }

        [Required]
        public bool IsEvent { get; set; }

        public virtual ICollection<AnnouncementUserRole> AnnouncementUserRoles { get; set; }

        public virtual ICollection<UserAnnouncement> UserAnnouncements { get; set; }

       
    }
}