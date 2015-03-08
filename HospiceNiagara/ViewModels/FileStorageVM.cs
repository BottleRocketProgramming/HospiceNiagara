using HospiceNiagara.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Paul Boyko Feb 2015

namespace HospiceNiagara.ViewModels
{
    public class FileStorageVM
    {
        public int ID { get; set; }

        [Required]
        [StringLength(256)]
        public string MimeType { get; set; }

        [Required(ErrorMessage = "Please name the file")]
        [StringLength(100, ErrorMessage = "File name can not be longer than 100 characters")]
        public string FileName { get; set; }

        [StringLength(100, ErrorMessage = "The file discription can not exceed 100 characters")]
        public string FileDescription { get; set; }

        //public int FileSortTypeID { get; set; }

        public virtual FileSortType FileSortType { get; set; }

        public virtual ICollection<RoleList> FileStoreUserRoles { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }

        public virtual ICollection<FileCat> FileCats { get; set; }
    }
}