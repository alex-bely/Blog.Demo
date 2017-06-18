using BLL.Interfacies.Services;
using BlogPL.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPL.Controllers
{
    /// <summary>
    /// Provides methods for tag handling
    /// </summary>
    public class TagController : Controller
    {

        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }
        // GET: Tag
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetTags(int articleId)
        {
            var tags = tagService.GetTagsByArticleId(articleId);
            return PartialView("_TagsOfArticle",tags.Select(el=>el.ToMvcTag()));
        }


    }
}