using BLL.Interfacies.Entities;
using BlogPL.Infrastructure;
using BlogPL.Models.AccountViewModel;
using BlogPL.Providers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogPL.Controllers
{
    /// <summary>
    /// Provides methods for account handling
    /// </summary>
    public class AccountController : Controller
    {
        
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }


        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong password or login");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            model.Roles = "user";
            if (model.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Incorrect captcha input.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                
                string filePath = Server.MapPath(Url.Content("~/Content/Images/icon_user_gray.png"));
                Image im = Image.FromFile(filePath);
                ImageConverter converter = new ImageConverter();
                byte[] imgArray = (byte[])converter.ConvertTo(im, typeof(byte[]));
                

                var blluser = new UserEntity()
                {
                    Login = model.UserName,
                    Email = model.UserEmail,
                    Password = model.UserPassword,
                    Avatar=imgArray

                };
                MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(blluser);
                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User with this login already exists");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] =
                new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return null;
        }
    }
}