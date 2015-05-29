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
    public class SchedTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SchedTypes
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            SchedType schdTyp = new SchedType();
            schdTyp.RoleLists = new List<RoleList>();
            PopulateAssignedRoles(schdTyp);
            return View(db.SchedTypes.ToList());
        }

        // GET: SchedTypes/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedType schedType = db.SchedTypes.Find(id);
            if (schedType == null)
            {
                return HttpNotFound();
            }
            return View(schedType);
        }

        // GET: SchedTypes/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            SchedType schdTyp = new SchedType();
            schdTyp.RoleLists = new List<RoleList>();
            PopulateAssignedRoles(schdTyp);
            return View();
        }

        // POST: SchedTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "ID,SchedTypeName")] string[] selectedRoles, SchedType schedType)
        {
            try
            {
                if (selectedRoles != null)
                {
                    schedType.RoleLists = new List<RoleList>();
                    foreach (var role in selectedRoles)
                    {
                        var roleToAdd = db.RoleLists.Find(int.Parse(role));
                        schedType.RoleLists.Add(roleToAdd);
                        PopulateAssignedRoles(schedType);
                    }
                }
                if (ModelState.IsValid)
                {
                    db.SchedTypes.Add(schedType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
         

            return View(schedType);
        }

        // GET: SchedTypes/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedType schedType = db.SchedTypes.Find(id);
            if (schedType == null)
            {
                return HttpNotFound();
            }
            PopulateAssignedRoles(schedType);
            return View(schedType);
        }

        // POST: SchedTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id, string[] selectedRoles)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var scheduleTypeToUpdate = db.SchedTypes.Include(a => a.RoleLists).Where(i => i.ID == id).Single();

            if (TryUpdateModel(scheduleTypeToUpdate, "", new string[] { "ID", "SchedTypeName" }))
            {
                try
                {

                    UpdateScheduleRoles(selectedRoles, scheduleTypeToUpdate);
                    if (ModelState.IsValid)
                    {
                        db.Entry(scheduleTypeToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save after multiple attempts.  If problem persists, contact systems administrator");
                }
            }

            PopulateAssignedRoles(scheduleTypeToUpdate);
            return View(scheduleTypeToUpdate);
        }
            
            
            
        //    [Bind(Include = "ID,SchedTypeName")] SchedType schedType)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(schedType).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(schedType);
        //}

        // GET: SchedTypes/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedType schedType = db.SchedTypes.Find(id);
            if (schedType == null)
            {
                return HttpNotFound();
            }
            return View(schedType);
        }

        // POST: SchedTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            SchedType schedType = db.SchedTypes.Find(id);
            db.SchedTypes.Remove(schedType);
            db.SaveChanges();
            return RedirectToAction("Index");
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

        public void PopulateAssignedRoles(SchedType schedule)
        {
            var allRole = db.RoleLists;
            var aRoles = new HashSet<int>(schedule.RoleLists.Select(r => r.ID));
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

        private void UpdateScheduleRoles(string[] selectedRoles, SchedType ScheduleToUpdate)
        {
            if (selectedRoles == null)
            {
                ScheduleToUpdate.RoleLists = new List<RoleList>();
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var scheduleRoles = new HashSet<int>(ScheduleToUpdate.RoleLists.Select(c => c.ID));
            foreach (var rls in db.RoleLists)
            {
                if (selectedRolesHS.Contains(rls.ID.ToString()))
                {
                    if (!scheduleRoles.Contains(rls.ID))
                    {
                        ScheduleToUpdate.RoleLists.Add(rls);
                    }
                }
                else
                {
                    if (scheduleRoles.Contains(rls.ID))
                    {
                        ScheduleToUpdate.RoleLists.Remove(rls);
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
