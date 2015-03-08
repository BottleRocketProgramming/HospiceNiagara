using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class FileSortType
    {
        public FileSortType()
        {
            this.FileStorages = new HashSet<FileStorage>();
        }
        
        [Required]
        public int ID { get; set; }

        [Required]
        public string FileSrtDefintion { get; set; }

        public virtual ICollection<FileStorage> FileStorages { get; set; }
    }
}