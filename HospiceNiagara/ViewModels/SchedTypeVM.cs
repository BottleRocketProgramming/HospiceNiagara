using HospiceNiagara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospiceNiagara.ViewModels
{
    public class SchedTypeVM
    {
        public int ID { get; set; }

        public string SchedTypeName { get; set; }

        public bool SchedTypeSelected { get; set; }
    }
}