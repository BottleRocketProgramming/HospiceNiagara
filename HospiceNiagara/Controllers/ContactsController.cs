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
    public class ContactsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contacts
        [Authorize]
        public ActionResult Index()
        {
            var listOfContacts = db.Contacts;
            var listOfContactTypes = db.ContactTypes;

            var contact = new Contact();
            contact.ContactType = new ContactType();
            PopulateContactTypes(contact);
            ViewData["Contacts"] = listOfContacts.ToList().Distinct();
            ViewData["ContactTypes"] = listOfContactTypes.ToList().Distinct();

            return View();
        }

        // GET: Contacts/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateContact")]
        [Authorize(Roles = "Administrator, Create/Modify Contacts")]
        public ActionResult Create([Bind(Include = "ID,Name,Position,Extention,CellNumber")] Contact contact, int selectedContactType)
        {
            try
            {

                var typeToAdd = db.ContactTypes.Find(selectedContactType);
                contact.ContactType = typeToAdd;

                if (ModelState.IsValid)
                {
                    db.Contacts.Add(contact);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        [Authorize(Roles = "Administrator, Create/Modify Contacts")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            PopulateContactTypes(contact);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Create/Modify Contacts")]
        public ActionResult Edit(int? id, int selectedContactType)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contactToUpdate = db.Contacts.Find(id);

            if (TryUpdateModel(contactToUpdate, "", new string[] { "ID","Name","Position","Extention","CellNumber" }))
            {
                try
                {
                    contactToUpdate.ContactType = db.ContactTypes.Find(selectedContactType);
                    if (ModelState.IsValid)
                    {
                        db.Entry(contactToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save after multiple attempts.  If problem persists, contact systems administrator");
                }
            }
            PopulateContactTypes(contactToUpdate);
            return View(contactToUpdate);
        }

        // GET: Contacts/Delete/5
        [Authorize(Roles = "Administrator, Remove Records")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Remove Records")]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void PopulateContactTypes(Contact contact)
        {
            var contactTypes = db.ContactTypes;
            var selected = contact.ContactType;
            var viewModel = new List<ContactTypeVM>();
            foreach (var type in contactTypes)
            {
                viewModel.Add(new ContactTypeVM
                {
                    ID = type.ID,
                    ContactTypeName = type.ContactTypeName,
                    ContactTypeSelected = selected.Equals(type)
                });
            }
            ViewBag.ContTypes = viewModel;
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
