using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class Contact
    {
        public int ID { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Contact Name is required")]
        public string Name { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        [Display(Name = "Extention")]
        public string Extention { get; set; }

        [Display(Name = "Cell Number")]
        public string CellNumber { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }


        public virtual ContactType ContactType { get; set; }
    }
}