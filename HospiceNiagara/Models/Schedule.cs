using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class Schedule
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Schedule")]
        [StringLength(50, ErrorMessage = "Shedule name can not exceed 50 characters")]
        public string SchedName { get; set; }

        [Required]
        [StringLength(510, ErrorMessage = "Schedule Link URL can not exceed 510 characters")]
        public string SchedLink { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime SchedStartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        public DateTime SchedEndDate { get; set; }

        public virtual SchedType SchedType { get; set; }

        public virtual ICollection<RoleList> ScheduleRoles { get; set; }


    }
}