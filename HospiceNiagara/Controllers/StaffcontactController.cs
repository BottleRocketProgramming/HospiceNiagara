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
using System.Data.Entity.Infrastructure;

namespace HospiceNiagara.Controllers
{
    public class StaffcontactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Staffcontact/
        public ActionResult Index()
        {
            return View(db.StaffContacts.ToList());
        }

        // GET: /Staffcontact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffContact staffcontact = db.StaffContacts.Find(id);
            if (staffcontact == null)
            {
                return HttpNotFound();
            }
            return View(staffcontact);
        }

        // GET: /Staffcontact/Create
        public ActionResult Create()
        {
            StaffContact staffContact = new StaffContact();
            staffContact.JobDescriptions = new List<JobDescription>();
            staffContact.ContUser = new ApplicationUser();
            PopulateAssignedUsers(staffContact);
            PopulateJobDescriptions(staffContact);
            return View();
        }

        // POST: /Staffcontact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ContExten, ContWorkCell")] StaffContact staffContact, string selectedUsers, string[] selectedJobs)
        {
           
            if(selectedUsers != null)
            {
                var userToAdd = db.Users.Find(selectedUsers);
                staffContact.ContUser = userToAdd;
                if (selectedJobs != null)
                {
                    staffContact.JobDescriptions = new List<JobDescription>();
                    foreach (var job in selectedJobs)
                    {
                        var jobToAdd = db.JobDescriptions.Find(int.Parse(job));
                        staffContact.JobDescriptions.Add(jobToAdd);
                    }
                }
               
                db.StaffContacts.Add(staffContact);
            }
            
            if (ModelState.IsValid)
            {
                db.StaffContacts.Add(staffContact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffContact);
        }

        // GET: /Staffcontact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffContact staffcontact = db.StaffContacts.Include(u => u.ContUser).Include(j=>j.JobDescriptions).Where(i=> i.ID == id).Single();
            if (staffcontact == null)
            {
                return HttpNotFound();
            }
            PopulateAssignedUsers(staffcontact);
            PopulateJobDescriptions(staffcontact);
            return View(staffcontact);
        }

        // POST: /Staffcontact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditPost(int? id, string[] selectedJobs)
        //"ID,ContExten,ContWorkCell"
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var staffContact = db.StaffContacts.Include(j => j.JobDescriptions).Include(u => u.ContUser).Where(i => i.ID == id).Single();

            if (TryUpdateModel(staffContact, "", new string[] { "ID","ContExten","ContWorkCell" }))
            {
                try
                {
                    UpdateJobDescriptions(selectedJobs, staffContact);

                    if (ModelState.IsValid)
                    {
                        db.Entry(staffContact).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save after multiple attempts.  If problem persists, contact systems administrator");
                }
                catch
                {
                    ModelState.AddModelError("", "Unable to save after multiple attempts.  If problem persists, contact systems administrator");
                }
            }

            PopulateAssignedUsers(staffContact);
            PopulateJobDescriptions(staffContact);

            return View(staffContact);
        }

        // GET: /Staffcontact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffContact staffcontact = db.StaffContacts.Find(id);
            if (staffcontact == null)
            {
                return HttpNotFound();
            }
            return View(staffcontact);
        }

        // POST: /Staffcontact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffContact staffcontact = db.StaffContacts.Find(id);
            db.StaffContacts.Remove(staffcontact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void PopulateAssignedUsers(StaffContact staffCont)
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
    
        public void PopulateJobDescriptions(StaffContact contact)
        {
            var jobDescriptions = db.JobDescriptions;
            var aJobs = new HashSet<int>(contact.JobDescriptions.Select(r => r.ID));
            var viewModel = new List<JobDescVM>();
            foreach (var job in jobDescriptions)
            {
                viewModel.Add(new JobDescVM
                {
                    JobID = job.ID,
                    JobName = job.JobTitle,
                    JobDesc = job.JobDescpt,
                    JobAssigned = aJobs.Contains(job.ID)
                });
            }

            ViewBag.JobDescriptions = viewModel;
        }

        private void UpdateJobDescriptions(string[] selectedJobs, StaffContact ContactToUpdate)
        {
            if (selectedJobs == null)
            {
                ContactToUpdate.JobDescriptions = new List<JobDescription>();
                return;
            }

            var selectedJobsHS = new HashSet<string>(selectedJobs);
            var meetingJobs = new HashSet<int>(ContactToUpdate.JobDescriptions.Select(c => c.ID));

            foreach (var jobs in db.JobDescriptions)
            {
                if (selectedJobsHS.Contains(jobs.ID.ToString()))
                {
                    if (!meetingJobs.Contains(jobs.ID))
                    {
                        ContactToUpdate.JobDescriptions.Add(jobs);
                    }
                }
                else
                {
                    if (meetingJobs.Contains(jobs.ID))
                    {
                        ContactToUpdate.JobDescriptions.Remove(jobs);
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
