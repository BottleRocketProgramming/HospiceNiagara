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
            sched.ScheduleRoles = new List<RoleList>();
            PopulateSchdType();
            PopulateAssignedRoles(sched);

            var schedForList = db.Schedules.Where(a => a.ID == 0);
            var schedTypeForList = db.SchedTypes.Where(i => i.ID == 0);

            foreach (var r in cUserRole)
            {
                if (User.IsInRole(r.RoleName))
                {
                    var schd = db.Schedules.Include(a => a.ScheduleRoles);
                    var schdTyp = db.SchedTypes.Include(b => b.RoleLists);
                    schdTyp = schdTyp.Where(a => a.RoleLists.Any(aur => aur.ID == r.ID));
                    schd = schd.Where(a => a.ScheduleRoles.Any(aur => aur.ID == r.ID));
                    schedForList = schedForList.Concat(schd);
                    schedTypeForList = schedTypeForList.Concat(schdTyp);
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

            ViewData["Schedules"] = schedForList.ToList().Distinct();
            ViewData["ScheduleTypes"] = schedTypeForList.ToList().Distinct();

            return View();
        }

        //Admin List
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminList()
        {
            var sched = new Schedule();
            sched.SchedType = new SchedType();
            sched.ScheduleRoles = new List<RoleList>();
            PopulateScheduleTypes(sched);
            PopulateAssignedRoles(sched);
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
        [Authorize(Roles = "Administrator, Create/Modify Schedules")]
        public ActionResult AdminCreate()
        {
            var sched = new Schedule();
            sched.SchedType = new SchedType();
            sched.ScheduleRoles = new List<RoleList>();
            PopulateScheduleTypes(sched);
            PopulateAssignedRoles(sched);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Create/Modify Schedules")]
        public ActionResult AdminCreate([Bind(Include = "ID,SchedName,SchedLink")] Schedule schedule, int selectedSchedType, string[] selectedRoles)
        {
            try
            {
                schedule.SchedType = new SchedType();
                var typeToAdd = db.SchedTypes.Find(selectedSchedType);

                schedule.SchedType = typeToAdd;

                if (selectedRoles != null)
                {
                    schedule.ScheduleRoles = new List<RoleList>();
                    foreach (var role in selectedRoles)
                    {
                        var roleToAdd = db.RoleLists.Find(int.Parse(role));
                        schedule.ScheduleRoles.Add(roleToAdd);
                        PopulateAssignedRoles(schedule);
                    }
                }

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
            PopulateAssignedRoles(schedule);
            return View(schedule);
        }


        // POST: Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateSchedule")]
        [Authorize(Roles = "Administrator, Create/Modify Schedules")]
        public ActionResult Create([Bind(Include = "ID,SchedName,SchedLink")] Schedule schedule, int selectedSchedType, string[] selectedRoles)
        {
            try
            {
                    schedule.SchedType = new SchedType();
                    var typeToAdd = db.SchedTypes.Find(selectedSchedType);

                    schedule.SchedType = typeToAdd;

                    if (selectedRoles != null)
                    {
                        schedule.ScheduleRoles = new List<RoleList>();
                        foreach (var role in selectedRoles)
                        {
                            var roleToAdd = db.RoleLists.Find(int.Parse(role));
                            schedule.ScheduleRoles.Add(roleToAdd);
                            PopulateAssignedRoles(schedule);
                        }
                    }
                
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
            PopulateAssignedRoles(schedule);
            return View(schedule);
        }



        // GET: Schedule/Edit/5
        [Authorize(Roles = "Administrator, Create/Modify Schedules")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Include(r => r.ScheduleRoles).Where(i => i.ID == id).Single();
            if (schedule == null)
            {
                return HttpNotFound();
            }
            PopulateScheduleTypes(schedule);
            PopulateAssignedRoles(schedule);
            return View(schedule);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Create/Modify Schedules")]
        public ActionResult Edit(int? id, int selectedSchedType, string[] selectedRoles)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var scheduleToUpdate = db.Schedules.Include(a => a.ScheduleRoles).Where(i => i.ID == id).Single();

            if (TryUpdateModel(scheduleToUpdate, "", new string[] { "ID" , "SchedName" , "SchedLink" }))
            {
                try
                {
                    UpdateScheduleType(selectedSchedType, scheduleToUpdate);
                    UpdateScheduleRoles(selectedRoles, scheduleToUpdate);
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
            PopulateAssignedRoles(scheduleToUpdate);
            return View(scheduleToUpdate);
        }

        // GET: Schedule/Delete/5
        [Authorize(Roles = "Administrator, Remove Records")]
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
        [Authorize(Roles = "Administrator, Remove Records")]
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
                    RoleAssigned = aRoles.Contains(roll.ID),
                    IsPerm = roll.IsPerm
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
            var scheduleRoles = new HashSet<int> (ScheduleToUpdate.ScheduleRoles.Select(c => c.ID));
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
