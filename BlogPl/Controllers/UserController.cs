using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BlogPL.Infrastructure;
using BlogPL.Infrastructure.Mappers;
using BlogPL.Models.AccountViewModel;
using BlogPL.Models.UserViewModel;
using BlogPL.Providers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogPL.Controllers
{
    /// <summary>
    /// Provides methods for users handling
    /// </summary>
    public class UserController : Controller
    {

        private readonly IArticleService articleService;
        private readonly IUserService userService;
        

        public UserController(IArticleService articleService, IUserService userService)
        {
            this.articleService = articleService;
            this.userService = userService;
        }



        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel model)
        {
            if (model.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Incorrect input.");
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
                    Roles = new List<RoleEntity>() { new RoleEntity { Name = model.Roles } },
                    Avatar = imgArray
                };
                MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(blluser);
                if (membershipUser != null)
                {
                    return RedirectToAction("GetAllUsers", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration");
                }
            }
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int userId)
        {
            UserEntity user = userService.GetUserById(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            var mvcUser = user.ToMvcInfoUser();
            return View(mvcUser);
            //return View(user.ToMvcInfoUser());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int UserId)
        {
            UserEntity user = userService.GetUserById(UserId);
            List<ArticleEntity> articles = articleService.GetAllByPredicate(el => el.UserId == UserId).ToList();
            if(articles.Count!=0)
            {
                foreach (var item in articles)
                    articleService.Delete(item);
            }
            userService.DeleteUser(user);
            return RedirectToAction("GetAllUsers");
        }


        [HttpGet]
        [Authorize(Roles ="admin")]
        public ActionResult GetAllUsers(int page = 1)///page????Look ArticleController
        {
            var users = userService.GetAllUsers().Select(el=>el.ToMvcInfoUser());
            
            List<UserViewModel> models = new List<UserViewModel>();
            foreach (var user in users)
            {
                var count = articleService.GetAllByPredicate(el => el.UserId == user.UserId).Count();
                user.ArticlesCount = count;
                models.Add(user);
            }
            return View(models);
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public ActionResult UpdateRole(int userId, string role)
        {
            var t = userService.GetUserById(userId);
            t.Roles.Add(new RoleEntity { Name = role });
            userService.UpdateUser(t);
            t= userService.GetUserById(userId);
            var tt = t.ToMvcInfoUser();
            return PartialView("_UpdateRole",tt);
        }


        [HttpGet]
        [Authorize]
        public ActionResult GetUserProfile(string userLogin)
        {
            if (this.Profile.UserName != userLogin && !this.User.IsInRole("admin"))
                 return RedirectToAction("Forbidden", "Error"); 
            if(TempData["shortMessage"]!=null)
            ViewBag.Message = TempData["shortMessage"].ToString();
            var t = userService.GetOneByPredicate(el=>el.Login==userLogin);
            if(t==null)
                return RedirectToAction("NotFound", "Error");

            string imageDataURL = userService.GetAvatarAsString(userLogin);
            ViewBag.Avatar = imageDataURL;
            var tt = t.ToMvcInfoUser();
            return View(tt);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Update(UserViewModel user)
        {
            if(ModelState.IsValid)
            {
                var t = userService.GetUserById(user.UserId);
                t.Email = user.UserEmail;
                userService.UpdateUser(t);
                TempData["shortMessage"] = "Upload successfully";
            }
            else
            {
                ModelState.AddModelError("", "Wrong e-mail");
            }
            return RedirectToAction("GetUserProfile","User", new { userLogin = user.Login });
        }

        public ActionResult GetAvatar(string Login, int id=0)
        {
            if (id != 0)
                Login = userService.GetUserById(id).Login;
            string avatar = userService.GetAvatarAsString(Login);
            return Content(avatar);
        }

        

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, string userLogin)
        {
            if (file.ContentType.ToLower() != "image/jpg" &&
                    file.ContentType.ToLower() != "image/jpeg" &&
                    file.ContentType.ToLower() != "image/pjpeg" &&
                    file.ContentType.ToLower() != "image/gif" &&
                    file.ContentType.ToLower() != "image/x-png" &&
                    file.ContentType.ToLower() != "image/png")
            {
                return RedirectToAction("GetUserProfile", "User", new { userLogin = userLogin });
            }

           
            var user = userService.GetOneByPredicate(el => el.Login == userLogin);

            if (!ReferenceEquals(file, null))
            {
                using (var binaryreader = new BinaryReader(file.InputStream))
                {
                    user.Avatar= binaryreader.ReadBytes(file.ContentLength);
                    userService.UpdateUser(user);
                }
            }
            TempData["shortMessage"] = "Upload successfully";
            return RedirectToAction("GetUserProfile", "User", new { userLogin = user.Login});
        }

    }
}