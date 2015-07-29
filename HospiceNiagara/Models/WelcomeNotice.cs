using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospiceNiagara.Models
{
    public class WelcomeNotice
    {
        public int ID { get; set; }

        [Required(ErrorMessage="Welcome notice is required")]
        [Display(Name="Welcome Notice")]
        [StringLength(250, ErrorMessage="Welcome notice can not be longer then 250 characters")]
        [AllowHtml]
        public string WelocomeNotice { get; set; }
    }
}