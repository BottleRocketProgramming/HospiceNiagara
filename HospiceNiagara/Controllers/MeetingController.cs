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

//Andreas King March 2015

namespace HospiceNiagara.Controllers
{
    public class MeetingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Meeting
        public ActionResult Index(int? id)
        {
            var meet = new Meeting();
            meet.RolesLists = new List<RoleList>();
            PopulateAssignedRoles(meet);
            var mtt = db.Announcements.Include(a => a.RolesLists);

            ViewData["Meeting"] = db.Meetings.ToList();
            ViewData["MeetingID"] = id;
            Meeting meeting = db.Meetings.Find(id);

            return View(meeting);
        }

        // GET: Meeting/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meetingOrEvent = db.Meetings.Find(id);
            if (meetingOrEvent == null)
            {
                return HttpNotFound();
            }
            return View(meetingOrEvent);
        }

        // GET: Meeting/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Meeting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateMeeting")]
        public ActionResult Create([Bind(Include = "ID,EventTitle,EventDiscription,EventLocation,EventStart,EventEnd,EventRequirments,EventLinks")] Meeting meeting, string[] selectedRoles)
        {
            try
            {
                if (selectedRoles != null)
                {
                    meeting.RolesLists = new List<RoleList>();
                    foreach (var role in selectedRoles)
                    {
                        var roleToAdd = db.RoleLists.Find(int.Parse(role));
                        meeting.RolesLists.Add(roleToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    db.Meetings.Add(meeting);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateAssignedRoles(meeting);
            return View(meeting);


        }

        // GET: Meeting/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meetingOrEvent = db.Meetings.Find(id);
            if (meetingOrEvent == null)
            {
                return HttpNotFound();
            }
            return View(meetingOrEvent);
        }

        // POST: Meeting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EventTitle,EventDiscription,EventLocation,EventStart,EventEnd,EventRequirments,EventLinks")] Meeting meetingOrEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetingOrEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meetingOrEvent);
        }

        // GET: Meeting/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meetingOrEvent = db.Meetings.Find(id);
            if (meetingOrEvent == null)
            {
                return HttpNotFound();
            }
            return View(meetingOrEvent);
        }

        // POST: Meeting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meetingOrEvent = db.Meetings.Find(id);
            db.Meetings.Remove(meetingOrEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void PopulateAssignedRoles(Meeting meeting)
        {
            var allRole = db.RoleLists;
            var aRoles = new HashSet<int>(meeting.RolesLists.Select(r => r.ID));
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
