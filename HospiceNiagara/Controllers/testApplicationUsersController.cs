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
    public class testApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: testApplicationUsers
        public ActionResult Index()
        {
           
            return View(db.Users.ToList());
        }

        ////// GET: testApplicationUsers/Details/5
        ////public ActionResult Details(string id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ////    }
        ////    ApplicationUser applicationUser = db.IdentityUsers.Find(id);
        ////    if (applicationUser == null)
        ////    {
        ////        return HttpNotFound();
        ////    }
        ////    return View(applicationUser);
        ////}

        ////// GET: testApplicationUsers/Create
        ////public ActionResult Create()
        ////{
        ////    return View();
        ////}

        ////// POST: testApplicationUsers/Create
        ////// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        ////// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,UserFName,UserMName,UserLName,UserDOB,UserAddress")] ApplicationUser applicationUser)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        db.IdentityUsers.Add(applicationUser);
        ////        db.SaveChanges();
        ////        return RedirectToAction("Index");
        ////    }

        ////    return View(applicationUser);
        ////}

        ////// GET: testApplicationUsers/Edit/5
        ////public ActionResult Edit(string id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ////    }
        ////    ApplicationUser applicationUser = db.IdentityUsers.Find(id);
        ////    if (applicationUser == null)
        ////    {
        ////        return HttpNotFound();
        ////    }
        ////    return View(applicationUser);
        ////}

        ////// POST: testApplicationUsers/Edit/5
        ////// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        ////// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,UserFName,UserMName,UserLName,UserDOB,UserAddress")] ApplicationUser applicationUser)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        db.Entry(applicationUser).State = EntityState.Modified;
        ////        db.SaveChanges();
        ////        return RedirectToAction("Index");
        ////    }
        ////    return View(applicationUser);
        ////}

        ////// GET: testApplicationUsers/Delete/5
        ////public ActionResult Delete(string id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ////    }
        ////    ApplicationUser applicationUser = db.IdentityUsers.Find(id);
        ////    if (applicationUser == null)
        ////    {
        ////        return HttpNotFound();
        ////    }
        ////    return View(applicationUser);
        ////}

        ////// POST: testApplicationUsers/Delete/5
        ////[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult DeleteConfirmed(string id)
        ////{
        ////    ApplicationUser applicationUser = db.IdentityUsers.Find(id);
        ////    db.IdentityUsers.Remove(applicationUser);
        ////    db.SaveChanges();
        ////    return RedirectToAction("Index");
        ////}

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
