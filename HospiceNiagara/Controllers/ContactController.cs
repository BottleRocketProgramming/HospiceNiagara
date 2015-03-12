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
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contact
        public ActionResult Index(int? id)
        {
            var cont = new BoardContact();
            cont.JobDescriptions = new List<JobDescription>();
            PopulateJobDescriptions(cont);
            var ctt = db.BoardContacts.Include(a => a.JobDescriptions);

            ViewData["Contact"] = db.BoardContacts.ToList();
            ViewData["ContactID"] = id;
            BoardContact contact = db.BoardContacts.Find(id);
            return View(contact);
        }

        public ActionResult ContactJobDesc(int? id)
        {
            ViewData["Contact"] = db.BoardContacts.ToList();
            ViewData["ContactID"] = id;
            BoardContact contact = db.BoardContacts.Find(id);
            return View(contact);
        }

        // GET: Contact/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateContact")]
        public ActionResult Create([Bind(Include = "ID,BoardContPosition,BoardContHomeAddy,BoardContWorkAddy,BoardContHomePhone,BoardContWorkPhone,BoardContCellPhone,BoardContFaxNum,BoardContPartnerName")] BoardContact boardContact, string[] selectedJobs)
        {
            try
            {
                if (selectedJobs != null)
                {
                    boardContact.JobDescriptions = new List<JobDescription>();
                    foreach (var job in selectedJobs)
                    {
                        var jobToAdd = db.JobDescriptions.Find(int.Parse(job));
                        boardContact.JobDescriptions.Add(jobToAdd);
                    }
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
            PopulateJobDescriptions(boardContact);
            return View(boardContact);


        }

        //Original Create

        //public ActionResult Create([Bind(Include = "ID,BoardContPosition,BoardContHomeAddy,BoardContWorkAddy,BoardContHomePhone,BoardContWorkPhone,BoardContCellPhone,BoardContFaxNum,BoardContPartnerName")] BoardContact boardContact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.BoardContacts.Add(boardContact);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(boardContact);
        //}


        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardContact boardContact = db.BoardContacts.Include(j => j.JobDescriptions).Where(i => i.ID == id).Single();
            if (boardContact == null)
            {
                return HttpNotFound();
            }
            PopulateJobDescriptions(boardContact);
            return View(boardContact);
        }

        // POST: Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedJobs)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var jobToUpdate = db.BoardContacts.Include(a => a.JobDescriptions).Where(i => i.ID == id).Single();

            if (TryUpdateModel(jobToUpdate, "", new string[] { "ID" , "BoardContPosition" , "BoardContHomeAddy" , "BoardContWorkAddy" , "BoardContHomePhone" , "BoardContWorkPhone" , "BoardContCellPhone" , "BoardContFaxNum" , "BoardContPartnerName" }))
            {
                try
                {
                    UpdateJobDescriptions(selectedJobs, jobToUpdate);

                    if (ModelState.IsValid)
                    {

                        db.Entry(jobToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save after multiple attempts.  If problem persists, contact systems administrator");
                }
            }
            PopulateJobDescriptions(jobToUpdate);
            return View(jobToUpdate);

        }

        // GET: Contact/Delete/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            BoardContact boardContact = db.BoardContacts.Find(id);
            db.BoardContacts.Remove(boardContact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void PopulateJobDescriptions(BoardContact contact)
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

        private void UpdateJobDescriptions(string[] selectedJobs, BoardContact ContactToUpdate)
        {
            if (selectedJobs == null)
            {
                ContactToUpdate.JobDescriptions = new List<JobDescription>();
                return;
            }

            var selectedJobsHS = new HashSet<string>(selectedJobs);
            var meetingJobs = new HashSet<int>
                (ContactToUpdate.JobDescriptions.Select(c => c.ID));
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
