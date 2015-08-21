using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using HospiceNiagara.Models;
using System.Collections.Generic;
using HospiceNiagara.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;
using System.Data.Entity;

//Paul Boyko April 2015

namespace HospiceNiagara.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        ApplicationDbContext context = new ApplicationDbContext();
        
        
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var appUserEmailConf = db.Users.Where(a => a.UserName == model.Email).Single();
                if (appUserEmailConf.EmailConfirmed)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, change to shouldLockout: true
                    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
                    switch (result)
                    {
                        case SignInStatus.Success:
                            if(appUserEmailConf.PasswordChanged)
                            {
                                appUserEmailConf.LastLogin = DateTime.Now;
                                db.Entry(appUserEmailConf).State = EntityState.Modified;
                                db.SaveChanges();
                                return RedirectToLocal(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Manage/ChangePassword");
                            }
                            
                        case SignInStatus.LockedOut:
                            return View("Lockout");
                        case SignInStatus.RequiresVerification:
                            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("", "Invalid login attempt.");
                            return View(model);
                    }
                }
            }
            catch(Exception ex)
            {
                if (ex.InnerException==null)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                else
                {
                    ModelState.AddModelError("", ex.InnerException);
                }
                return View(model);
            }
            ModelState.AddModelError("", "Please confirm email.");
            return View(model);
        }

        //// GET: /Account/VerifyCode
        //[AllowAnonymous]
        //public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        //{
        //    // Require that the user has already logged in via username/password or external login
        //    if (!await SignInManager.HasBeenVerifiedAsync())
        //    {
        //        return View("Error");
        //    }
        //    var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
        //    if (user != null)
        //    {
        //        var code = await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
        //    }
        //    return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        
        //// POST: /Account/VerifyCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // The following code protects for brute force attacks against the two factor codes. 
        //    // If a user enters incorrect codes for a specified amount of time then the user account 
        //    // will be locked out for a specified amount of time. 
        //    // You can configure the account lockout settings in IdentityConfig
        //    var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(model.ReturnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid code.");
        //            return View(model);
        //    }
        //}

        
        // GET: /Account/Register
        [Authorize(Roles = "Administrator, Manage Users")]
        public ActionResult Register()
        {
            ApplicationUser user = new ApplicationUser();
            PopulateAssignedRoles(user);
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "Administrator, Manage Users")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string[] selectedRoles) //, ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                //PopulateAssignedRoles(user);
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));                
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, UserFName = model.UserFName, UserMName = model.UserMName, UserLName = model.UserLName, UserDOB = model.UserDOB, UserAddress = model.UserAddress, PhoneNumber = model.PhoneNumber, StartDate = model.StartDate  };
                user.EmailConfirmed = false;
                user.PasswordChanged = false;
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    MailMessage m = new MailMessage();
                    m.From = new MailAddress("noreply@hospiceniagaraportal.ca");
                    m.To.Add(new MailAddress(user.Email));
                    m.Subject = "Hospice Niagara Registration Confirmation";
                    m.Body = String.Format("Dear " + user.UserFullName + ",<br /> <br /> You have been registered as a user in the Hospice Niagara Portal.<br /><br /> Please click on the following link to confirm your email and log in with <b>" + user.Email + "</b> as your email and <b>" + model.Password + "</b> as the password. <br /><br /> <a href=\"{1}\" title= \"User Email Confirmation\">Click this link to confirm your e-mail</a>", user.UserName, Url.Action("ConfirmEmail", "Account", new { Token = user.Id, Email = user.Email }, Request.Url.Scheme));
                    m.IsBodyHtml = true;
                    SmtpClient smtp = new System.Net.Mail.SmtpClient("smtpout.secureserver.net");
                    smtp.Port = 25;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("noreply@hospiceniagaraportal.ca", "HNPortalAdmin1");
                    smtp.EnableSsl = false;
                    smtp.Send(m);
             
                    if(selectedRoles != null)
                    {
                        //user.RoleLists = new List<RoleList>();
                        foreach (var r in selectedRoles)
                        {
                            var roleToAdd = db.RoleLists.Find(int.Parse(r));
                            //user.RoleLists.Add(roleToAdd);
                            //PopulateAssignedRoles(user);
                            manager.AddToRole(user.Id, roleToAdd.RoleName);                         
                        }
                    }

                    return RedirectToAction("Confirm", "Account", new { Email = user.Email });       
                   
                }
                   
                else
                {
                    AddErrors(result);
                }
                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string Token, string Email)
        {
            ApplicationUser user = this.UserManager.FindById(Token);
            if(user != null)
            {
                if(user.Email == Email)
                {
                    user.EmailConfirmed = true;
                    await UserManager.UpdateAsync(user);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home"); //, new { ConfirmEmail = user.Email } );
                }
                else
                {
                    return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                }
            }
            else
            {
                return RedirectToAction("Confirm", "Account", new { Email = user.Email });
            }
           
        }

        [AllowAnonymous]
        public ActionResult Confirm(string Email)
        {
            ViewBag.Email = Email;
            return View();
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                MailMessage m = new MailMessage();
                m.From = new MailAddress("noreply@hospiceniagaraportal.ca");
                m.To.Add(new MailAddress(user.Email));
                m.Subject = "Hospice Niagara Password Reset";
                m.Body = String.Format("Dear: " + user.UserFullName + ", <br/> You have requested a password reset for the Hospice Niagara's Employee and Volunteer Portal.  Please click on the link to reset your password. <br/> <a href=\"{1}\" title= \"User Email Confirmation\">Click this link so you can reset your Password</a>", user.UserName, Url.Action("ResetPassword", "Account", new { UserId = user.Id, code = code }, protocol: Request.Url.Scheme));
                m.IsBodyHtml = true;
                SmtpClient smtp = new System.Net.Mail.SmtpClient("smtpout.secureserver.net");
                smtp.Port = 25;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("noreply@hospiceniagaraportal.ca", "HNPortalAdmin1");
                smtp.EnableSsl = false;
                smtp.Send(m);

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }


        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        
        //// POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}


        //// GET: /Account/SendCode
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        //{
        //    var userId = await SignInManager.GetVerifiedUserIdAsync();
        //    if (userId == null)
        //    {
        //        return View("Error");
        //    }
        //    var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
        //    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //// POST: /Account/SendCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SendCode(SendCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    // Generate the token and send it
        //    if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
        //    {
        //        return View("Error");
        //    }
        //    return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        //}

        
        //// GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //        case SignInStatus.Failure:
        //        default:
        //            // If the user does not have an account, then prompt the user to create an account
        //            ViewBag.ReturnUrl = returnUrl;
        //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //    }
        //}

        //
        //// POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        //empty Roles for create
        public void PopulateAssignedRoles(ApplicationUser appUser)
        {
            var allRole = db.RoleLists;            
            var viewModel = new List<RoleVM>();
            foreach (var roll in allRole)
            {
                viewModel.Add(new RoleVM
                {
                    RoleID = roll.ID,
                    RoleName = roll.RoleName,
                    IsPerm = roll.IsPerm
                });
            }

            ViewBag.RolesLists = viewModel;
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

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}