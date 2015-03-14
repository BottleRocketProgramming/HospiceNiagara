using HospiceNiagara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospiceNiagara.ViewModels
{
    public class FileSubCatVM
    {
       
        public int ID { get; set; }
      
        public string FileSubCatName { get; set; }       

        public bool FileSubCatAssigned { get; set; }
    }
}