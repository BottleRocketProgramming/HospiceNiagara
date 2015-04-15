using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospiceNiagara.Models;
using System.Web.Security;

//Paul Boyko April 2015

namespace HospiceNiagara.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        } 
      

        // GET: ApplicationUsers/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(string id)
        {
            //try
            //{
                ApplicationUser applicationUser = db.Users.Find(id);
                List<MeetingUserRSVP> uMeet = applicationUser.MeetingUserRSVPs.ToList();
                var uRole = applicationUser.Roles.ToList();
                foreach(var m in uMeet)
                {
                    var userMeetingToRemove = db.MeetingUserRSVPs.Where(r => r.AppUser.Id == m.AppUser.Id);
                    foreach(var uTr in userMeetingToRemove)
                    {
                        db.MeetingUserRSVPs.Remove(uTr);
                    }
                }
                foreach(var r in uRole)
                {
                    var rl = db.IdentUserRoles.Where(rs => rs.RoleId == r.RoleId);
                    rl = rl.Where(a => a.UserId == applicationUser.Id);
                    foreach(var rr in rl)
                    {
                        db.IdentUserRoles.Remove(rr);
                    }
                    
                }
                db.Users.Remove(applicationUser);
                db.SaveChanges();
                
            //}
            //catch
            //{

            //}

            return RedirectToAction("Index");
        }

        // GET: FileCats/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: FileCats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditPost([Bind(Include = "ID,UserFName,UserMName,UserLName,UserDOB,UserAddress,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,Lockoutenabled,AccessFailedCount,UserName")] string Id)
        {
            var user = db.Users.Find(Id);
            var has = user.PasswordHash;
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
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
