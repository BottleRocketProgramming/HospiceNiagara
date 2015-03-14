using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Paul Boyko March 2015

namespace HospiceNiagara.Models
{
    public class FileSubCat
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string FileSubCatName { get; set; }

        public virtual ICollection<FileCat> FileCats { get; set; }

        public virtual ICollection<FileStorage> FileStores { get; set; }
    }
}