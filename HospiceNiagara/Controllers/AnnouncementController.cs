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


namespace HospiceNiagara.Controllers
{
    public class AnnouncementController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationDbContext dbb = new ApplicationDbContext();

        // GET: Announcement
        public ActionResult Index(int? id)
        {
            var announce = new Announcement();
            announce.RolesLists = new List<RoleList>();
            PopulateAssignedRoles(announce);
            var ann = db.Announcements.Include(a => a.RolesLists); 

            ViewData["AnnouncementOrEvent"] = ann.ToList();
            ViewData["AnnOrEvntId"] = id;
            Announcement announcement = db.Announcements.Find(id);
            ViewData["DeathNoticeList"] = dbb.DeathNotices.ToList();
            
            return View();

            //return View(db.Announcements.ToList());
        }
        
        //public ActionResult DeathNotices(int? id)
        //{
        //    ViewData["DeathNoticeList"] = dbb.DeathNotices.ToList();
        //    ViewData["DeathNoticeId"] = id;
        //    DeathNotice deathNotice = dbb.DeathNotices.Find(id);
        //    return View(deathNotice);
        //}

        // GET: Announcement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        //// GET: Announcement/Create
        //public ActionResult Create()
        //{
        //    var announce = new Announcement();
        //    announce.RolesLists = new List<RoleList>();
        //    PopulateAssignedRoles(announce);

        //    return View();
        //}

        // POST: Announcement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateAnnouncement")]
        public ActionResult Create([Bind(Include = "ID,AnnounceText,AnnounceEndDate,IsEvent")] Announcement announcement, string[] selectedRoles)
        {
            try
            {
                if (selectedRoles != null)
                {
                    announcement.RolesLists = new List<RoleList>();
                    foreach (var role in selectedRoles)
                    {
                        var roleToAdd = db.RoleLists.Find(int.Parse(role));
                        announcement.RolesLists.Add(roleToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    announcement.IsEvent = false;
                    db.Announcements.Add(announcement);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            PopulateAssignedRoles(announcement);
            return View(announcement);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateEvent")]
        public ActionResult Create2([Bind(Include = "ID,AnnounceText,AnnounceEndDate,IsEvent")] Announcement announcement, string[] selectedRoles)
        {
            try
            {
                if (selectedRoles != null)
                {
                    announcement.RolesLists = new List<RoleList>();
                    foreach (var role in selectedRoles)
                    {
                        var roleToAdd = db.RoleLists.Find(int.Parse(role));
                        announcement.RolesLists.Add(roleToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    announcement.IsEvent = true;
                    db.Announcements.Add(announcement);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            PopulateAssignedRoles(announcement);
            return View(announcement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateDeathNotice")]
        public ActionResult Create([Bind(Include = "ID,AnnounceText,AnnounceEndDate,IsEvent")] DeathNotice deathNotice)
        {
            if (ModelState.IsValid)
            {

                dbb.DeathNotices.Add(deathNotice);
                dbb.SaveChanges();


            }

            return RedirectToAction("Index");
        }

        // GET: Announcement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: Announcement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AnnounceText,AnnounceEndDate")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(announcement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        // GET: Announcement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: Announcement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            db.Announcements.Remove(announcement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void PopulateAssignedRoles(Announcement announcement)
        {
            var allRole = db.RoleLists;
            var aRoles = new HashSet<int>(announcement.RolesLists.Select(r => r.ID));
            var viewModel = new List<RoleVM>();
            foreach (var roll in allRole)
            {
                viewModel.Add(new RoleVM
                {
                    RoleID = roll.ID,
                    RoleName = roll.RoleName,
                    RoleAssigned = aRoles.Contains(roll.ID)
                });
            }

            ViewBag.RolesLists = viewModel;
        }

        private void UpdateAnnouncementRoles(string[] selectedRoles, Announcement AnnouncementToUpdate)
        {
            if (selectedRoles == null)
            {
                AnnouncementToUpdate.RolesLists = new List<RoleList>();
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var announcementRoles = new HashSet<int>
                (AnnouncementToUpdate.RolesLists.Select(c => c.ID));//IDs of the currently selected roles
            foreach (var rls in db.RoleLists)
            {
                if (selectedRolesHS.Contains(rls.ID.ToString()))
                {
                    if (!announcementRoles.Contains(rls.ID))
                    {
                        AnnouncementToUpdate.RolesLists.Add(rls);
                    }
                }
                else
                {
                    if (announcementRoles.Contains(rls.ID))
                    {
                        AnnouncementToUpdate.RolesLists.Remove(rls);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       
        
    }
}
