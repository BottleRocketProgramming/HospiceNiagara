using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospiceNiagara.Models;
using System.IO;
using HospiceNiagara.ViewModels;
using System.Data.Entity.Infrastructure;

//Paul Boyko March 2015

namespace HospiceNiagara.Controllers
{
    public class FileStoragesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
      

        // GET: FileStorages
        [Authorize]
        public ActionResult Index(string searchString, int? id)
        {
            var uRole = db.RoleLists;
            var fileR = new FileStorage();
            fileR.FileStoreUserRoles = new List<RoleList>();
            fileR.FileSubCats = new List<FileSubCat>();
            PopulateAssignedRoles(fileR);
            PopulateCatAndSubCat(fileR);

             var allFiles = db.FileStorages.Include(r => r.FileStoreUserRoles).Include(fsc => fsc.FileSubCats);

            foreach(var u in uRole)
            {
                 if(User.IsInRole(u.RoleName))
                 {
                     allFiles = allFiles.Where(f => f.FileStoreUserRoles.Any(fu => fu.ID == u.ID));
                 }
            }                           
            
            if(id != null && id != 0)
            {
                allFiles =allFiles.Where(f => f.FileSubCats.Any(sc => sc.ID == id));
            }
    
            if(!String.IsNullOrEmpty(searchString))
            {
                allFiles = allFiles.Where(f => f.FileDescription.ToUpper().Contains(searchString.ToUpper()) || f.FileName.ToUpper().Contains(searchString.ToUpper()));
            }

            var viewModel = new List<FileStorageVM>();
            foreach (var f in allFiles)
            {
                viewModel.Add(new FileStorageVM
                            {
                                ID = f.ID,
                                FileName = f.FileName,
                                MimeType = f.MimeType,
                                FileDescription = f.FileDescription,
                                FileUploadDate = f.FileUploadDate
                            });
            }
                      
            return View(viewModel.ToList());
        }

        [HttpPost]
        [OnAction(ButtonName = "UploadFile")]
        [Authorize]
        public ActionResult Index(string fileDescription, string[] selectedRoles, string[] selectedSubCats)
        {
            DateTime uploadDate = DateTime.Now;
            string mimeType = Request.Files[0].ContentType;
            string fileName = Path.GetFileName(Request.Files[0].FileName);
            int fileLenght = Request.Files[0].ContentLength;
            if (!(fileName == "" || fileLenght == 0))
            {
                Stream fileStream = Request.Files[0].InputStream;
                byte[] fileData = new byte[fileLenght];
                fileStream.Read(fileData, 0, fileLenght);

                FileStorage newFile = new FileStorage
                {
                    FileContent = fileData,
                    MimeType = mimeType,
                    FileName = fileName,
                    FileDescription = fileDescription,
                    FileUploadDate = uploadDate
                };
            if(selectedRoles != null)
            {
                newFile.FileStoreUserRoles = new List<RoleList>();
                foreach(var r in selectedRoles)
                {
                    var roleToAdd = db.RoleLists.Find(int.Parse(r));
                    newFile.FileStoreUserRoles.Add(roleToAdd);
                }
            }

            if (selectedSubCats != null)
            {
                newFile.FileSubCats = new List<FileSubCat>();
                foreach(var sc in selectedSubCats)
                {
                    var subCatToAdd = db.FileSubCats.Find(int.Parse(sc));
                    newFile.FileSubCats.Add(subCatToAdd);
                }
            }
                db.FileStorages.Add(newFile);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: FileStorages/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStorage fileStorage = db.FileStorages.Find(id);
            if (fileStorage == null)
            {
                return HttpNotFound();
            }
            return View(fileStorage);
        }

        // GET: FileStorages/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStorage fileStorage = db.FileStorages.Include(r => r.FileStoreUserRoles).Where(i => i.ID == id).Single();
            if (fileStorage == null)
            {
                return HttpNotFound();
            }

            PopulateAssignedRoles(fileStorage);
            return View(fileStorage);
        }

        // POST: FileStorages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditPost(int? id, string[] selectedRoles)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var roleToUpdate = db.FileStorages.Include(a => a.FileStoreUserRoles).Where(i => i.ID == id).Single();

            if (TryUpdateModel(roleToUpdate, "", new string[] { "ID", "MimeType", "FileName","FileDescription" }))
            {
                try
                {
                    UpdateFileStorageRoles(selectedRoles, roleToUpdate);

                    if (ModelState.IsValid)
                    {

                        db.Entry(roleToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save after multiple attempts.  If problem persists, contact systems administrator");
                }
            }
            PopulateAssignedRoles(roleToUpdate);
            return View(roleToUpdate);
        }

        // GET: FileStorages/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStorage fileStorage = db.FileStorages.Find(id);
            if (fileStorage == null)
            {
                return HttpNotFound();
            }
            return View(fileStorage);
        }

        // POST: FileStorages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            FileStorage fileStorage = db.FileStorages.Find(id);
            db.FileStorages.Remove(fileStorage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Download
        public FileContentResult Download(int id)
        {
            
            var downloadFile = db.FileStorages.Where(f => f.ID == id).SingleOrDefault();
            return File(downloadFile.FileContent, downloadFile.MimeType, downloadFile.FileName);
        }

        public void PopulateAssignedRoles(FileStorage fileStore)
        {
            var allRole = db.RoleLists;
            var aRoles = new HashSet<int>(fileStore.FileStoreUserRoles.Select(r => r.ID));
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

        public void PopulateCatAndSubCat(FileStorage fileStore)
        {
            var allSubCats = db.FileSubCats.Include(fc => fc.FlCat);
            var aSubCats = new HashSet<int>(fileStore.FileSubCats.Select(r => r.ID));
            var viewModelSubCat = new List<FileSubCatVM>();
            foreach (var subCat in allSubCats)
            {
                viewModelSubCat.Add(new FileSubCatVM
                {
                    ID = subCat.ID,
                    FileSubCatName = subCat.FileSubCatName,                    
                    FlCat = subCat.FlCat,
                    FileCatFK = subCat.FileCatFK,
                    FileSubCatAssigned = aSubCats.Contains(subCat.ID)

                });
            }

            var allCats = db.FileCats;
            var viewModelCats = new List<FileCatVM>();
         
            foreach (var cat in allCats)
            {
                viewModelCats.Add(new FileCatVM
                {
                    ID = cat.ID,
                    FileCatName = cat.FileCatName,
                    
                });
            }

            ViewBag.FileSubCat = viewModelSubCat;
            ViewBag.Cat = viewModelCats;
        }

        private void UpdateFileStorageRoles(string[] selectedRoles, FileStorage FileToUpdate)
        {
            if (selectedRoles == null)
            {
                FileToUpdate.FileStoreUserRoles = new List<RoleList>();
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var fileRoles = new HashSet<int>
                (FileToUpdate.FileStoreUserRoles.Select(c => c.ID));//IDs of the currently selected roles
            foreach (var rls in db.RoleLists)
            {
                if (selectedRolesHS.Contains(rls.ID.ToString()))
                {
                    if (!fileRoles.Contains(rls.ID))
                    {
                        FileToUpdate.FileStoreUserRoles.Add(rls);
                    }
                }
                else
                {
                    if (fileRoles.Contains(rls.ID))
                    {
                        FileToUpdate.FileStoreUserRoles.Remove(rls);
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
