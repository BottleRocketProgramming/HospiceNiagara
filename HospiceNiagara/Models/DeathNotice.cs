using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class DeathNotice
    {
        [Required]
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string DnFullName
        {
            get
            {
                return DnFirstName + (string.IsNullOrEmpty(DnMiddleName) ? " " : (" " + (char?)DnMiddleName[0] + ". ").ToUpper()) + DnLastName;
            }
        }

        [Display(Name= "First")]
        [Required(ErrorMessage = "First name is required for a death notice")]
        [StringLength(50, ErrorMessage = "First name can not exceed 50 characters")]
        public string DnFirstName { get; set; }

        [Display(Name= "Middle")]
        [StringLength(50, ErrorMessage = "Middle name can not exceed 50 characters")]
        public string DnMiddleName { get; set; }

        [Display(Name="Last")]
        [Required(ErrorMessage = "Last name is required for a death notice")]
        [StringLength(50, ErrorMessage = "Last name can not exceed 50 characters")]
        public string DnLastName { get; set; }

        [Display(Name="Date")]
        [Required(ErrorMessage = "Date is required for death notice")]
        [DataType(DataType.Date)]
        public DateTime DnDate { get; set; }

        [Display(Name ="Date")]
        public string DnDateString
        {
            get
            {
                return this.DnDate.ToLongDateString();
            }
        }

        [Display(Name="Location")]
        [StringLength(150, ErrorMessage = "Location can not exceed 150 characters")]
        public string DnLocation { get; set; }

        [Display(Name="Notes")]
        [StringLength(510, ErrorMessage = "Notes can not exceed 510 characters")]
        public string DnNotes { get; set; }
    }
}