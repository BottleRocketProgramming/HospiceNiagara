using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class FileCat
    {
        public int ID { get; set; }

        [Display(Name="File Category Name")]
        [Required(ErrorMessage="File Category Name is Required")]
        public string FileCatName { get; set; }

        public virtual ICollection<FileSubCat> FileSubCats { get; set; }
        
    }
}