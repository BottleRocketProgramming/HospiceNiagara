﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospiceNiagara.Models
{
    public class ScheduleRole
    {
        [Required]
        public int ID { get; set; }

        public virtual Schedule Schedules { get; set; }

        public virtual IdentityRole IdentityRoles { get; set; }
    }
}