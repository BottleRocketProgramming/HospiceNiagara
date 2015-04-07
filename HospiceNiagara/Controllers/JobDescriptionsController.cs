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
    public class JobDescriptionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobDescriptions
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.JobDescriptions.ToList());
        }

        // GET: JobDescriptions/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobDescription jobDescription = db.JobDescriptions.Find(id);
            if (jobDescription == null)
            {
                return HttpNotFound();
            }
            return View(jobDescription);
        }

        // GET: JobDescriptions/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobDescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,JobTitle,JobDescpt")] JobDescription jobDescription)
        {
            if (ModelState.IsValid)
            {
                db.JobDescriptions.Add(jobDescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobDescription);
        }

        // GET: JobDescriptions/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobDescription jobDescription = db.JobDescriptions.Find(id);
            if (jobDescription == null)
            {
                return HttpNotFound();
            }
            return View(jobDescription);
        }

        // POST: JobDescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,JobTitle,JobDescpt")] JobDescription jobDescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobDescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobDescription);
        }

        // GET: JobDescriptions/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobDescription jobDescription = db.JobDescriptions.Find(id);
            if (jobDescription == null)
            {
                return HttpNotFound();
            }
            return View(jobDescription);
        }

        // POST: JobDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobDescription jobDescription = db.JobDescriptions.Find(id);
            db.JobDescriptions.Remove(jobDescription);
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
