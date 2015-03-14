﻿using HospiceNiagara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospiceNiagara.ViewModels
{
    public class FileCatVM
    {
        public class FileCat
        {
            public int ID { get; set; }

            public string FileCatName { get; set; }

            public virtual ICollection<FileSubCat> FileSubCats { get; set; }

        }
    }
}