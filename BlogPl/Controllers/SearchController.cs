using BLL.Interfacies.Services;
using BlogPL.Infrastructure.Mappers;
using BlogPL.Models.ArticleViewModel;
using BlogPL.Models.SearchViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPL.Controllers
{
    /// <summary>
    /// Provides methods for search handling
    /// </summary>
    public class SearchController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IUserService userService;

        public SearchController(IArticleService articleService, IUserService userService)
        {
            this.articleService = articleService;
            this.userService = userService;
        }

        [ChildActionOnly]
        public ActionResult Index()
        {
            return PartialView("~/Views/Shared/_Search.cshtml");
        }
        
        public ActionResult SearchByTitle(SearchViewModel model)
        {
            string title = (model.SearchString==" ")?null: model.SearchString;
            var articles = articleService.GetAllByPredicate(a => a.Title.Contains(title))
                .Take(5).Select(a => new { label=a.Title, value = a.Id, ArticleId=a.Id });

            if(articles.ToArray().Length==0)
            {
                return new JsonResult()
                {
                    Data = new { message = "No results found" }
                };
            }
            return Json(articles, JsonRequestBehavior.AllowGet);
    }

        public ActionResult SearchByTag(string tagName)
        {
            var lid = this.Request.UrlReferrer.Query.Skip(11).ToList();
            int id=Int32.Parse(String.Join(String.Empty, lid));
            
            var t = articleService.GetArticlesByTagName(tagName).Where(el=>el.Id!=id).Select(el=>el.ToMvcArticle());

            return PartialView("~/Views/Search/_SearchByTag.cshtml", t);
        }

        
        [HttpPost]
        public ActionResult FullSearch(string title)
        {
            if (title.Trim() == String.Empty)
            {
                var temp = new List<ArticleViewModel>();
                return PartialView("~/Views/Search/_FullSearch.cshtml", temp);
            }
            var result = articleService.GetAllByPredicate(u => u.Title.Contains(title)).
                    Select(r => r.ToMvcArticle()).ToList();
            result.ForEach(el => el.User = userService.GetUserById(el.UserId).ToMvcUser());
            ViewBag.QuerySearch = title;
            return PartialView ("~/Views/Search/_FullSearch.cshtml", result);
        }
        

    }
}