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
using System.Net.Mail;

//Paul Boyko April 2015

namespace HospiceNiagara.Controllers
{
    public class MeetingUserRSVPsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MeetingUserRSVPs
        [Authorize]
        public ActionResult Index()
        {
            var meetingUserRSVP = db.MeetingUserRSVPs.Include(u => u.AppUser).Include(m => m.MeetingRSVP);
            meetingUserRSVP = meetingUserRSVP.Where(u => u.AppUser.UserName == User.Identity.Name).Where(u => u.MeetingRSVP.EventDate >= DateTime.Today);
            
            return View(meetingUserRSVP);
        }

        //Admin List
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminList()
        {
            var orderedMeetRSVP = db.MeetingUserRSVPs.OrderBy(m => m.MeetingRSVP.ID).ToList();
            return View(orderedMeetRSVP);
        }

        public ActionResult GetCount()
        {
            var count = db.MeetingUserRSVPs.Count(p => p.AppUser.UserName == User.Identity.Name && p.ComingYorN == null && p.MeetingRSVP.EventDate >= DateTime.Today);

            return PartialView(count);
        }

        // GET: MeetingUserRSVPs/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingUserRSVP meetingUserRSVP = db.MeetingUserRSVPs.Find(id);
            if (meetingUserRSVP == null)
            {
                return HttpNotFound();
            }
            return View(meetingUserRSVP);
        }

        // GET: MeetingUserRSVPs/Create
        [Authorize(Roles = "Administrator, Manage Invitations")]
        public ActionResult Create(int? id, string roles)
        {
            var meeting = db.Meetings.Where(m => m.ID == id).ToList();
            var meetRSVP = new MeetingUserRSVP();
            var allUsers = db.Users.Include(u => u.Roles);
            var uRoles = db.Roles;
            var perms = db.RoleLists.Where(r => r.IsPerm == false).Select(r=> r.RoleName);
            var filteredRoles = uRoles.Where(r => perms.Contains(r.Name));

            if (!string.IsNullOrEmpty(roles))
            {
                allUsers = allUsers.Where(u => u.Roles.Any(r => r.RoleId == roles));
            }

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in filteredRoles)
            {
                list.Add(new SelectListItem() { Value = role.Id.ToString(), Text = role.Name });
            }

            var users = allUsers.ToList();

            ViewBag.Roles = list;
            ViewBag.User = users;
            ViewBag.Meeting = meeting;
            ViewBag.MeetingID = id;
            return View();
        }



        // POST: MeetingUserRSVPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator, Manage Invitations")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ComingYorN,RSVPNotes,AdminRequirements,UserRequirements")] MeetingUserRSVP meetingUserRSVP, string[] selectedMeetings, string[] selectedUsers)
        {
            int meetingID = 0;
            if(selectedMeetings != null)
            {
                foreach(var m in selectedMeetings)
                {
                   var meetingToAdd = db.Meetings.Find(int.Parse(m));
                    //meetingUserRSVP.MeetingRSVP = meetingToAdd;

                    if (selectedUsers != null)
                    {
                        foreach (var u in selectedUsers)
                        {
                            var userToAdd = db.Users.Find(u);
                            if (!db.MeetingUserRSVPs.Any(us => us.AppUser.Id == u && us.MeetingRSVP.ID == meetingToAdd.ID))
                            {
                                meetingUserRSVP.MeetingRSVP = meetingToAdd;
                                meetingUserRSVP.AppUser = userToAdd;
                                db.MeetingUserRSVPs.Add(meetingUserRSVP);

                                MailMessage eMail = new MailMessage();
                                eMail.From = new MailAddress("noreply@hospiceniagaraportal.ca");
                                eMail.To.Add(new MailAddress(userToAdd.Email));
                                eMail.Subject = "You've been invited to " + meetingToAdd.EventTitle;
                                eMail.Body = String.Format("Dear " + userToAdd.UserFullName + ",<br /> <br /><strong>You have been invited to an event:</strong> <br /> <br /> " + meetingToAdd.EventTitle+ " <br />at " + meetingToAdd.EventLocation + "<br /> " + meetingToAdd.EventDateString + " " + meetingToAdd.EventTime + "<br/><a href='http://hospiceniagaraportal.ca/Meeting/Details/" + meetingToAdd.ID+"'>Follow this link to log in and RSVP to the event</a> <br />Please do not reply to this email!");
                                eMail.IsBodyHtml = true;
                                SmtpClient smtp = new System.Net.Mail.SmtpClient("relay-hosting.secureserver.net");
                                smtp.Send(eMail);

                                db.SaveChanges();
                            }
                            else
                            {
                               ModelState.AddModelError("", "User " + userToAdd.UserFName +" " + userToAdd.UserLName + " has already been sent RSVP for this event");
                            }
                        }
                    }
                    meetingID = int.Parse(m);
                }
            }
            var uRoles = db.Roles;
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in uRoles)
            {
                list.Add(new SelectListItem() { Value = role.Id.ToString(), Text = role.Name });
            }
            PopulateAssignedMeetings(meetingUserRSVP);
            PopulateAssignedUsers(meetingUserRSVP);
            ViewBag.MeetingID = meetingID;
            ViewBag.Roles = list;
            return RedirectToAction("Details", "Meeting", new { id = meetingID });
        }

        // GET: MeetingUserRSVPs/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingUserRSVP MuRSVP = db.MeetingUserRSVPs.Include(m => m.MeetingRSVP).Include(u => u.AppUser).Where(i => i.ID == id).Single(); 
            
            if (MuRSVP == null)
            {
                return HttpNotFound();
            }
            return View(MuRSVP);
        }

        // POST: MeetingUserRSVPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ComingYorN,RSVPNotes,UserRequirements,AdminRequirements")] MeetingUserRSVP meetingUserRSVP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetingUserRSVP).State = EntityState.Modified;
                db.SaveChanges();
                var rsvpID = meetingUserRSVP.ID;
                MeetingUserRSVP MuRSVP = db.MeetingUserRSVPs.Include(m => m.MeetingRSVP).Where(i => i.ID == rsvpID).Single();
                return RedirectToAction("Details", "Meeting", new { id = MuRSVP.MeetingRSVP.ID });
            }
            return View(meetingUserRSVP);
        }

        //// GET: MeetingUserRSVPs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MeetingUserRSVP meetingUserRSVP = db.MeetingUserRSVPs.Find(id);
        //    if (meetingUserRSVP == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(meetingUserRSVP);
        //}

        //// POST: MeetingUserRSVPs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MeetingUserRSVP meetingUserRSVP = db.MeetingUserRSVPs.Find(id);
        //    db.MeetingUserRSVPs.Remove(meetingUserRSVP);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public void PopulateAssignedMeetings(MeetingUserRSVP meetings)
        {
            var allMeetings = db.Meetings;            
            var viewmodel = new List<Meeting>();
            foreach(var m in allMeetings)
            {
                viewmodel.Add(new Meeting
                {
                    ID = m.ID,
                    EventTitle = m.EventTitle,
                    EventDiscription = m.DescriptionTrimmed
                });
            }

            ViewBag.Meeting = viewmodel;
        }

        public void PopulateAssignedUsers(MeetingUserRSVP meetings)
        {
            var allUsers = db.Users;
            var viewModel = new List<ApplicationUser>();
            foreach (var m in allUsers)
            {
                viewModel.Add(new ApplicationUser
                {
                    Id = m.Id,
                    UserFName = m.UserFName,
                    UserLName = m.UserLName
                });
            }

            ViewBag.User = viewModel;
        }

        //public void PopulateAssignedUsers(MeetingUserRSVP meetings)
        //{
        //    var allUsers = db.Users;
        //    var aUsers = new HashSet<string>(meetings.);
        //    var viewModelAvailible = new List<FileStorageVM>();
        //    var viewModelSelected = new List<FileStorageVM>();
        //    foreach (var file in allFile)
        //    {
        //        if (aUsers.Contains(file.ID))
        //        {
        //            viewModelSelected.Add(new FileStorageVM
        //            {
        //                ID = file.ID,
        //                FileName = file.FileName,
        //                FileDescription = file.FileDescription,
        //                FileUploadDate = file.FileUploadDate
        //            });
        //        }
        //        else
        //        {
        //            viewModelAvailible.Add(new FileStorageVM
        //            {
        //                ID = file.ID,
        //                FileName = file.FileName,
        //                FileDescription = file.FileDescription,
        //                FileUploadDate = file.FileUploadDate
        //            });
        //        }

        //    }
        //    ViewBag.FileStorages = viewModelSelected;
        //    ViewBag.selFiles = new MultiSelectList(viewModelSelected, "ID", "FileName");
        //    ViewBag.avlFiles = new MultiSelectList(viewModelAvailible, "ID", "FileName");
        //}

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
