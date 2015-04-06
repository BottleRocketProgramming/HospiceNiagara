﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        [StringLength(510, ErrorMessage = "Event's discription can not exceed 510 characters")]
        public string EventDiscription { get; set; }

        private int DescriptionLimit = 100;

        //You probably need this if you want to use .LabelFor() 
        //and let this property mimic the "full" one  
        [Display(Name = "Description Short")]
        public string DescriptionTrimmed
        {
            get
            {
                if (this.EventDiscription.Length > this.DescriptionLimit)
                    return this.EventDiscription.Substring(0, this.DescriptionLimit) + "...";
                else
                    return this.EventDiscription;
            }
        }

        [Display(Name="Location")]
        [Required(ErrorMessage = "Event location can not be left blank")]
        [StringLength(510, ErrorMessage = "Event location can not exceed 510 characters")]
        public string EventLocation { get; set; }

        [Display(Name="Start")]
        [Required(ErrorMessage = "Event needs a start time")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime EventStart { get; set; }

        [Display(Name="End")]
        [Required(ErrorMessage = "Event needs an end time")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime EventEnd { get; set; }

        [Display(Name="Event Requirements")]
        [StringLength(510, ErrorMessage = "Event requirements can not exceed 510 characters")]
        public string EventRequirments { get; set; }

        [Display(Name="Links")]
        [StringLength(510, ErrorMessage = "Event links can not exceed 510 characters")]
        public string EventLinks { get; set; }

        public virtual ICollection<RoleList> RolesLists { get; set; }

        public virtual ICollection<FileStorage> FileStores { get; set; }

        public virtual ICollection<MeetingUserRSVP> MeetingUserRSVPs { get; set; }





    }
}