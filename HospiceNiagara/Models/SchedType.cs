using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Paul Boyko March 2015

namespace HospiceNiagara.Models
{
    public class SchedType
    {
        public int ID { get; set; }

        public string SchedTypeName { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}