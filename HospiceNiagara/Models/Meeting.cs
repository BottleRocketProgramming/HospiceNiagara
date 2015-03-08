using System;
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

        [Required(ErrorMessage = "Event's title can not be left blank")]
        [StringLength(100, ErrorMessage = "Event's title can not exceed 100 characters")]
        public string EventTitle { get; set; }

        [Required(ErrorMessage = "Event's description can not be left blank")]
        [StringLength(510, ErrorMessage = "Event's discription can not exceed 510 characters")]
        public string EventDiscription { get; set; }

        [Required(ErrorMessage = "Event location can not be left blank")]
        [StringLength(510, ErrorMessage = "Event location can not exceed 510 characters")]
        public string EventLocation { get; set; }

        [Required(ErrorMessage = "Event needs a start time")]
        [DataType(DataType.DateTime)]
        public DateTime EventStart { get; set; }

        [Required(ErrorMessage = "Event needs an end time")]
        [DataType(DataType.DateTime)]
        public DateTime EventEnd { get; set; }

        [StringLength(510, ErrorMessage = "Event requirements can not exceed 510 characters")]
        public string EventRequirments { get; set; }

        [StringLength(510, ErrorMessage = "Event links can not exceed 510 characters")]
        public string EventLinks { get; set; }

        public virtual ICollection<RoleList> MeetingOrEventUserRoles { get; set; }

        public virtual ICollection<FileStorage> FileStores { get; set; }





    }
}