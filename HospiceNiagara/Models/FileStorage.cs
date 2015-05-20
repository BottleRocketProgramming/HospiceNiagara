using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Models
{
    public class FileStorage
    {
        public int ID { get; set; }

        [Display(Name="File")]
        [Required(ErrorMessage = "Please select a file to upload")]
        public byte[] FileContent { get; set; }

        [Required]
        [Display(Name="Mime Type")]
        [StringLength(256)]
        public string MimeType { get; set; }

        [Required(ErrorMessage = "Please name the file")]
        [Display(Name="File Name")]
        [StringLength(100, ErrorMessage = "File name can not be longer than 100 characters")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Please give the file a description")]
        [StringLength(100, ErrorMessage = "The file discription can not exceed 100 characters")]
        [Display(Name="Description")]
        public string FileDescription { get; set; }

        [Required]
        [Display(Name="Upload Date")]
        [DataType(DataType.Date)]
        public DateTime FileUploadDate { get; set; }

        [Display(Name = "Uploaded By")]
        public string UploadedBy { get; set; }

        [Display(Name = "Upload Date")]
        public string FileUploadDateString
        {
            get
            {
                return this.FileUploadDate.ToLongDateString();
            }
        }
               
        public virtual ICollection<RoleList> FileStoreUserRoles { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }

        public virtual ICollection<FileSubCat> FileSubCats { get; set; }
    }

}