using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class FileCat
    {
        public int ID { get; set; }

        public string FileCatName { get; set; }

        public virtual ICollection<FileStorage> FileStores { get; set; }

        public virtual ICollection<RoleList> Role { get; set; }
    }
}