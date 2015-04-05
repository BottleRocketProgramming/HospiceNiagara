using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospiceNiagara.Models;
using HospiceNiagara.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Infrastructure;

namespace HospiceNiagara.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            int c = 0;
            var cUserRoles = db.RoleLists;
            var ann = db.Announcements.Include(a => a.RolesLists);

            foreach (var ur in cUserRoles)
            {
                if (User.IsInRole(ur.RoleName))
                {
                    ann = ann.Where(a => a.RolesLists.Any(aur => aur.ID == ur.ID));
                    c++;
                }
            }
            if( c == 0 )
            {
                ann = ann.Where(a=>a.ID == 0);
            }
            ViewData["AnnouncementOrEvent"] = ann.ToList();
            ViewData["Schedule"] = db.Schedules.ToList();
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}