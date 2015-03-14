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
            sched.SchedType = new SchedType();

            if (id != null)
            {
                Schedule Schedule = db.Schedules.Include(j => j.SchedType).Where(i => i.ID == id).Single();
                PopulateScheduleTypes(Schedule);
            }
            else
            {
                PopulateScheduleTypes(sched);
            }

            ViewData["Schedule"] = db.Schedules.ToList();
            ViewData["ScheduleID"] = id;
            Schedule schedule = db.Schedules.Find(id);
            return View(schedule);
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
            var selected = schedule.SchedType;
            var viewModel = new List<SchedTypeVM>();
            foreach (var type in scheduleTypes)
            {
                viewModel.Add(new SchedTypeVM
                {
                    ID = type.ID,
                    SchedTypeName = type.SchedTypeName,
                    SchedTypeSelected = selected.Equals(type.ID)
                });
            }
            ViewBag.SchedTypes = viewModel;
        }

        private void UpdateScheduleType(int selectedSchedType, Schedule ScheduleToUpdate)
        {
            if (selectedSchedType == null)
            {
                ScheduleToUpdate.SchedType = null;
                return;
            }

            foreach (var type in db.SchedTypes)
            {
                if (selectedSchedType.Equals(type.ID))
                {
                    ScheduleToUpdate.SchedType = type;
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
