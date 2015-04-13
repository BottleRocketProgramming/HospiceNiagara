using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class StaffContact
    {
        public int ID { get; set; }

        [Display(Name="Ext:")]
        public string ContExten { get; set; }

        [Display(Name="Work Cell")]
        public string ContWorkCell { get; set; }

        public virtual ApplicationUser ContUser { get; set; }

        [Display(Name="Job Titles")]
        public virtual ICollection<JobDescription> JobDescriptions { get; set; }
    }
}