﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospiceNiagara.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HospiceNiagara.ViewModels;

//Paul Boyko April 2015

namespace HospiceNiagara.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationDbContext context = new ApplicationDbContext();

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
                List<BoardContact> bCont = applicationUser.BoardContacts.ToList();
                List<StaffContact> sCont = applicationUser.StaffContacts.ToList();
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
                foreach(var b in bCont)
                {
                    var userBContToRemove = db.BoardContacts.Where(u => u.AppUser.Id == b.AppUser.Id).Single();
                    db.BoardContacts.Remove(userBContToRemove);
                }
                foreach(var s in sCont)
                {
                    var userSContToRemove = db.StaffContacts.Where(u => u.ContUser.Id == s.ContUser.Id).Single();
                    db.StaffContacts.Remove(userSContToRemove);
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
            PopulateAssignedRoles(user);
            return View(user);
        }

        // POST: FileCats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "ID,UserFName,UserMName,UserLName,UserDOB,UserAddress,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,Lockoutenabled,AccessFailedCount,UserName")]ApplicationUser user, string Id, string[] selectedRoles)
        {
           
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser aUser = manager.FindById(user.Id);
            var r = db.IdentUserRoles.Where(u => u.UserId == aUser.Id);
            aUser.UserFName = user.UserFName;
            aUser.UserMName = user.UserMName;
            aUser.UserLName = user.UserLName;
            aUser.UserAddress = user.UserAddress;
            aUser.PhoneNumber = user.PhoneNumber;
            //ApplicationUser user = manager.FindById(Id);
            //PopulateAssignedRoles(aUser);
            foreach(var ur in r)
            {
                db.IdentUserRoles.Remove(ur);
            }
            if (selectedRoles != null)
            {
                foreach (var aur in selectedRoles)
                {
                    var newRole = db.Roles.Where(a => a.Id == aur).Single();
                    var result = manager.AddToRole(aUser.Id, newRole.Name);

                }
            }

            
            if (ModelState.IsValid)
            {
                var result = manager.Update(aUser);
                //db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
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

        public void PopulateAssignedRoles(ApplicationUser applicationUser)
        {
            var allRole = db.Roles.OrderBy(r => r.Name);
            var aRoles = new HashSet<string>(applicationUser.Roles.Select(r => r.RoleId));
            var viewModel = new List<NetRollVM>();
            foreach (var roll in allRole)
            {
                viewModel.Add(new NetRollVM
                {
                    RoleID = roll.Id,
                    RoleName = roll.Name,                    
                    RoleAssigned = aRoles.Contains(roll.Id)
                });
            }

            ViewBag.RolesLists = viewModel;
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
