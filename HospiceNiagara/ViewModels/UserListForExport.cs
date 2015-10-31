using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospiceNiagara.ViewModels
{
    public class UserListForExport
    {
        public string FName { get; set; }

        public string LName { get; set; }

        public string Email { get; set; }

        public Nullable<DateTime> LastLogin { get; set; }

        public bool Registered { get; set; }
    }
}