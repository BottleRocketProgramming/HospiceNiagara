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
    public class DeathNoticePoemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /DeathNoticePoems/
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var dnPoem = db.DeathNoticePoems;
            return View(dnPoem.ToList());
        }

        // GET: /DeathNoticePoems/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeathNoticePoems deathnoticepoems = db.DeathNoticePoems.Find(id);
            if (deathnoticepoems == null)
            {
                return HttpNotFound();
            }
            return View(deathnoticepoems);
        }

        // GET: /DeathNoticePoems/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DeathNoticePoems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include="ID,DnPoem")] DeathNoticePoems deathnoticepoems)
        {
            if (ModelState.IsValid)
            {
                db.DeathNoticePoems.Add(deathnoticepoems);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deathnoticepoems);
        }

        // GET: /DeathNoticePoems/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeathNoticePoems deathnoticepoems = db.DeathNoticePoems.Find(id);
            if (deathnoticepoems == null)
            {
                return HttpNotFound();
            }
            return View(deathnoticepoems);
        }

        // POST: /DeathNoticePoems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,DnPoem")] DeathNoticePoems deathnoticepoems)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deathnoticepoems).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deathnoticepoems);
        }

        // GET: /DeathNoticePoems/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeathNoticePoems deathnoticepoems = db.DeathNoticePoems.Find(id);
            if (deathnoticepoems == null)
            {
                return HttpNotFound();
            }
            return View(deathnoticepoems);
        }

        // POST: /DeathNoticePoems/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeathNoticePoems deathnoticepoems = db.DeathNoticePoems.Find(id);
            db.DeathNoticePoems.Remove(deathnoticepoems);
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
