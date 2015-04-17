using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Andreas King March 2015

namespace HospiceNiagara.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
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
    }
}