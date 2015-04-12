using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospiceNiagara.Models;

//Andreas King March 2015

namespace HospiceNiagara.Controllers
{
    public class DeathNoticeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeathNotice
        [Authorize(Roles = "Administrator, Staff, Board, Volunteers, Leadership, Admin, Community, Outreach, Residential, New Staff, Audit, Audit & Finance Committee, Community Relations Committee, Operations & Quality Improvement Committee, New Board Members, Bereavement, Day Hospice, Residential, Welcome Desk, New Volunteers")]
        public ActionResult Index(int? id)
        {
            ViewData["DeathNoticeList"] = db.DeathNotices.ToList();

            ViewData["DeathNoticeID"] = id;

            DeathNotice deathNotice = db.DeathNotices.Find(id);
            return View();
        }

        //Admin List
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminList()
        {
            return View(db.DeathNotices.ToList());
        }

        // GET: DeathNotice/Details/5
        [Authorize(Roles = "Administrator, Staff, Board, Volunteers, Leadership, Admin, Community, Outreach, Residential, New Staff, Audit, Audit & Finance Committee, Community Relations Committee, Operations & Quality Improvement Committee, New Board Members, Bereavement, Day Hospice, Residential, Welcome Desk, Admin, New Volunteers")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeathNotice deathNotice = db.DeathNotices.Find(id);
            if (deathNotice == null)
            {
                return HttpNotFound();
            }
            return View(deathNotice);
        }

        // GET: DeathNotice/adminCreate
        [Authorize(Roles = "Administrator")]
        public ActionResult adminCreate()
        {
            return View();
        }

        // POST: DeathNotice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateDeathNotice")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "ID,DnFirstName,DnMiddleName,DnLastName,DnDate,DnLocation,DnNotes")] DeathNotice deathNotice)
        {
            if (ModelState.IsValid)
            {
                db.DeathNotices.Add(deathNotice);
                db.SaveChanges();
                return RedirectToAction("AdminList");
            }

            return View(deathNotice);
        }

        //Admin Create for Death Notices
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult adminCreate([Bind(Include = "ID,DnFirstName,DnMiddleName,DnLastName,DnDate,DnLocation,DnNotes")] DeathNotice deathNotice)
        {
            if (ModelState.IsValid)
            {
                db.DeathNotices.Add(deathNotice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deathNotice);
        }

        // GET: DeathNotice/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeathNotice deathNotice = db.DeathNotices.Find(id);
            if (deathNotice == null)
            {
                return HttpNotFound();
            }
            return View(deathNotice);
        }

        // POST: DeathNotice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "ID,DnFirstName,DnMiddleName,DnLastName,DnDate,DnLocation,DnNotes")] DeathNotice deathNotice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deathNotice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deathNotice);
        }

        // GET: DeathNotice/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeathNotice deathNotice = db.DeathNotices.Find(id);
            if (deathNotice == null)
            {
                return HttpNotFound();
            }
            return View(deathNotice);
        }

        // POST: DeathNotice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            DeathNotice deathNotice = db.DeathNotices.Find(id);
            db.DeathNotices.Remove(deathNotice);
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
