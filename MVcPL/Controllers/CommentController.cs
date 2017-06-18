using BLL.Interfacies.Services;
using MVcPL.Infrastructure.Mappers;
using MVcPL.Models.CommentViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVcPL.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IUserService userService;

        public CommentController(ICommentService commentService, IUserService userService)
        {
            this.commentService = commentService;
            this.userService = userService;
        }
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult AddComment()
        {
            return PartialView("_CreateComment");
        }

        [HttpPost]
        public ActionResult AddComment(CommentViewModel model, int ArticleId)
        {
            //var name = HttpContext.User.Identity.Name;
            //var Id = userService.GetOneByPredicate(n => n.Login == name).Id;
            //model.UserId = Convert.ToInt32(Id);//?Convert?

            model.PublicationDate = DateTime.Now;
            model.UserId = Convert.ToInt32(HttpContext.Profile.GetPropertyValue("Id"));
            model.ArticleId = ArticleId;
            commentService.Create(model.ToBllComment());
            return GetComments(model.ArticleId);
        }

        [HttpGet]
        public ActionResult GetComments(int ArticleId)
        {
            var comments = commentService.GetAllByPredicate(u => u.ArticleId == ArticleId).Select(c => c.ToMvcComment());
            return PartialView("_CommentsOfArticle", GetCommentModel(comments));
        }

        [HttpPost]
        public ActionResult DeleteComment(int ArticleId, int commentid)
        {
            commentService.Delete(commentService.GetAllByPredicate(u => u.Id == commentid).FirstOrDefault());
            var comments = commentService.GetAllByPredicate(u => u.ArticleId == ArticleId).Select(c => c.ToMvcComment());
            return PartialView("_CommentsOfArticle", GetCommentModel(comments));
        }

        private IEnumerable<CommentViewModel> GetCommentModel(IEnumerable<CommentViewModel> comments)
        {
            if (comments == null) return null;
            List<CommentViewModel> models = new List<CommentViewModel>();
            foreach (var comment in comments)
            {
                comment.User = userService.GetOneByPredicate(u => u.Id == comment.UserId).ToMvcUser();
                models.Add(comment);
            }
            return models;
        }
    }
}