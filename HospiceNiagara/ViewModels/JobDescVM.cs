using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Andreas King March 2015

namespace HospiceNiagara.ViewModels
{
    public class JobDescVM
    {
        public int JobID { get; set; }
        public string JobName { get; set; }
        public string JobDesc { get; set; }
        public bool JobAssigned { get; set; }
    }
}