using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class MeetingUserRSVP
    {
        public int ID { get; set; }

        
        [Display(Name="Will be attending")]
        public bool? ComingYorN { get; set; }

        [Display(Name="RSVP Notes")]
        [StringLength(250, ErrorMessage = "Announcement can not exceed 250 characters")]
        public string RSVPNotes { get; set; }

        public virtual ApplicationUser AppUser { get; set; }

        public virtual Meeting MeetingRSVP { get; set; }
    }
}