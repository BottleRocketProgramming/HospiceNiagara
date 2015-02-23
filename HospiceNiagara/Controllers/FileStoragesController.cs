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
    public class FileStoragesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FileStorages
        public ActionResult Index()
        {
            var fileStorages = db.FileStorages.Include(f => f.FileSortType);
            return View(fileStorages.ToList());
        }

        // GET: FileStorages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStorage fileStorage = db.FileStorages.Find(id);
            if (fileStorage == null)
            {
                return HttpNotFound();
            }
            return View(fileStorage);
        }

        // GET: FileStorages/Create
        public ActionResult Create()
        {
            ViewBag.FileSortTypeID = new SelectList(db.FileSortTypes, "ID", "FileSrtDefintion");
            return View();
        }

        // POST: FileStorages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FileContent,MimeType,FileName,FileSortTypeID")] FileStorage fileStorage)
        {
            if (ModelState.IsValid)
            {
                db.FileStorages.Add(fileStorage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FileSortTypeID = new SelectList(db.FileSortTypes, "ID", "FileSrtDefintion", fileStorage.FileSortTypeID);
            return View(fileStorage);
        }

        // GET: FileStorages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStorage fileStorage = db.FileStorages.Find(id);
            if (fileStorage == null)
            {
                return HttpNotFound();
            }
            ViewBag.FileSortTypeID = new SelectList(db.FileSortTypes, "ID", "FileSrtDefintion", fileStorage.FileSortTypeID);
            return View(fileStorage);
        }

        // POST: FileStorages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FileContent,MimeType,FileName,FileSortTypeID")] FileStorage fileStorage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileStorage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FileSortTypeID = new SelectList(db.FileSortTypes, "ID", "FileSrtDefintion", fileStorage.FileSortTypeID);
            return View(fileStorage);
        }

        // GET: FileStorages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStorage fileStorage = db.FileStorages.Find(id);
            if (fileStorage == null)
            {
                return HttpNotFound();
            }
            return View(fileStorage);
        }

        // POST: FileStorages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileStorage fileStorage = db.FileStorages.Find(id);
            db.FileStorages.Remove(fileStorage);
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
