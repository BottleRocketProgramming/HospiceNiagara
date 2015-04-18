﻿using System;
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
        [Display(Name="File Sub-Catigory Name")]
        public string FileSubCatName { get; set; }

        [Display(Name="Related File Catigory")]
        public int FileCatFK { get; set; }

        public virtual FileCat FlCat { get; set; }

        public virtual ICollection<FileStorage> FileStores { get; set; }
    }
}