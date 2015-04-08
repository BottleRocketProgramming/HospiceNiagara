using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class BoardContact
    {
        [Required]
        public int ID { get; set; }

        [Display(Name = "Primary Position")]
        [Required(ErrorMessage = "Board Member's positon is required")]
        [StringLength(100, ErrorMessage = "Board Member's positon can not exceed 100 characters")]
        public string BoardContPosition { get; set; }

        [Display(Name = "Home Address")]
        [Required(ErrorMessage = "Board Member's home address is required")]
        [StringLength(250, ErrorMessage = "Board Member's home address can not exceed 250 characters")]
        public string BoardContHomeAddy { get; set; }

        [Display(Name = "Work Address")]
        [StringLength(250, ErrorMessage = "Board Member's work address can not exceed 250 characters")]
        public string BoardContWorkAddy { get; set; }

        [Display(Name = "Home Phone")]
        [Required(ErrorMessage = "Board Member's home phone number is required")]
        [StringLength(25, ErrorMessage = "Board Member's home phone number can not exceed 25 characters")]
        public string BoardContHomePhone { get; set; }

        [Display(Name = "Work Extension")]
        [StringLength(25, ErrorMessage = "Board Member's work phone number can not exceed 25 characters")]
        public string BoardContWorkPhone { get; set; }

        [Display(Name = "Cell Phone")]
        [StringLength(25, ErrorMessage = "Board Member's cell phone number can not exceed 25 characters")]
        public string BoardContCellPhone { get; set; }

        [Display(Name = "Fax Number")]
        [StringLength(25, ErrorMessage = "Board Member's fax number can not exceed 25 characters")]
        public string BoardContFaxNum { get; set; }

        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "Board Member's partner's name can not exceed 100 characters")]
        public string BoardContPartnerName { get; set; }

        public virtual ICollection<JobDescription> JobDescriptions { get; set; }

        public virtual ICollection<ApplicationUser> IdentUsers { get; set;  }
    }
}