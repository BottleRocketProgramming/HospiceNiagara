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
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace HospiceNiagara.Controllers
{
    public class ContactsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contacts
        [Authorize]
        public ActionResult Index(string sortOrder)
        {
            var listOfContacts = db.Contacts.ToList();
            var listOfContactTypes = db.ContactTypes;

            var contact = new Contact();
            contact.ContactType = new ContactType();
            PopulateContactTypes(contact);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PosSortParm = sortOrder == "Position" ? "pos_desc" : "Position";

            switch (sortOrder)
            {
                case "name_desc":
                    listOfContacts = listOfContacts.OrderByDescending(s => s.Name).ToList();
                    break;
                case "Position":
                    listOfContacts = listOfContacts.OrderBy(s => s.Position).ToList();
                    break;
                case "pos_desc":
                    listOfContacts = listOfContacts.OrderByDescending(s => s.Position).ToList();
                    break;
                default:
                    listOfContacts = listOfContacts.OrderBy(s => s.Name).ToList();
                    break;
            }

            ViewData["Contacts"] = listOfContacts.Distinct();
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
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminCreate()
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

        //Admin List
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminList()
        {
            return View(db.Contacts.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Create/Modify Contacts")]
        public ActionResult AdminCreate([Bind(Include = "ID,Name,Position,Extention,StartDate,CellNumber")] Contact contact, int selectedContactType)
        {
            try
            {

                var typeToAdd = db.ContactTypes.Find(selectedContactType);
                contact.ContactType = typeToAdd;

                if (ModelState.IsValid)
                {
                    db.Contacts.Add(contact);
                    db.SaveChanges();
                    return RedirectToAction("AdminList");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(contact);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        [OnAction(ButtonName = "CreateContact")]
        [Authorize(Roles = "Administrator, Create/Modify Contacts")]
        public ActionResult Create([Bind(Include = "ID,Name,Position,Extention,StartDate,CellNumber")] Contact contact, int selectedContactType)
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

            if (TryUpdateModel(contactToUpdate, "", new string[] { "ID", "Name", "Position", "Extention", "CellNumber", "StartDate" }))
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

        public ActionResult ExportContacts()
        {
            var ContactsForExcel = ContactsforExport();
            var filename = "HospiceNiagara_ContactList.xls";

            GridView gv = new GridView();
            gv.DataSource = ContactsForExcel;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename));
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return Content(sw.ToString(), "application/vnd.ms-excel");
        }

        public List<ContactListForExport> ContactsforExport()
        {
            List<Contact> contacts = db.Contacts.OrderBy(c => c.ContactType.ContactTypeName).ToList();
            var ContactListForExport = new List<ContactListForExport>();
            foreach (var contact in contacts)
            {
                ContactListForExport.Add(new ContactListForExport
                {
                    Name = contact.Name,
                    Position = contact.Position,
                    Extention = contact.Extention,
                    CellNumber = contact.CellNumber,
                    ContactType = contact.ContactType.ContactTypeName
                });
            }

            return ContactListForExport;
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
