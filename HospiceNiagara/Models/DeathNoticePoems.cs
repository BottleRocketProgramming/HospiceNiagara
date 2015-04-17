using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class DeathNoticePoems
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Poem is required")]
        [StringLength(1020, ErrorMessage = "Announcement can not exceed 1020 characters")]
        public string DnPoem { get; set; }
    }
}