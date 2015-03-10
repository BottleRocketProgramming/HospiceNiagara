using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospiceNiagara.Models;

//Andreas King March 2015

namespace HospiceNiagara.Controllers
{
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contact
        public ActionResult Index(int? id)
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
        public ActionResult Create([Bind(Include = "ID,BoardContPosition,BoardContHomeAddy,BoardContWorkAddy,BoardContHomePhone,BoardContWorkPhone,BoardContCellPhone,BoardContFaxNum,BoardContPartnerName")] BoardContact boardContact)
        {
            if (ModelState.IsValid)
            {
                db.BoardContacts.Add(boardContact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boardContact);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BoardContPosition,BoardContHomeAddy,BoardContWorkAddy,BoardContHomePhone,BoardContWorkPhone,BoardContCellPhone,BoardContFaxNum,BoardContPartnerName")] BoardContact boardContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boardContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boardContact);
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
