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

        [Required(ErrorMessage = "Board Member's positon is required")]
        [StringLength(100, ErrorMessage = "Board Member's positon can not exceed 100 characters")]
        public string BoardContPosition { get; set; }

        [Required(ErrorMessage = "Board Member's home address is required")]
        [StringLength(250, ErrorMessage = "Board Member's home address can not exceed 250 characters")]
        public string BoardContHomeAddy { get; set; }

        [StringLength(250, ErrorMessage = "Board Member's work address can not exceed 250 characters")]
        public string BoardContWorkAddy { get; set; }

        [Required(ErrorMessage = "Board Member's home phone number is required")]
        [StringLength(25, ErrorMessage = "Board Member's home phone number can not exceed 25 characters")]
        public string BoardContHomePhone { get; set; }

        
        [StringLength(25, ErrorMessage = "Board Member's work phone number can not exceed 25 characters")]
        public string BoardContWorkPhone { get; set; }

        [StringLength(25, ErrorMessage = "Board Member's cell phone number can not exceed 25 characters")]
        public string BoardContCellPhone { get; set; }

        [StringLength(25, ErrorMessage = "Board Member's fax number can not exceed 25 characters")]
        public string BoardContFaxNum { get; set; }

        [StringLength(100, ErrorMessage = "Board Member's partner's name can not exceed 100 characters")]
        public string BoardContPartnerName { get; set; }
    }
}