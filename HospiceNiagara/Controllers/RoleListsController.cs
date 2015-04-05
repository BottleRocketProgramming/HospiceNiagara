﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospiceNiagara.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;

namespace HospiceNiagara.Controllers
{
    public class RoleListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RoleLists
        public ActionResult Index()
        {
            return View(db.RoleLists.ToList());
        }

        // GET: RoleLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RoleName")] RoleList roleList)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                if(!db.Roles.Any(r => r.Name == roleList.RoleName))
                {
                    
                    var roleResult = roleManager.Create(new IdentityRole(roleList.RoleName));
                    if (roleResult.Succeeded)
                    {
                        db.RoleLists.Add(roleList);
                        db.SaveChanges();
                    }
                }
                
                return RedirectToAction("Index");
            }

            return View(roleList);
        }

       
        // GET: RoleLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleList roleList = db.RoleLists.Find(id);
            if (roleList == null)
            {
                return HttpNotFound();
            }
            return View(roleList);
        }

        // POST: RoleLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                RoleList roleList = db.RoleLists.Find(id);
                db.RoleLists.Remove(roleList);
                var thisRole = db.Roles.Where(r => r.Name.Equals(roleList.RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                db.Roles.Remove(thisRole);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Index"); }
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