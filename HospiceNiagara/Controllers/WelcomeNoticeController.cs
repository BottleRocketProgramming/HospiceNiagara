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
    public class WelcomeNoticeController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /WelcomeNotice/
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.WelcomeNotices.ToList());
        }

        //// GET: /WelcomeNotice/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    WelcomeNotice welcomenotice = db.WelcomeNotices.Find(id);
        //    if (welcomenotice == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(welcomenotice);
        //}

        //// GET: /WelcomeNotice/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /WelcomeNotice/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="ID,WelocomeNotice")] WelcomeNotice welcomenotice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.WelcomeNotices.Add(welcomenotice);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(welcomenotice);
        //}

        // GET: /WelcomeNotice/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WelcomeNotice welcomenotice = db.WelcomeNotices.Find(id);
            if (welcomenotice == null)
            {
                return HttpNotFound();
            }
            return View(welcomenotice);
        }

        // POST: /WelcomeNotice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,WelocomeNotice")] WelcomeNotice welcomenotice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(welcomenotice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(welcomenotice);
        }

        //// GET: /WelcomeNotice/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    WelcomeNotice welcomenotice = db.WelcomeNotices.Find(id);
        //    if (welcomenotice == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(welcomenotice);
        //}

        //// POST: /WelcomeNotice/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    WelcomeNotice welcomenotice = db.WelcomeNotices.Find(id);
        //    db.WelcomeNotices.Remove(welcomenotice);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
