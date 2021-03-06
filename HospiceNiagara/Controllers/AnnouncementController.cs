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
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Infrastructure;
using System.Web.Security;
using PagedList;

//Paul Boyko Feb 2015
//Edits from Andreas King March 2015

namespace HospiceNiagara.Controllers
{
    public class AnnouncementController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Announcement
        [Authorize]
        public ActionResult Index(int? id)
        {
            var cUserRoles = db.RoleLists.Where(r => r.IsPerm == false);
            var announce = new Announcement();
            announce.RolesLists = new List<RoleList>();
            announce.FileStorages = new List<FileStorage>();
            PopulateAssignedRoles(announce);
            PopulateAssignedFiles(announce);            
            
            var annForList = db.Announcements.Where(a => a.ID == 0);

            foreach (var ur in cUserRoles)
            {
                if (User.IsInRole(ur.RoleName))
                {
                    var ann = db.Announcements.Include(a => a.RolesLists).Include(a => a.FileStorages).Where(a => a.RolesLists.Any(aur => aur.ID == ur.ID));
                    annForList = annForList.Concat(ann);
                }
            }

            annForList = annForList.Where(a => a.AnnounceEndDate >= DateTime.Today);

            ViewData["AnnouncementOrEvent"] = annForList.Distinct().OrderByDescending(a => a.UploadDate).ToList();
            
            return View();
        }

        //Admin List
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminList(int page = 1, int pageSize = 10)
        {
            List<Announcement> Anns = db.Announcements.OrderByDescending(a => a.UploadDate).ToList();
            PagedList<Announcement> AnnsWithPage = new PagedList<Announcement>(Anns, page, pageSize);

            return View(AnnsWithPage);
        }

        // GET: Announcement/adminCreate
        [Authorize(Roles = "Administrator")]
        public ActionResult adminCreate()
        {
            var announce = new Announcement();
            announce.RolesLists = new List<RoleList>();
            announce.FileStorages = new List<FileStorage>();
            PopulateAssignedRoles(announce);
            PopulateAssignedFiles(announce);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OnAction(ButtonName = "CreateAnnouncement")]
        [Authorize(Roles = "Administrator, Create/Modify Announcements")]
        public ActionResult AdminCreate([Bind(Include = "ID,AnnounceTitle,AnnounceText,AnnounceEndDate,IsEvent")] Announcement announcement, string[] selectedRoles, string[] selectedFiles)
        {
            try
            {
                if (selectedRoles != null)
                {
                    announcement.RolesLists = new List<RoleList>();
                    foreach (var role in selectedRoles)
                    {
                        var roleToAdd = db.RoleLists.Find(int.Parse(role));
                        announcement.RolesLists.Add(roleToAdd);
                    }
                    PopulateAssignedRoles(announcement);
                }
                if (selectedFiles != null)
                {
                    announcement.FileStorages = new List<FileStorage>();
                    foreach (var file in selectedFiles)
                    {
                        var fileToAdd = db.FileStorages.Find(int.Parse(file));
                        announcement.FileStorages.Add(fileToAdd);
                    }
                    PopulateAssignedFiles(announcement);
                }
                if (ModelState.IsValid)
                {
                    DateTime uploadDate = DateTime.Now;
                    string uploadedBy = User.Identity.Name;

                    announcement.UploadDate = uploadDate;
                    announcement.UploadedBy = uploadedBy;

                    db.Announcements.Add(announcement);
                    db.SaveChanges();
                    return RedirectToAction("AdminList");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(announcement);
        }

        // GET: Announcement/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }

            var AnnouncementUserRoles = announcement.RolesLists;

            foreach (var ur in AnnouncementUserRoles)
            {
                if (User.IsInRole(ur.RoleName))
                {
                    PopulateAssignedFiles(announcement);
                    return View(announcement);
                }
            }
            return RedirectToAction("Index");
        }


        // POST: Announcement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateAnnouncement")]
        [Authorize(Roles = "Administrator, Create/Modify Announcements")]
        public ActionResult Create([Bind(Include = "ID,AnnounceTitle,AnnounceText,AnnounceEndDate,IsEvent")] Announcement announcement, string[] selectedRoles, string[] selectedFiles)
        {
            try
            {
                if (selectedRoles != null)
                {
                    announcement.RolesLists = new List<RoleList>();
                    foreach (var role in selectedRoles)
                    {
                        var roleToAdd = db.RoleLists.Find(int.Parse(role));
                        announcement.RolesLists.Add(roleToAdd);
                    }
                    PopulateAssignedRoles(announcement);
                }
                if (selectedFiles != null)
                {
                    announcement.FileStorages = new List<FileStorage>();
                    foreach (var file in selectedFiles)
                    {
                        var fileToAdd = db.FileStorages.Find(int.Parse(file));
                        announcement.FileStorages.Add(fileToAdd);
                    }
                    PopulateAssignedFiles(announcement);
                }
                if (ModelState.IsValid)
                {
                    DateTime uploadDate = DateTime.Now;
                    string uploadedBy = User.Identity.Name;

                    announcement.UploadDate = uploadDate;
                    announcement.UploadedBy = uploadedBy;

                    db.Announcements.Add(announcement);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("Index");
        }

       
        // GET: Announcement/Edit/5
        [Authorize(Roles = "Administrator, Create/Modify Announcements")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Include(r=>r.RolesLists).Where(i=>i.ID == id).Single();
            if (announcement == null)
            {
                return HttpNotFound();
            }
            PopulateAssignedRoles(announcement);
            PopulateAssignedFiles(announcement);
            return View(announcement);
        }

        // POST: Announcement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Create/Modify Announcements")]
        public ActionResult EditPost(int? id, string[] selectedRoles, string[] selectedFiles)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var announcementToUpdate = db.Announcements.Include(a => a.RolesLists).Where(i => i.ID == id).Single();

            if (TryUpdateModel(announcementToUpdate, "", new string[] { "ID","AnnounceTitle", "AnnounceText", "AnnounceEndDate", "IsEvent" }))
            {
                try
                {
                    UpdateAnnouncementRoles(selectedRoles, announcementToUpdate);
                    UpdateAnnouncementFiles(selectedFiles, announcementToUpdate);

                    if (ModelState.IsValid)
                    {

                        db.Entry(announcementToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }                   
                }
                catch(RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save after multiple attempts.  If problem persists, contact systems administrator");
                }               
            }
            PopulateAssignedRoles(announcementToUpdate);
            PopulateAssignedFiles(announcementToUpdate);

            return View(announcementToUpdate);
           
        }

        // GET: Announcement/Delete/5
        [Authorize(Roles = "Administrator, Remove Records")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: Announcement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Remove Records")]
        public ActionResult DeleteConfirmed(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            db.Announcements.Remove(announcement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //empty Roles for create
        public void PopulateAssignedRoles(Announcement announcement)
        {
            var allRole = db.RoleLists.OrderBy(r => r.RoleName);
            var aRoles = new HashSet<int>(announcement.RolesLists.Select(r => r.ID));
            var viewModel = new List<RoleVM>();
            foreach (var roll in allRole)
            {
                if (roll.IsPerm == false)
                {
                    viewModel.Add(new RoleVM
                    {
                        RoleID = roll.ID,
                        RoleName = roll.RoleName,
                        RoleAssigned = aRoles.Contains(roll.ID),
                        IsPerm = roll.IsPerm
                    });
                }
            }

            ViewBag.RolesLists = viewModel;
        }

//Empty Files for Create

        public void PopulateAssignedFiles(Announcement announcement)
        {
            var allFile = db.FileStorages.Where(r => r.Announcements.Any(m => m.ID == announcement.ID)).OrderBy(r => r.FileDescription).ToList();
            ViewBag.FileStorages = allFile;
            ViewBag.selFiles = new MultiSelectList(allFile, "ID", "FileName");
        }
        
        //Roles for edit with already selected Roles
        private void UpdateAnnouncementRoles(string[] selectedRoles, Announcement AnnouncementToUpdate)
        {
            if (selectedRoles == null)
            {
                AnnouncementToUpdate.RolesLists = new List<RoleList>();
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var announcementRoles = new HashSet<int>
                (AnnouncementToUpdate.RolesLists.Select(c => c.ID));//IDs of the currently selected roles
            foreach (var rls in db.RoleLists)
            {
                if (selectedRolesHS.Contains(rls.ID.ToString()))
                {
                    if (!announcementRoles.Contains(rls.ID))
                    {
                        AnnouncementToUpdate.RolesLists.Add(rls);
                    }
                }
                else
                {
                    if (announcementRoles.Contains(rls.ID))
                    {
                        AnnouncementToUpdate.RolesLists.Remove(rls);
                    }
                }
            }
        }

//Files for Edit with ones already selected

        private void UpdateAnnouncementFiles(string[] selectedFiles, Announcement AnnouncementToUpdate)
        {
            if (selectedFiles == null)
            {
                AnnouncementToUpdate.FileStorages.Clear();
                return;
            }

            var selectedFilesHS = new HashSet<string>(selectedFiles);
            var announcementFiles = new HashSet<int>
                (AnnouncementToUpdate.FileStorages.Select(c => c.ID));//IDs of the currently selected roles
            foreach (var files in db.FileStorages)
            {
                if (selectedFilesHS.Contains(files.ID.ToString()))
                {
                    if (!announcementFiles.Contains(files.ID))
                    {
                        AnnouncementToUpdate.FileStorages.Add(files);
                    }
                }
                else
                {
                    if (announcementFiles.Contains(files.ID))
                    {
                        AnnouncementToUpdate.FileStorages.Remove(files);
                    }
                }
            }
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
