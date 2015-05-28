using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class Meeting
    {
        [Required]
        public int ID { get; set; }

        [Display(Name="Event Name")]
        [Required(ErrorMessage = "Event's title can not be left blank")]
        [StringLength(100, ErrorMessage = "Event's title can not exceed 100 characters")]
        public string EventTitle { get; set; }

        [Display(Name="Description")]
        [Required(ErrorMessage = "Event's description can not be left blank")]
        [AllowHtml]
        public string EventDiscription { get; set; }

        private int DescriptionLimit = 125;
        private string evtText;

        [Display(Name = "Description Short")]
        public string DescriptionTrimmed
        {
            get
            {
                evtText = Helpers.HTMLhelper.TruncateHtml(this.EventDiscription, DescriptionLimit, "...");
                return evtText;
            }
        }

        [Display(Name="Location")]
        [Required(ErrorMessage = "Event location can not be left blank")]
        [StringLength(510, ErrorMessage = "Event location can not exceed 510 characters")]
        public string EventLocation { get; set; }

        [Display(Name="Date")]
        [Required(ErrorMessage = "Event needs a Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Date")]
        public string EventDateString
        {
            get
            {
                return this.EventDate.ToLongDateString();
            }
        }

        [Display(Name = "Time")]
        [Required(ErrorMessage = "Event needs an assigned time")]
        public string EventTime { get; set; }

        [Display(Name = "Upload Date")]
        public DateTime UploadDate { get; set; }

        [Display(Name = "Uploaded By")]
        public string UploadedBy { get; set; }

        public virtual ICollection<RoleList> RolesLists { get; set; }

        public virtual ICollection<FileStorage> FileStores { get; set; }

        public virtual ICollection<MeetingUserRSVP> MeetingUserRSVPs { get; set; }
    }
}