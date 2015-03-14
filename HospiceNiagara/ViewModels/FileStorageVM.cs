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
        [Display(Name="Mime Type")]
        [StringLength(256)]
        public string MimeType { get; set; }

        [Display(Name="Name")]
        [Required(ErrorMessage = "Please name the file")]
        [StringLength(100, ErrorMessage = "File name can not be longer than 100 characters")]
        public string FileName { get; set; }

        [Display(Name="Description")]
        [StringLength(100, ErrorMessage = "The file discription can not exceed 100 characters")]
        public string FileDescription { get; set; }

        [Required]
        [Display(Name="Date Uploaded")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FileUploadDate { get; set; }

       public virtual ICollection<RoleList> FileStoreUserRoles { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }

        public virtual ICollection<FileSubCat> FileSubCats { get; set; }
    }
}