using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class FileStorage
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please select a file to upload")]
        public byte[] FileContent { get; set; }

        [Required]
        [StringLength(256)]
        public string MimeType { get; set; }

        [Required(ErrorMessage = "Please name the file")]
        [StringLength(100, ErrorMessage = "File name can not be longer than 100 characters")]
        public string FileName { get; set; }
               
        public int FileSortTypeID { get; set; }

        public virtual FileSortType FileSortType { get; set; }

    }
}