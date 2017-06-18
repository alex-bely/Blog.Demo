using BLL.Interfacies.Services;
using MVcPL.Infrastructure.Mappers;
using MVcPL.Models;
using MVcPL.Models.ArticleViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVcPL.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IUserService userService;

        public ArticleController(IArticleService articleService, IUserService userService)
        {
            this.articleService = articleService;
            this.userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Authorize]
        public ActionResult CreateArticle()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateArticle(ArticleViewModel model)
        {
            var name = HttpContext.User.Identity.Name;
            var Id = userService.GetOneByPredicate(n => n.Login == name).Id;
            model.UserId = Convert.ToInt32(Id);//?Convert?
            model.Rating = 0;
            model.PublicationDate = DateTime.Now;
            articleService.Create(model.ToBllArticle(),"dfdf");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DeleteArticle(int ArticleId)
        {
            articleService.Delete(articleService.GetOneByPredicate(a => a.Id == ArticleId));
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult UpdateArticle(int ArticleId)
        {
            var article = articleService.GetOneByPredicate(a => a.Id == ArticleId).
                    ToMvcArticle();
            return View(article);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateArticle(ArticleViewModel model)
        {
            model.PublicationDate = DateTime.Now;
            articleService.Update(model.ToBllArticle()," sdas");
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult ShowArticles(int page = 1)
        {
            var articles = articleService.GetAllArticles().
                    OrderByDescending(a => a.PublicationDate).
                        Select(a => a.ToMvcArticle());

            List<ArticleViewModel> models = new List<ArticleViewModel>();
            foreach (var article in articles)
            {
                //article.User = userService.GetById(article.User.UserId).GetMvcEntity();
                models.Add(article);
            }

            int pageSize = 3;
            IEnumerable<ArticleViewModel> articleModels = models.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = models.Count };
            @ViewBag.PageInfo = pageInfo;
            return PartialView(articleModels);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ShowArticle(int ArticleId)
        {
            var article = articleService.GetOneByPredicate(u => u.Id == ArticleId).ToMvcArticle();
            article.User = userService.GetUserById(article.UserId).ToMvcUser();
            return PartialView(article);
        }

        public ActionResult ShowPages(IEnumerable<ArticleViewModel> models, int page = 1)
        {
            int pageSize = 3;
            IEnumerable<ArticleViewModel> articleModels = models.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = models.Count() };
            @ViewBag.PageInfo = pageInfo;
            return PartialView("ShowArticles", articleModels);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewArticle(int ArticleId)
        {
            var article = articleService.GetOneByPredicate(u => u.Id == ArticleId).ToMvcArticle();
            article.Rating += 1;
            articleService.Update(article.ToBllArticle(),"sda");
            article.User = userService.GetUserById(article.UserId).ToMvcUser();
            return View(article);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewBloggerArticles(int bloggerid)
        {
            var user = userService.GetUserById(bloggerid);
            var articles = articleService.GetAllByPredicate(u => u.UserId == bloggerid).
                    OrderByDescending(a => a.PublicationDate).Select(a => a.ToMvcArticle());
            List<ArticleViewModel> models = new List<ArticleViewModel>();
            foreach (var article in articles)
            {
                //article.User = userService.GetById(article.User.UserId).GetMvcEntity();
                models.Add(article);
            }
            @ViewBag.BloggerLogin = user.Login;
            return View("BloggerArticles", models);
        }
    }
}