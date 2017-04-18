using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class Announcement
    {
        [Required]
        public int ID { get; set; }

        [Display(Name = "Announcement Title")]
        [Required(ErrorMessage = "Announcement's title can not be left blank")]
        [StringLength(100, ErrorMessage = "Announcement's title can not exceed 100 characters")]
        public string AnnounceTitle { get; set; }

        [Display(Name= "Announcement")]
        [AllowHtml]
        public string AnnounceText { get; set; }

        [Display(Name= "End Date")]
        [Required(ErrorMessage = "Announcement end date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime AnnounceEndDate { get; set; }

        [Display(Name = "Upload Date")]
        public DateTime UploadDate { get; set; }

        [Display(Name = "Uploaded By")]
        public string UploadedBy { get; set; }

        [Display(Name="End Date")]
        public string AnnounceEndDateString
        {
            get
            {
                return this.AnnounceEndDate.ToLongDateString();
            }
        }

        private int AnnouncementTextLimit = 150;
        private string annTxt;
        public string AnnouncementTextTrimmed
        {
            get
            {
                try
                {
                    annTxt = Helpers.HTMLhelper.TruncateHtml(this.AnnounceText, AnnouncementTextLimit, "...");
                }
                catch (InvalidOperationException)
                {
                    annTxt = "ERROR - Announcement Text is too styled to truncate";
                }
                return annTxt;
            }
        }


        public virtual ICollection<RoleList> RolesLists { get; set; }

        public virtual ICollection<FileStorage> FileStorages { get; set; }

       
    }
}