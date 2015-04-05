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

//Andreas King March 2015

namespace HospiceNiagara.Controllers
{
    public class MeetingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Meeting
        [Authorize]
        public ActionResult Index(int? id)
        {
            int c = 0;
            var cUserRoles = db.RoleLists;
            var meet = new Meeting();
            meet.RolesLists = new List<RoleList>();
            meet.FileStores = new List<FileStorage>();
            PopulateAssignedFiles(meet);
            PopulateAssignedRoles(meet);
            var mtt = db.Meetings.Include(a => a.RolesLists);
            mtt = db.Meetings.Include(a => a.FileStores);

            foreach(var u in cUserRoles)
            {
                if(User.IsInRole(u.RoleName))
                {
                    mtt = mtt.Where(a => a.RolesLists.Any(aur => aur.ID == u.ID));
                    c++;
                }
            }
            if( c == 0 )
            {
                mtt = mtt.Where(a => a.ID == 0);
            }

            ViewData["Meeting"] = mtt.ToList();
            ViewData["MeetingID"] = id;
            Meeting meeting = db.Meetings.Find(id);

            return View(meeting);
        }

        // GET: Meeting/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            PopulateAssignedFiles(meeting);
            return View(meeting);
        }

        // POST: Meeting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateMeeting")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "ID,EventTitle,EventDiscription,EventLocation,EventStart,EventEnd,EventRequirments,EventLinks")] Meeting meeting, string[] selectedRoles, string[] selectedFiles)
        {
            try
            {
                if (selectedRoles != null)
                {
                    meeting.RolesLists = new List<RoleList>();
                    foreach (var role in selectedRoles)
                    {
                        var roleToAdd = db.RoleLists.Find(int.Parse(role));
                        meeting.RolesLists.Add(roleToAdd);
                        PopulateAssignedRoles(meeting);
                    }
                }

                if (selectedFiles != null)
                {
                    meeting.FileStores = new List<FileStorage>();
                    foreach (var file in selectedRoles)
                    {
                        var fileToAdd = db.FileStorages.Find(int.Parse(file));
                        meeting.FileStores.Add(fileToAdd);
                        PopulateAssignedFiles(meeting);
                    }
                }

                if (ModelState.IsValid)
                {
                    db.Meetings.Add(meeting);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateAssignedRoles(meeting);
            return View(meeting);


        }

        // GET: Meeting/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Include(r => r.RolesLists).Where(i => i.ID == id).Single();
            if (meeting == null)
            {
                return HttpNotFound();
            }
            PopulateAssignedRoles(meeting);
            PopulateAssignedFiles(meeting);
            return View(meeting);
        }

        // POST: Meeting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id, string[] selectedRoles, string[] selectedFiles)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meetingToUpdate = db.Meetings.Include(a => a.RolesLists).Where(i => i.ID == id).Single();

            if (TryUpdateModel( meetingToUpdate,  "", new string[] { "ID" , "EventTitle" , "EventDiscription" , "EventLocation" , "EventStart" , "EventEnd" , "EventRequirments" , "EventLinks" }))
            {
                try
                {
                    UpdateMeetingRoles(selectedRoles, meetingToUpdate);
                    UpdateMeetingFiles(selectedFiles, meetingToUpdate);

                    if (ModelState.IsValid)
                    {

                        db.Entry(meetingToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save after multiple attempts.  If problem persists, contact systems administrator");
                }
            }
            PopulateAssignedRoles(meetingToUpdate);
            PopulateAssignedFiles(meetingToUpdate);
            return View(meetingToUpdate);

        }

        // GET: Meeting/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // POST: Meeting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            db.Meetings.Remove(meeting);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

//Empty Roles for Create
        public void PopulateAssignedRoles(Meeting meeting)
        {
            var allRole = db.RoleLists;
            var aRoles = new HashSet<int>(meeting.RolesLists.Select(r => r.ID));
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

//Roles for Edit
        private void UpdateMeetingRoles(string[] selectedRoles, Meeting MeetingToUpdate)
        {
            if (selectedRoles == null)
            {
                MeetingToUpdate.RolesLists = new List<RoleList>();
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var meetingRoles = new HashSet<int>
                (MeetingToUpdate.RolesLists.Select(c => c.ID));
            foreach (var rls in db.RoleLists)
            {
                if (selectedRolesHS.Contains(rls.ID.ToString()))
                {
                    if (!meetingRoles.Contains(rls.ID))
                    {
                        MeetingToUpdate.RolesLists.Add(rls);
                    }
                }
                else
                {
                    if (meetingRoles.Contains(rls.ID))
                    {
                        MeetingToUpdate.RolesLists.Remove(rls);
                    }
                }
            }
        }

//Empty Files for Create
        public void PopulateAssignedFiles(Meeting meeting)
        {
            var allFile = db.FileStorages.OrderBy(r => r.FileDescription);
            var afiles = new HashSet<int>(meeting.FileStores.Select(r => r.ID));
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

//Files for Edit
        private void UpdateMeetingFiles(string[] selectedFiles, Meeting MeetingToUpdate)
        {
            if (selectedFiles == null)
            {
                MeetingToUpdate.FileStores = new List<FileStorage>();
                return;
            }

            var selectedFilesHS = new HashSet<string>(selectedFiles);
            var meetingFiles = new HashSet<int>
                (MeetingToUpdate.FileStores.Select(c => c.ID));//IDs of the currently selected roles
            foreach (var files in db.FileStorages)
            {
                if (selectedFilesHS.Contains(files.ID.ToString()))
                {
                    if (!meetingFiles.Contains(files.ID))
                    {
                        MeetingToUpdate.FileStores.Add(files);
                    }
                }
                else
                {
                    if (meetingFiles.Contains(files.ID))
                    {
                        MeetingToUpdate.FileStores.Remove(files);
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
