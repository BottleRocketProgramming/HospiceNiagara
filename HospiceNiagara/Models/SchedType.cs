using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Paul Boyko March 2015

namespace HospiceNiagara.Models
{
    public class SchedType
    {
        public int ID { get; set; }

        [Display(Name="Schedule type")]
        [Required(ErrorMessage="Schedule type name is required")]
        public string SchedTypeName { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }

        public virtual ICollection<RoleList> RoleLists { get; set; }
    }
}