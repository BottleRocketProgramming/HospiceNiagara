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
    public class SchedTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SchedTypes
        public ActionResult Index()
        {
            return View(db.SchedTypes.ToList());
        }

        // GET: SchedTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedType schedType = db.SchedTypes.Find(id);
            if (schedType == null)
            {
                return HttpNotFound();
            }
            return View(schedType);
        }

        // GET: SchedTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchedTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SchedTypeName")] SchedType schedType)
        {
            if (ModelState.IsValid)
            {
                db.SchedTypes.Add(schedType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schedType);
        }

        // GET: SchedTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedType schedType = db.SchedTypes.Find(id);
            if (schedType == null)
            {
                return HttpNotFound();
            }
            return View(schedType);
        }

        // POST: SchedTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SchedTypeName")] SchedType schedType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schedType);
        }

        // GET: SchedTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedType schedType = db.SchedTypes.Find(id);
            if (schedType == null)
            {
                return HttpNotFound();
            }
            return View(schedType);
        }

        // POST: SchedTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchedType schedType = db.SchedTypes.Find(id);
            db.SchedTypes.Remove(schedType);
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