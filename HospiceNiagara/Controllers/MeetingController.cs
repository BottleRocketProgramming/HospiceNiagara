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
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;


//Andreas King March 2015
//Paul Boyko April 2015

namespace HospiceNiagara.Controllers
{
    public class MeetingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: Meeting
        [Authorize]
        public ActionResult Index(int? id)
        {
            var cUserRoles = db.RoleLists;
            var meet = new Meeting();
            meet.RolesLists = new List<RoleList>();
            meet.FileStores = new List<FileStorage>();
            PopulateAssignedFiles(meet);
            PopulateAssignedRoles(meet);
            
            var mttForList = db.Meetings.Where(m => m.ID == 0);            

            foreach(var u in cUserRoles)
            {
                if(User.IsInRole(u.RoleName))
                {
                    var mtt = db.Meetings.Include(a => a.RolesLists);
                    mtt = mtt.Where(a => a.RolesLists.Any(aur => aur.ID == u.ID) && a.EventDate >= DateTime.Today);
                    mttForList = mttForList.Concat(mtt);                    
                }
            }

            ViewData["Meeting"] = mttForList.ToList().Distinct();
            ViewData["MeetingID"] = id;
            Meeting meeting = db.Meetings.Find(id);

            return View(meeting);
        }

        //Admin List
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminList()
        {
            return View(db.Meetings.ToList());
        }

        // GET: Meeting/adminCreate
        [Authorize(Roles = "Administrator")]
        public ActionResult adminCreate()
        {
            var meet = new Meeting();
            meet.RolesLists = new List<RoleList>();
            meet.FileStores = new List<FileStorage>();
            PopulateAssignedRoles(meet);
            PopulateAssignedFiles(meet);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OnAction(ButtonName = "CreateMeeting")]
        [Authorize(Roles = "Administrator, Create/Modify Meetings or Events")]
        public ActionResult AdminCreate([Bind(Include = "ID,EventTitle,EventDiscription,EventLocation,EventDate,EventTime,EventRequirments,EventLinks")] Meeting meeting, string[] selectedRoles, string[] selectedFiles)
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
                    }
                }

                if (selectedFiles != null)
                {
                    meeting.FileStores = new List<FileStorage>();
                    foreach (var file in selectedFiles)
                    {
                        var fileToAdd = db.FileStorages.Find(int.Parse(file));
                        meeting.FileStores.Add(fileToAdd);
                    }
                }

                if (ModelState.IsValid)
                {
                    DateTime uploadDate = DateTime.Now;
                    string uploadedBy = User.Identity.Name;

                    meeting.UploadDate = uploadDate;
                    meeting.UploadedBy = uploadedBy;
                    db.Meetings.Add(meeting);
                    db.SaveChanges();
                }

                return RedirectToAction("AdminList");
            }

            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateAssignedRoles(meeting);
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
            

            var EventUserRoles = meeting.RolesLists;

            foreach (var ur in EventUserRoles)
            {
                if (User.IsInRole(ur.RoleName))
                {
                    PopulateAssignedFiles(meeting);
                    PopulateAssignedRSVP(meeting);
                    if (User.IsInRole("Administrator") || User.IsInRole("Manage Invitations"))
                    {
                        PopulateAllRSVPs(meeting);
                    }

                    return View(meeting);
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Meeting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateMeeting")]
        [Authorize(Roles = "Administrator, Create/Modify Meetings or Events")]
        public ActionResult Create([Bind(Include = "ID,EventTitle,EventDiscription,EventLocation,EventDate,EventTime,EventRequirments,EventLinks")] Meeting meeting, string[] selectedRoles, string[] selectedFiles)
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
                        //PopulateAssignedRoles(meeting);                       
                    }
                }

                if (selectedFiles != null)
                {
                    meeting.FileStores = new List<FileStorage>();
                    foreach (var file in selectedFiles)
                    {
                        var fileToAdd = db.FileStorages.Find(int.Parse(file));
                        meeting.FileStores.Add(fileToAdd);
                        //PopulateAssignedFiles(meeting);
                    }
                }

                if (ModelState.IsValid)
                {
                    DateTime uploadDate = DateTime.Now;
                    string uploadedBy = User.Identity.Name;

                    meeting.UploadDate = uploadDate;
                    meeting.UploadedBy = uploadedBy;
                    db.Meetings.Add(meeting);
                    db.SaveChanges();                   
                }
                    return RedirectToAction("Index");
            }
            
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateAssignedRoles(meeting);
            return View(meeting);


        }

        // GET: Meeting/Edit/5
        [Authorize(Roles = "Administrator, Create/Modify Meetings or Events")]
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
        [Authorize(Roles = "Administrator, Create/Modify Meetings or Events")]
        public ActionResult Edit(int? id, string[] selectedRoles, string[] selectedFiles)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meetingToUpdate = db.Meetings.Include(a => a.RolesLists).Where(i => i.ID == id).Single();

            if (TryUpdateModel(meetingToUpdate, "", new string[] { "ID", "EventTitle", "EventDiscription", "EventLocation", "EventDate", "EventTime", "EventRequirments", "EventLinks" }))
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
        [Authorize(Roles = "Administrator, Remove Records")]
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
        [Authorize(Roles = "Administrator, Remove Records")]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            db.Meetings.Remove(meeting);
            var meetingRsvpToRemove = db.MeetingUserRSVPs.Where(a => a.MeetingRSVP.ID == id);
            if(meetingRsvpToRemove != null)
            {
                foreach(var m in meetingRsvpToRemove)
                {
                    db.MeetingUserRSVPs.Remove(m);
                }
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ExportData(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            var RSVPlist = PopulateAssignedRSVPList(meeting);
            var RSVPforExcel = RSVPsforExport(RSVPlist);
            var filename = meeting.EventTitle + "_RSVPList.xls";

            GridView gv = new GridView();
            gv.DataSource = RSVPforExcel;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename));
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return Content(sw.ToString(), "application/vnd.ms-excel");
        }

        public void PopulateAllRSVPs(Meeting meeting)
        {
            var allRSVP = db.MeetingUserRSVPs.Where(r => r.MeetingRSVP.ID == meeting.ID).Include(r => r.AppUser).ToList();
            int amtRSVP = allRSVP.Count();
            int amtRSVPYes = 0;
            int amtRSVPNo = 0;
            foreach (var rsvp in allRSVP) { if (rsvp.ComingYorN.Equals(true)) { amtRSVPYes++; } }
            foreach (var rsvp in allRSVP) { if (rsvp.ComingYorN.Equals(false)) { amtRSVPNo++; } }
            ViewBag.amtRSVPNo = amtRSVPNo;
            ViewBag.amtRSVPYes = amtRSVPYes;
            ViewBag.amtRSVPs = amtRSVP;
            ViewBag.meetingRSVPs = allRSVP;
        }

        public void PopulateAssignedRSVP(Meeting meeting)
        {
            var allRSVP = db.MeetingUserRSVPs.Where(r => r.AppUser.UserName == User.Identity.Name);
            var aRSVP = new HashSet<int>(meeting.MeetingUserRSVPs.Select(r => r.ID));
            var meetingRSVP = new List<MeetingUserRSVP>();
            foreach (var rsvp in allRSVP)
            {
                if (aRSVP.Contains(rsvp.ID))
                {
                    meetingRSVP.Add(new MeetingUserRSVP
                    {
                        ID = rsvp.ID,
                        ComingYorN = rsvp.ComingYorN,
                        RSVPNotes = rsvp.RSVPNotes
                    });
                }

            }

            ViewBag.meetingRSVP = meetingRSVP;
        }

        public List<MeetingUserRSVP> PopulateAssignedRSVPList(Meeting meeting)
        {
            var allRSVP = db.MeetingUserRSVPs.Where(r => r.MeetingRSVP.ID == meeting.ID).Include(r => r.AppUser).ToList();
            var meetingRSVP = new List<MeetingUserRSVP>();
            foreach (var rsvp in allRSVP)
            {
                meetingRSVP.Add(new MeetingUserRSVP
                {
                    ID = rsvp.ID,
                    AppUser = rsvp.AppUser,
                    UserRequirements = rsvp.UserRequirements,
                    ComingYorN = rsvp.ComingYorN,
                    RSVPNotes = rsvp.RSVPNotes
                });
            }
            return meetingRSVP;
        }

        public List<RSVPforExport> RSVPsforExport(List<MeetingUserRSVP> meetingRSVP)
        {
            var RSVPsForExport = new List<RSVPforExport>();
            foreach(var rsvp in meetingRSVP)
            {
                var isAttending = "";
                if(rsvp.ComingYorN == true)
                {
                    isAttending = "Yes";
                }
                else if(rsvp.ComingYorN == false)
                {
                    isAttending = "No";
                }
                else
                {
                    isAttending = "No Response";
                }

                RSVPsForExport.Add(new RSVPforExport
                {
                    Name = rsvp.AppUser.UserFullName,
                    Email = rsvp.AppUser.Email,
                    Attending = isAttending,
                    Notes = rsvp.RSVPNotes,
                    UserRequirements = rsvp.UserRequirements
                });
            }

            return RSVPsForExport;
        }

//Empty Roles for Create
        public void PopulateAssignedRoles(Meeting meeting)
        {
            var allRole = db.RoleLists;
            var aRoles = new HashSet<int>(meeting.RolesLists.Select(r => r.ID));
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
            var allFile = db.FileStorages.Where(r => r.Meetings.Any(m => m.ID == meeting.ID)).OrderBy(r => r.FileDescription).ToList();
            ViewBag.FileStorages = allFile;
            ViewBag.selFiles = new MultiSelectList(allFile, "ID", "FileName");
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
