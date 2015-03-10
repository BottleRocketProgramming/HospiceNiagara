using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class FileSubCat
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string FileSubCatName { get; set; }

        public virtual FileCat FileCatFK { get; set; }

        public virtual ICollection<FileStorage> FileStores { get; set; }
    }
}