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
            meetingUserRSVP = meetingUserRSVP.Where(u => u.AppUser.UserName == User.Identity.Name);


            return View(meetingUserRSVP);
        }

        //Admin List
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminList()
        {
            var orderedMeetRSVP = db.MeetingUserRSVPs.OrderBy(m => m.MeetingRSVP.ID).ToList();
            return View(orderedMeetRSVP);
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
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var meetRSVP = new MeetingUserRSVP();
            PopulateAssignedMeetings(meetRSVP);
            PopulateAssignedUsers(meetRSVP);
            return View();
        }

        // POST: MeetingUserRSVPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ComingYorN,RSVPNotes")] MeetingUserRSVP meetingUserRSVP, string[] selectedMeetings, string[] selectedUsers)
        {

           
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
                                db.SaveChanges();
                            }
                            else
                            {
                               ModelState.AddModelError("", "User " + userToAdd.UserFName +" " + userToAdd.UserLName + " has already been sent RSVP for this event");
                            }
                        }
                    }
                    
                }
            }

            
            
            if (ModelState.IsValid)
            {
                
                
                //PopulateAssignedMeetings(meetingUserRSVP);
                //PopulateAssignedUsers(meetingUserRSVP);
                return RedirectToAction("AdminList");
            }
            PopulateAssignedMeetings(meetingUserRSVP);
            PopulateAssignedUsers(meetingUserRSVP);
            return View(meetingUserRSVP);
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
        public ActionResult Edit([Bind(Include = "ID,ComingYorN,RSVPNotes")] MeetingUserRSVP meetingUserRSVP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetingUserRSVP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            var veiwModel = new List<MettingVM>();
            foreach(var m in allMeetings)
            {
                veiwModel.Add(new MettingVM
                {
                    MeetingId = m.ID,
                    MeetingName = m.EventTitle,
                    MeetingDesc = m.DescriptionTrimmed
                });
            }

            ViewBag.Meeting = veiwModel;
        }

        public void PopulateAssignedUsers(MeetingUserRSVP meetings)
        {
            var allUsers = db.Users;
            var viewModel = new List<UserVM>();
            foreach (var m in allUsers)
            {
                viewModel.Add(new UserVM
                {
                    UserID = m.Id,
                    UserFName = m.UserFName,
                    UserLName = m.UserLName,
                    UserFullName = m.UserFName + " " + m.UserLName + ", " + m.UserName 
                    
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
