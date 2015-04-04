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
    public class FileSubCatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FileSubCats
        public ActionResult Index()
        {
            return View(db.FileSubCats.ToList());
        }

        // GET: FileSubCats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileSubCat fileSubCat = db.FileSubCats.Find(id);
            if (fileSubCat == null)
            {
                return HttpNotFound();
            }
            return View(fileSubCat);
        }

        // GET: FileSubCats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileSubCats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FileSubCatName,FileCatFK")] FileSubCat fileSubCat)
        {
            if (ModelState.IsValid)
            {
                db.FileSubCats.Add(fileSubCat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fileSubCat);
        }

        // GET: FileSubCats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileSubCat fileSubCat = db.FileSubCats.Find(id);
            if (fileSubCat == null)
            {
                return HttpNotFound();
            }
            return View(fileSubCat);
        }

        // POST: FileSubCats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FileSubCatName,FileCatFK")] FileSubCat fileSubCat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileSubCat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fileSubCat);
        }

        // GET: FileSubCats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileSubCat fileSubCat = db.FileSubCats.Find(id);
            if (fileSubCat == null)
            {
                return HttpNotFound();
            }
            return View(fileSubCat);
        }

        // POST: FileSubCats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileSubCat fileSubCat = db.FileSubCats.Find(id);
            db.FileSubCats.Remove(fileSubCat);
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
