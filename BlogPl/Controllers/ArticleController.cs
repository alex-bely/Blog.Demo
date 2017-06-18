using BLL.Interfacies.Services;
using BlogPL.Infrastructure.Mappers;
using BlogPL.Models;
using BlogPL.Models.ArticleViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPL.Controllers
{
    /// <summary>
    /// Provides methods for articles handling
    /// </summary>
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IUserService userService;
        private readonly ITagService tagService;

        public ArticleController(IArticleService articleService, IUserService userService,ITagService tagService)
        {
            this.articleService = articleService;
            this.userService = userService;
            this.tagService = tagService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        
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
            model.UserId = Id;
            model.Rating = 0;
            model.PublicationDate = DateTime.Now;
            articleService.Create(model.ToBllArticle(), model.Tags);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DeleteArticle(int ArticleId)
        {
            var article = articleService.GetOneByPredicate(a => a.Id == ArticleId);
            int bloggerId = article.UserId;
            articleService.Delete(article);
            return RedirectToAction("GetBloggerArticles", "Article", new { bloggerid= bloggerId });
        }

        [HttpGet]
        public ActionResult DeleteArticleFromAdmin(int ArticleId)
        {
            var article = articleService.GetOneByPredicate(a => a.Id == ArticleId);
            int bloggerId = article.UserId;
            articleService.Delete(article);
            return RedirectToAction("GetAllArticlesForAdmin", "Article");
        }


        [HttpGet]
        public ActionResult UpdateArticle(int ArticleId)
        {
            var article = articleService.GetOneByPredicate(a => a.Id == ArticleId).
                    ToMvcArticle();
            var articleTags = tagService.GetTagsByArticleId(ArticleId).Select(el => el.ToMvcTag());
            article.Tags = String.Empty;
            foreach (var tag in articleTags)
                article.Tags += tag.Name + " ";
            article.Tags = article.Tags.Trim();
            return View(article);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateArticle(ArticleViewModel model)
        {
            model.PublicationDate = DateTime.Now;
            articleService.Update(model.ToBllArticle(), model.Tags);
            return RedirectToAction("ViewArticle", "Article", new {ArticleId=model.Id });
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
                models.Add(article);
            }

            int pageSize = 4;
            int pageNumber = page;
            return PartialView("~/Views/Article/_ShowArticles.cshtml", models.ToPagedList(pageNumber, pageSize));
        }

        

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ShowArticle(int ArticleId)
        {
            var article = articleService.GetOneByPredicate(u => u.Id == ArticleId).ToMvcArticle();
            article.User = userService.GetUserById(article.UserId).ToMvcUser();
            return PartialView("~/Views/Article/_ShowArticle.cshtml",article);
        }
        

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewArticle(int ArticleId)
        {
            ViewBag.BackPath = HttpContext.Request.UrlReferrer?.PathAndQuery;
            var bllArticle = articleService.GetOneByPredicate(u => u.Id == ArticleId);
            bllArticle.Tags = tagService.GetTagsByArticleId(ArticleId).ToList();
            var article= bllArticle.ToMvcArticle();
            article.Rating += 1;
            articleService.Update(article.ToBllArticle(),article.Tags);
            article.User = userService.GetUserById(article.UserId).ToMvcUser();
            return View(article);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetBloggerArticles(int bloggerid)
        {
            return View(bloggerid);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewBloggerArticles(int bloggerid, int page=1)
        {
            var user = userService.GetUserById(bloggerid);
            var articles = articleService.GetAllByPredicate(u => u.UserId == bloggerid).
                    OrderByDescending(a => a.PublicationDate).Select(a => a.ToMvcArticle());
            List<ArticleViewModel> models = new List<ArticleViewModel>();
            foreach (var article in articles)
            {
                models.Add(article);
            }
            @ViewBag.BloggerLogin = user.Login;
            int pageSize = 4;
            int pageNumber = page;
            return PartialView("_ShowBloggerArticles",models.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        [Authorize(Roles ="admin")]
        public ActionResult GetAllArticles()
        {
            var articles = articleService.GetAllArticles().
                    OrderByDescending(a => a.PublicationDate).
                        Select(a => a.ToMvcArticle());

            List<ArticleViewModel> models = new List<ArticleViewModel>();
            foreach (var article in articles)
            {
                article.Tags = userService.GetUserById(article.UserId).Login;
                models.Add(article);
            }
            return View(models);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult GetAllArticlesForAdmin()
        {
            var articles = articleService.GetAllArticles().
                    OrderByDescending(a => a.PublicationDate).
                        Select(a => a.ToMvcArticle());

            List<ArticleViewModel> models = new List<ArticleViewModel>();
            foreach (var article in articles)
            {
                article.Tags = userService.GetUserById(article.UserId).Login;
                models.Add(article);
            }
            return PartialView("_GetArticlesForAdmin", models);
        }


    }
}