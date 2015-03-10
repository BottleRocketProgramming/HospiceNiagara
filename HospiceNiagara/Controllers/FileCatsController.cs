﻿using System;
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
    public class FileCatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FileCats
        public ActionResult Index()
        {
            return View(db.FileCats.ToList());
        }

        // GET: FileCats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileCat fileCat = db.FileCats.Find(id);
            if (fileCat == null)
            {
                return HttpNotFound();
            }
            return View(fileCat);
        }

        // GET: FileCats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileCats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FileCatName")] FileCat fileCat)
        {
            if (ModelState.IsValid)
            {
                db.FileCats.Add(fileCat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fileCat);
        }

        // GET: FileCats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileCat fileCat = db.FileCats.Find(id);
            if (fileCat == null)
            {
                return HttpNotFound();
            }
            return View(fileCat);
        }

        // POST: FileCats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FileCatName")] FileCat fileCat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileCat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fileCat);
        }

        // GET: FileCats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileCat fileCat = db.FileCats.Find(id);
            if (fileCat == null)
            {
                return HttpNotFound();
            }
            return View(fileCat);
        }

        // POST: FileCats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileCat fileCat = db.FileCats.Find(id);
            db.FileCats.Remove(fileCat);
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