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
            
            var cUserRoles = db.RoleLists;
            var meetForList = db.Meetings.Where(m => m.ID == 0);
            var annForLst = db.Announcements.Where(a => a.ID == 0);

            foreach (var ur in cUserRoles)
            {
                if (User.IsInRole(ur.RoleName))
                {
                    var ann = db.Announcements.Include(a => a.RolesLists);
                    var meet = db.Meetings.Include(a => a.RolesLists);
                    meet = meet.Where(a => a.RolesLists.Any(aur => aur.ID == ur.ID));
                    meetForList = meetForList.Concat(meet);
                    ann = ann.Where(a => a.RolesLists.Any(aur => aur.ID == ur.ID));
                    annForLst = annForLst.Concat(ann);
                    
                }
            }

            var welcomeNotice = db.WelcomeNotices.Single();
            var viewModel = new WelcomeNoticeVM();
            viewModel.ID = welcomeNotice.ID;
            viewModel.WelocomeNotice = welcomeNotice.WelocomeNotice;         

            ViewBag.WelcomeNotice = viewModel;
        

            ViewData["AnnouncementOrEvent"] = annForLst.ToList().Distinct();
            ViewData["Meeting"] = meetForList.ToList().Distinct();
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