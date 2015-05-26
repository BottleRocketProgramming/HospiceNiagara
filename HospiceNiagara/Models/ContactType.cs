using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class ContactType
    {
        public int ID { get; set; }

        [Display(Name = "Contact type")]
        [Required(ErrorMessage = "Contact type name is required")]
        public string ContactTypeName { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}