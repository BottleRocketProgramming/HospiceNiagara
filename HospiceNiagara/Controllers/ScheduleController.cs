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
        [Authorize]
        public ActionResult Index(int? id)
        {
            var cUserRole = db.RoleLists;
            var sched = new Schedule();
            sched.SchedType = new SchedType();
            PopulateSchdType();
            PopulateAssignedRoles(sched);

            foreach (var r in cUserRole)
            {
                if (User.IsInRole(r.RoleName))
                {
                    var schd = db.Schedules.Include(a => a.ScheduleRoles);
                    schd = schd.Where(a => a.ScheduleRoles.Any(aur => aur.ID == r.ID));
                }
            }

            if (id != null)
            {
                Schedule schedule = db.Schedules.Include(j => j.SchedType).Where(i => i.ID == id).Single();
                PopulateScheduleTypes(schedule);
            }
            else
            {
                PopulateScheduleTypes(sched);
            }

            Schedule schedules = db.Schedules.Find(id);
            return View(schedules);
        }

        //Admin List
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminList()
        {
            return View(db.Schedules.ToList());
        }

        // GET: Schedule/Details/5
        [Authorize]
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
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var sched = new Schedule();
            sched.SchedType = new SchedType();
            PopulateScheduleTypes(sched);

            return View();
        }

        // POST: Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateSchedule")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "ID,SchedName,SchedLink,SchedStartDate,SchedEndDate")] Schedule schedule, int selectedSchedType)
        {
            try
            {
                    schedule.SchedType = new SchedType();
                    var typeToAdd = db.SchedTypes.Find(selectedSchedType);

                    schedule.SchedType = typeToAdd;
                
                if (ModelState.IsValid)
                {
                    db.Schedules.Add(schedule);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            PopulateScheduleTypes(schedule);
            return View(schedule);
        }



        // GET: Schedule/Edit/5
        [Authorize(Roles = "Administrator")]
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
            PopulateScheduleTypes(schedule);
            return View(schedule);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id, int selectedSchedType)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var scheduleToUpdate = db.Schedules.Include(a => a.SchedType).Where(i => i.ID == id).Single();

            if (TryUpdateModel(scheduleToUpdate, "", new string[] { "ID" , "SchedName" , "SchedLink" , "SchedStartDate" , "SchedEndDate" }))
            {
                try
                {
                    UpdateScheduleType(selectedSchedType, scheduleToUpdate);

                    if (ModelState.IsValid)
                    {
                        db.Entry(scheduleToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save after multiple attempts.  If problem persists, contact systems administrator");
                }
            }
            PopulateScheduleTypes(scheduleToUpdate);
            return View(scheduleToUpdate);
        }

        // GET: Schedule/Delete/5
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void PopulateAssignedRoles(Schedule schedule)
        {
            var allRole = db.RoleLists;
            var aRoles = new HashSet<int>(schedule.ScheduleRoles.Select(r => r.ID));
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

        private void UpdateScheduleRoles(string[] selectedRoles, Schedule ScheduleToUpdate)
        {
            if (selectedRoles == null)
            {
                ScheduleToUpdate.ScheduleRoles = new List<RoleList>();
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var scheduleRoles = new HashSet<int>
                (ScheduleToUpdate.ScheduleRoles.Select(c => c.ID));
            foreach (var rls in db.RoleLists)
            {
                if (selectedRolesHS.Contains(rls.ID.ToString()))
                {
                    if (!scheduleRoles.Contains(rls.ID))
                    {
                        ScheduleToUpdate.ScheduleRoles.Add(rls);
                    }
                }
                else
                {
                    if (scheduleRoles.Contains(rls.ID))
                    {
                        ScheduleToUpdate.ScheduleRoles.Remove(rls);
                    }
                }
            }
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

        private void UpdateScheduleType(int? selectedSchedType, Schedule ScheduleToUpdate)
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

        public void PopulateSchdType()
        {
            var allSchdTyp = db.SchedTypes.Include(sc => sc.Schedules);
            //var aSchdTyp = new HashSet<int>(allSchdTyp.s .FileSubCats.Select(r => r.ID));
            var viewModelSchTyp = new List<SchedTypeVM>();
            foreach (var schdTyp in allSchdTyp)
            {
                viewModelSchTyp.Add(new SchedTypeVM
                {
                    ID = schdTyp.ID,
                    SchedTypeName = schdTyp.SchedTypeName,
                    Schedules = schdTyp.Schedules
                });
            }

            var allSchd = db.Schedules;
            var viewModelSchd = new List<SchdVM>();

            foreach (var schd in allSchd)
            {
                viewModelSchd.Add(new SchdVM
                {
                    ID = schd.ID,
                    SchedName = schd.SchedName,
                    SchedLink = schd.SchedLink,
                    SchedType = schd.SchedType
                });
            }

            ViewBag.SchTyp = viewModelSchTyp;
            ViewBag.Schd = viewModelSchd;
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
