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

//Paul Boyko April 2015

namespace HospiceNiagara.Controllers
{
    public class FileSubCatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FileSubCats
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.FileSubCats.ToList());
        }

        // GET: FileSubCats/Details/5
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            FileSubCat fSubCat = new FileSubCat();
            PopulateAssignedCat(fSubCat);
            return View();
        }

        // POST: FileSubCats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "ID,FileSubCatName,FileCatFK")] FileSubCat fileSubCat, int selectedCats)
        {
            var catToAdd = db.FileCats.Find(selectedCats);
            fileSubCat.FlCat = catToAdd;
            fileSubCat.FileCatFK = catToAdd.ID;

            if (ModelState.IsValid)
            {
                db.FileSubCats.Add(fileSubCat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateAssignedCat(fileSubCat);
            return View(fileSubCat);
        }

        // GET: FileSubCats/Edit/5
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "ID,FileSubCatName,FileCatFK")] FileSubCat fileSubCat, string selectedCats)
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
        [Authorize(Roles = "Administrator")]
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

            PopulateAssignedCat(fileSubCat);
            return View(fileSubCat);
        }

        // POST: FileSubCats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            FileSubCat fileSubCat = db.FileSubCats.Find(id);
            db.FileSubCats.Remove(fileSubCat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

         public void PopulateAssignedCat(FileSubCat fileSubCat)
        {
            var allCats = db.FileCats;
            var veiwModel = new List<FileCatVM>();
            foreach (var m in allCats)
            {
                veiwModel.Add(new FileCatVM
                {
                    ID = m.ID,
                    FileCatName = m.FileCatName
                });
            }

            ViewBag.FileCats = veiwModel;
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
