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
//Paul Boyko April 2015

namespace HospiceNiagara.Controllers
{
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contact
        [Authorize]
        public ActionResult Index(int? id)
        {
            BoardContact bc = new BoardContact();
            StaffContact sc = new StaffContact();
            PopulateAssignedUsers(bc);
            ViewData["Contact"] = db.BoardContacts.ToList();
            ViewData["StaffContact"] = db.StaffContacts.ToList();
            ViewData["ContactID"] = id;
            BoardContact contact = db.BoardContacts.Find(id);
            return View(contact);
        }

        //Admin List
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminList()
        {
            return View(db.BoardContacts.ToList());
        }

        // GET: Announcement/adminCreate
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminCreate()
        {
             BoardContact boardContact = new BoardContact();
            boardContact.AppUser = new ApplicationUser();
            PopulateAssignedUsers(boardContact);
            
            return View();

        }

        // GET: Contact/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardContact boardContact = db.BoardContacts.Find(id);
            if (boardContact == null)
            {
                return HttpNotFound();
            }
            return View(boardContact);
        }

        // GET: Contact/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            BoardContact boardContact = new BoardContact();
            boardContact.AppUser = new ApplicationUser();
            PopulateAssignedUsers(boardContact);

            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateContact")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "ID,BoardContPosition, BoardContWorkAddy, BoardContWorkPhone, BoardContCellPhone,BoardContFaxNum,BoardContPartnerName")] BoardContact boardContact, string selectedUsers)
        {
            try
            {
                if (selectedUsers != null)
                {
                    
                       var userToAdd = db.Users.Find(selectedUsers);
                        boardContact.AppUser = userToAdd;
                      
//                   
                }
                if (ModelState.IsValid)
                {
                    db.BoardContacts.Add(boardContact);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(boardContact);


        }

//        //Original Create

//        //public ActionResult Create([Bind(Include = "ID,BoardContPosition,BoardContHomeAddy,BoardContWorkAddy,BoardContHomePhone,BoardContWorkPhone,BoardContCellPhone,BoardContFaxNum,BoardContPartnerName")] BoardContact boardContact)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        db.BoardContacts.Add(boardContact);
//        //        db.SaveChanges();
//        //        return RedirectToAction("Index");
//        //    }

//        //    return View(boardContact);
//        //}


        // GET: /Default1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardContact boardcontact = db.BoardContacts.Find(id);
            if (boardcontact == null)
            {
                return HttpNotFound();
            }
            return View(boardcontact);
        }

        // POST: /Default1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BoardContPosition,BoardContWorkAddy,BoardContWorkPhone,BoardContCellPhone,BoardContFaxNum,BoardContPartnerName")] BoardContact boardcontact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boardcontact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boardcontact);
        }

        //// GET: Contact/Edit/5
        //[Authorize(Roles = "Administrator")]
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BoardContact boardContact = db.BoardContacts.Where(i => i.ID == id).Single();
        //    if (boardContact == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(boardContact);
        //}

        //// POST: Contact/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        //public ActionResult EditPost(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var boardCont = db.BoardContacts.Where(b => b.ID == id).Single();
            
        //    if (TryUpdateModel(new string[] { "ID", "BoardContPosition", "BoardContWorkAddy",  "BoardContWorkPhone", "BoardContCellPhone", "BoardContFaxNum", "BoardContPartnerName" }))
        //    {
               
        //        try
        //        {
                    
        //            if (ModelState.IsValid)
        //            {

        //                db.Entry(boardCont).State = EntityState.Modified;
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }
        //        }
        //        catch (RetryLimitExceededException)
        //        {
        //            ModelState.AddModelError("", "Unable to save after multiple attempts.  If problem persists, contact systems administrator");
        //        }
        //    }

        //    return View(boardCont);

        //}

        // GET: Contact/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardContact boardContact = db.BoardContacts.Find(id);
            if (boardContact == null)
            {
                return HttpNotFound();
            }
            return View(boardContact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            BoardContact boardContact = db.BoardContacts.Find(id);
            db.BoardContacts.Remove(boardContact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

//        public void PopulateJobDescriptions(BoardContact contact)
//        {
//            var jobDescriptions = db.JobDescriptions;
//            var aJobs = new HashSet<int>(contact.JobDescriptions.Select(r => r.ID));
//            var viewModel = new List<JobDescVM>();
//            foreach (var job in jobDescriptions)
//            {
//                viewModel.Add(new JobDescVM
//                {
//                    JobID = job.ID,
//                    JobName = job.JobTitle,
//                    JobDesc = job.JobDescpt,
//                    JobAssigned = aJobs.Contains(job.ID)
//                });
//            }

//            ViewBag.JobDescriptions = viewModel;
//        }

//        private void UpdateJobDescriptions(string[] selectedJobs, BoardContact ContactToUpdate)
//        {
//            if (selectedJobs == null)
//            {
//                ContactToUpdate.JobDescriptions = new List<JobDescription>();
//                return;
//            }

//            var selectedJobsHS = new HashSet<string>(selectedJobs);
//            var meetingJobs = new HashSet<int>(ContactToUpdate.JobDescriptions.Select(c => c.ID));

//            foreach (var jobs in db.JobDescriptions)
//            {
//                if (selectedJobsHS.Contains(jobs.ID.ToString()))
//                {
//                    if (!meetingJobs.Contains(jobs.ID))
//                    {
//                        ContactToUpdate.JobDescriptions.Add(jobs);
//                    }
//                }
//                else
//                {
//                    if (meetingJobs.Contains(jobs.ID))
//                    {
//                        ContactToUpdate.JobDescriptions.Remove(jobs);
//                    }
//                }
//            }
//        }

        public void PopulateAssignedUsers(BoardContact boardCont)
        {
            var allUsers = db.Users;
            var veiwModel = new List<UserVM>();
            foreach (var m in allUsers)
            {
                veiwModel.Add(new UserVM
                {
                    UserID = m.Id,
                    UserFName = m.UserFName,
                    UserLName = m.UserLName,
                    UserFullName = m.UserFName + " " + m.UserLName + ", " + m.UserName

                });
            }

            ViewBag.User = veiwModel;
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
