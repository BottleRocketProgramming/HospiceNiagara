﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospiceNiagara.Models;
using HospiceNiagara.ViewModels;
using PagedList;

//Andreas King March 2015

namespace HospiceNiagara.Controllers
{
    public class DeathNoticeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeathNotice
        [Authorize]
        public ActionResult Index(string sortOrder, int page = 1, int pageSize = 10)
        {
            PopulatePoems();
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_asc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            var deathnotices = db.DeathNotices.OrderByDescending(d => d.DnDate).ToList();
            

            switch (sortOrder)
            {
                case "name_desc":
                    deathnotices = deathnotices.OrderByDescending(d => d.DnLastName).ToList();
                    break;
                case "Name":
                    deathnotices = deathnotices.OrderBy(d => d.DnLastName).ToList();
                    break;
                case "date_asc":
                    deathnotices = deathnotices.OrderBy(d => d.DnDate).ToList();
                    break;
                default:
                    deathnotices = deathnotices.OrderByDescending(d => d.DnDate).ToList();
                    break;
            }

            PagedList<DeathNotice> DnsWithPage = new PagedList<DeathNotice>(deathnotices, page, pageSize);

            ViewData["DeathNoticeList"] = DnsWithPage;

            return View();
        }

        //Admin List
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminList(int page = 1, int pageSize = 10)
        {
            List<DeathNotice> Dns = db.DeathNotices.OrderByDescending(dn => dn.DnDate).ToList();
            PagedList<DeathNotice> DnsWithPage = new PagedList<DeathNotice>(Dns, page, pageSize);

            return View(DnsWithPage);
        }

        // GET: DeathNotice/Details/5
        [Authorize]
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
        [Authorize(Roles = "Administrator, Create/Modify Death Notices")]
        public ActionResult Create([Bind(Include = "ID,DnFirstName,DnMiddleName,DnLastName,DnDate,DnLocation,DnNotes,DnLink")] DeathNotice deathNotice)
        {
            if (ModelState.IsValid)
            {
                db.DeathNotices.Add(deathNotice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deathNotice);
        }

        //Admin Create for Death Notices
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult adminCreate([Bind(Include = "ID,DnFirstName,DnMiddleName,DnLastName,DnDate,DnLocation,DnNotes,DnLink")] DeathNotice deathNotice)
        {
            if (ModelState.IsValid)
            {
                db.DeathNotices.Add(deathNotice);
                db.SaveChanges();
                return RedirectToAction("AdminList");
            }

            return View(deathNotice);
        }

        // GET: DeathNotice/Edit/5
        [Authorize(Roles = "Administrator, Create/Modify Death Notices")]
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
        [Authorize(Roles = "Administrator, Create/Modify Death Notices")]
        public ActionResult Edit([Bind(Include = "ID,DnFirstName,DnMiddleName,DnLastName,DnDate,DnLocation,DnNotes,DnLink")] DeathNotice deathNotice)
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
        [Authorize(Roles = "Administrator, Remove Records")]
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
        [Authorize(Roles = "Administrator, Remove Records")]
        public ActionResult DeleteConfirmed(int id)
        {
            DeathNotice deathNotice = db.DeathNotices.Find(id);
            db.DeathNotices.Remove(deathNotice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void PopulatePoems()
        {
            var dnPoems = db.DeathNoticePoems;

            var viewModel = new List<DnPoemVM>();
            int c = 0;
            Random rand = new Random();
            foreach (var p in dnPoems)
            {
                viewModel.Add(new DnPoemVM
                {
                    ID = p.ID,
                    DnPoem = p.DnPoem,

                });
                c++;
            }
            int randIndex = rand.Next(c);

            ViewBag.DnPoem = viewModel[randIndex];

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
