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
    public class MeetingUserRSVPsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MeetingUserRSVPs
        public ActionResult Index()
        {
            var meetingUserRSVP = db.MeetingUserRSVPs.Include(u => u.AppUser).Include(m => m.MeetingRSVP);
            meetingUserRSVP = meetingUserRSVP.Where(u => u.AppUser.UserName == User.Identity.Name);


            return View(meetingUserRSVP);
        }

        // GET: MeetingUserRSVPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingUserRSVP meetingUserRSVP = db.MeetingUserRSVPs.Find(id);
            if (meetingUserRSVP == null)
            {
                return HttpNotFound();
            }
            return View(meetingUserRSVP);
        }

        // GET: MeetingUserRSVPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeetingUserRSVPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ComingYorN,RSVPNotes")] MeetingUserRSVP meetingUserRSVP)
        {
            if (ModelState.IsValid)
            {
                db.MeetingUserRSVPs.Add(meetingUserRSVP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meetingUserRSVP);
        }

        // GET: MeetingUserRSVPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var meetingUserRSVP = db.MeetingUserRSVPs.Include(u => u.AppUser).Include(m => m.MeetingRSVP);
            meetingUserRSVP = meetingUserRSVP.Where(u => u.AppUser.UserName == User.Identity.Name);
            MeetingUserRSVP MuRSVP = meetingUserRSVP.Where(m => m.MeetingRSVP.ID == id).Single();
            if (meetingUserRSVP == null)
            {
                return HttpNotFound();
            }
            return View(MuRSVP);
        }

        // POST: MeetingUserRSVPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ComingYorN,RSVPNotes")] MeetingUserRSVP meetingUserRSVP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetingUserRSVP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meetingUserRSVP);
        }

        // GET: MeetingUserRSVPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingUserRSVP meetingUserRSVP = db.MeetingUserRSVPs.Find(id);
            if (meetingUserRSVP == null)
            {
                return HttpNotFound();
            }
            return View(meetingUserRSVP);
        }

        // POST: MeetingUserRSVPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeetingUserRSVP meetingUserRSVP = db.MeetingUserRSVPs.Find(id);
            db.MeetingUserRSVPs.Remove(meetingUserRSVP);
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
