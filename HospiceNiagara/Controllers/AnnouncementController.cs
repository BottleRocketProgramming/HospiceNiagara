﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospiceNiagara.Controllers
{
    public class AnnouncementController : Controller
    {
        // GET: Announcement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VolunteerEvent()
        {
            return View();
        }

        public ActionResult Announcement()
        {
            return View();
        }
    }
}
