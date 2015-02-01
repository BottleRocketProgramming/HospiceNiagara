using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class Announcement
    {
        [Required]
        public int ID { get; set; }

        [Required(ErrorMessage = "Announcement can not be left blank")]
        [StringLength(1020, ErrorMessage = "Announcement can not exceed 1020 characters")]
        public string AnnounceText { get; set; }

        [Required(ErrorMessage = "Announcement end date is required")]
        [DataType(DataType.DateTime)]
        public DateTime AnnounceEndDate { get; set; }
    }
}