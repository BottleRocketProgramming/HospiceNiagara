using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class FileCat
    {
        public int ID { get; set; }

        public string FileCatName { get; set; }

        public virtual ICollection<FileSubCat> FileSubCats { get; set; }
        
    }
}