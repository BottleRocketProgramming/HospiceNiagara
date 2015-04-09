using System;
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
            var cUserRoles = db.RoleLists;
            var announce = new Announcement();
            announce.RolesLists = new List<RoleList>();
            announce.FileStorages = new List<FileStorage>();
            PopulateAssignedRoles(announce);
            PopulateAssignedFiles(announce);            
           
            var meetForList = db.Meetings.Where(m => m.ID == 0);
            var annForList = db.Announcements.Where(a => a.ID == 0);

           
                foreach (var ur in cUserRoles)
                {
                    if (User.IsInRole(ur.RoleName))
                    {
                        var ann = db.Announcements.Include(a => a.RolesLists).Include(a => a.FileStorages);
                        var meet = db.Meetings.Include(a => a.RolesLists);
                        ann = ann.Where(a => a.RolesLists.Any(aur => aur.ID == ur.ID));
                        meet = meet.Where(a => a.RolesLists.Any(aur => aur.ID == ur.ID));
                        meetForList = meetForList.Concat(meet);
                        annForList = annForList.Concat(ann);
                    }
                }
                

            ViewData["AnnouncementOrEvent"] = annForList.ToList().Distinct();
            ViewData["AnnOrEvntId"] = id;
            Announcement announcement = db.Announcements.Find(id);
            ViewData["Meeting"] = meetForList.ToList().Distinct();
            
            return View();
            
        }

        //Admin List
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminList()
        {
            return View(db.Announcements.ToList());
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
            PopulateAssignedFiles(announcement);
            return View(announcement);
        }


        // POST: Announcement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateAnnouncement")]
        [Authorize(Roles="Administrator")]
        public ActionResult Create([Bind(Include = "ID,AnnounceText,AnnounceEndDate,IsEvent")] Announcement announcement, string[] selectedRoles, string[] selectedFiles)
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
                        PopulateAssignedRoles(announcement);
                    }
                }
                if (selectedFiles != null)
                {
                    announcement.FileStorages = new List<FileStorage>();
                    foreach (var file in selectedFiles)
                    {
                        var fileToAdd = db.FileStorages.Find(int.Parse(file));
                        announcement.FileStorages.Add(fileToAdd);
                        PopulateAssignedFiles(announcement);
                    }
                }
                if (ModelState.IsValid)
                {
                    //announcement.IsEvent = false;
                    db.Announcements.Add(announcement);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            
            return View(announcement);
        }

       
        // GET: Announcement/Edit/5
        [Authorize(Roles="Administrator")]
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
        [Authorize(Roles="Administrator")]
        public ActionResult EditPost(int? id, string[] selectedRoles, string[] selectedFiles)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var announcementToUpdate = db.Announcements.Include(a => a.RolesLists).Where(i => i.ID == id).Single();

            if (TryUpdateModel(announcementToUpdate, "", new string[] { "ID", "AnnounceText", "AnnounceEndDate", "IsEvent" }))
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
                viewModel.Add(new RoleVM
                {
                    RoleID = roll.ID,
                    RoleName = roll.RoleName,
                    RoleAssigned = aRoles.Contains(roll.ID)
                });
            }

            ViewBag.RolesLists = viewModel;
        }

//Empty Files for Create

        public void PopulateAssignedFiles(Announcement announcement)
        {
            var allFile = db.FileStorages.OrderBy(r => r.FileDescription);
            var afiles = new HashSet<int>(announcement.FileStorages.Select(r => r.ID));
            var viewModelAvailible = new List<FileStorageVM>();
            var viewModelSelected = new List<FileStorageVM>();
            foreach (var file in allFile)
            {
                if (afiles.Contains(file.ID))
                {
                    viewModelSelected.Add(new FileStorageVM
                    {
                        ID = file.ID,
                        FileName = file.FileName,
                        FileDescription = file.FileDescription,
                        FileUploadDate = file.FileUploadDate
                    });
                }
                else
                {
                    viewModelAvailible.Add(new FileStorageVM
                    {
                        ID = file.ID,
                        FileName = file.FileName,
                        FileDescription = file.FileDescription,
                        FileUploadDate = file.FileUploadDate
                    });
                }
                
            }
            ViewBag.FileStorages = viewModelSelected;
            ViewBag.selFiles = new MultiSelectList(viewModelSelected, "ID", "FileName");
            ViewBag.avlFiles = new MultiSelectList(viewModelAvailible, "ID", "FileName");
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
                AnnouncementToUpdate.FileStorages = new List<FileStorage>();
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
