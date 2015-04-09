using HospiceNiagara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospiceNiagara.ViewModels
{
    public class SchdVM
    {
        
        public int ID { get; set; }

        
        public string SchedName { get; set; }

        
        public string SchedLink { get; set; }

        public virtual SchedType SchedType { get; set; }

        public virtual ICollection<RoleList> ScheduleRoles { get; set; }
    }
}