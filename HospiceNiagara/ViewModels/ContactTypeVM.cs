using HospiceNiagara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospiceNiagara.ViewModels
{
    public class ContactTypeVM
    {
        public int ID { get; set; }

        public string ContactTypeName { get; set; }

        public bool ContactTypeSelected { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}