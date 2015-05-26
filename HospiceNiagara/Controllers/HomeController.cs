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

//Paul Boyko April 2015

namespace HospiceNiagara.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            DateTime insevendays = DateTime.Today.AddDays(7);
            var cUserRoles = db.RoleLists;
            var meetForList = db.Meetings.Where(m => m.ID == 0);
            var annForList = db.Announcements.Where(a => a.ID == 0);
            var schedForList = db.Schedules.Where(s => s.ID == 0);

            foreach (var ur in cUserRoles)
            {
                if (User.IsInRole(ur.RoleName))
                {
                    var ann = db.Announcements.Include(a => a.RolesLists);
                    var meet = db.Meetings.Include(a => a.RolesLists);
                    var sched = db.Schedules.Include(a => a.ScheduleRoles);
                    meet = meet.Where(a => a.RolesLists.Any(aur => aur.ID == ur.ID));
                    meetForList = meetForList.Concat(meet);
                    sched = sched.Where(a => a.ScheduleRoles.Any(aur => aur.ID == ur.ID));
                    schedForList = schedForList.Concat(sched);
                    ann = ann.Where(a => a.RolesLists.Any(aur => aur.ID == ur.ID));
                    annForList = annForList.Concat(ann);
                }
            }

            var welcomeNotice = db.WelcomeNotices.Single();
            var viewModel = new WelcomeNoticeVM();
            viewModel.ID = welcomeNotice.ID;
            viewModel.WelocomeNotice = welcomeNotice.WelocomeNotice;

            ViewBag.WelcomeNotice = viewModel;
            annForList = annForList.Where(a => a.AnnounceEndDate >= DateTime.Today);
            meetForList = meetForList.Where(m => m.EventStart < insevendays).Where(m => m.EventEnd > DateTime.Today);
            ViewData["AnnouncementOrEvent"] = annForList.ToList().Distinct().OrderByDescending(a => a.UploadDate);
            ViewData["Meeting"] = meetForList.ToList().Distinct().OrderByDescending(a => a.UploadDate);
            ViewData["Schedule"] = schedForList.ToList().Distinct();

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

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;

            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");

            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model)
            };

        }
    }
}