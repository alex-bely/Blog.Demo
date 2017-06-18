using BLL.Interfacies.Services;
using BlogPL.Infrastructure.Mappers;
using BlogPL.Models.CommentViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPL.Controllers
{
    /// <summary>
    /// Provides methods for comments handling
    /// </summary>
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
            model.PublicationDate = DateTime.Now;
            model.UserId = Convert.ToInt32(HttpContext.Profile.GetPropertyValue("Id"));
            model.ArticleId = ArticleId;
            commentService.Create(model.ToBllComment());
            return GetComments(model.ArticleId);
        }

        [HttpGet]
        public ActionResult GetComments(int articleId)
        {
            var comments = commentService.GetAllByPredicate(u => u.ArticleId == articleId).Select(c => c.ToMvcComment());
            return PartialView("_CommentsOfArticle", GetCommentModel(comments));
        }

        [HttpPost]
        public ActionResult DeleteComment(int articleId, int commentid)
        {
            commentService.Delete(commentService.GetAllByPredicate(u => u.Id == commentid).FirstOrDefault());
            var comments = commentService.GetAllByPredicate(u => u.ArticleId == articleId).Select(c => c.ToMvcComment());
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