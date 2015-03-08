using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

//Paul Boyko Feb 2015

namespace HospiceNiagara.Controllers
{
    class OnActionAttribute : ActionMethodSelectorAttribute
    {
        public String ButtonName { get; set; }

        public override bool IsValidForRequest(ControllerContext controllerContext,MethodInfo methodInfo)
        {
            var req = controllerContext.RequestContext.HttpContext.Request;
            return !string.IsNullOrEmpty(req.Form[this.ButtonName]);
        }
    }

   
}
