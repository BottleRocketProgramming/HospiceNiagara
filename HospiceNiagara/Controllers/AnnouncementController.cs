using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospiceNiagara.Models;

namespace HospiceNiagara.Controllers
{
    public class AnnouncementController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationDbContext dbb = new ApplicationDbContext();

        // GET: Announcement
        public ActionResult Index(int? id)
        {            
            ViewData["AnnouncementOrEvent"] = db.Announcements.ToList();
            ViewData["AnnOrEvntId"] = id;
            Announcement announcement = db.Announcements.Find(id);
            ViewData["DeathNoticeList"] = dbb.DeathNotices.ToList();
            
            return View(announcement);           

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

        // GET: Announcement/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Announcement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]    
        [ActionName("Index")]
        [OnAction(ButtonName= "CreateAnnouncement")]
        public ActionResult Create([Bind(Include = "ID,AnnounceText,AnnounceEndDate,IsEvent")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                announcement.IsEvent = false;
                db.Announcements.Add(announcement);
                db.SaveChanges();
               
                
            }
            
            return RedirectToAction("Index");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateEvent")]
        public ActionResult Create2([Bind(Include = "ID,AnnounceText,AnnounceEndDate,IsEvent")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                announcement.IsEvent = true;
                db.Announcements.Add(announcement);
                db.SaveChanges();


            }

            return RedirectToAction("Index");

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
