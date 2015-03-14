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
    public class ScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Schedule
        public ActionResult Index(int? id)
        {
            var sched = new Schedule();
            sched.SchedTypes = new List<SchedType>();

            return View(db.Schedules.ToList());
        }

        // GET: Schedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SchedName,SchedLink,SchedStartDate,SchedEndDate")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schedule);
        }

        // GET: Schedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SchedName,SchedLink,SchedStartDate,SchedEndDate")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schedule);
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void PopulateScheduleTypes(Schedule schedule)
        {
            var scheduleTypes = db.SchedTypes;
            var aTypes = new HashSet<int>(schedule.SchedTypes.Select(t => t.ID));
            var viewModel = new List<SchedTypeVM>();
            foreach (var type in scheduleTypes)
            {
                viewModel.Add(new SchedTypeVM
                {
                    ID = type.ID,
                    SchedTypeName = type.SchedTypeName,
                    SchedTypeSelected = aTypes.Contains(type.ID)
                });
            }
            ViewBag.SchedTypes = viewModel;
        }

        private void UpdateScheduleTypes(string[] selectedSchedType, Schedule ScheduleToUpdate)
        {
            if (selectedSchedType == null)
            {
                ScheduleToUpdate.SchedTypes = new List<JobDescription>();
                return;
            }

            var selectedJobsHS = new HashSet<string>(selectedSchedType);
            var meetingJobs = new HashSet<int>
                (ScheduleToUpdate.SchedTypes.Select(c => c.ID));
            foreach (var jobs in db.SchedTypes)
            {
                if (selectedJobsHS.Contains(jobs.ID.ToString()))
                {
                    if (!meetingJobs.Contains(jobs.ID))
                    {
                        ScheduleToUpdate.SchedTypes.Add(jobs);
                    }
                }
                else
                {
                    if (meetingJobs.Contains(jobs.ID))
                    {
                        ScheduleToUpdate.SchedTypes.Remove(jobs);
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
