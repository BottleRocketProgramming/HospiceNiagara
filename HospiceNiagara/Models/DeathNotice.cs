using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class DeathNotice
    {
        [Required]
        public int ID { get; set; }

        [Required(ErrorMessage = "First name is required for a death notice")]
        [StringLength(50, ErrorMessage = "First name can not exceed 50 characters")]
        public string DnFirstName { get; set; }

        
        [StringLength(50, ErrorMessage = "Middle name can not exceed 50 characters")]
        public string DnMiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required for a death notice")]
        [StringLength(50, ErrorMessage = "Last name can not exceed 50 characters")]
        public string DnLastName { get; set; }

        [Required(ErrorMessage = "Date is required for death notice")]
        [DataType(DataType.Date)]
        public DateTime DnDate;

        
        [StringLength(150, ErrorMessage = "Location can not exceed 150 characters")]
        public string DnLocation { get; set; }

        [StringLength(510, ErrorMessage = "Notes can not exceed 510 characters")]
        public string DnNotes { get; set; }
    }
}