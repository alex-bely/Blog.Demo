using BLL.Interfacies.Entities;
using BlogPL.Models.CommentViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPL.Infrastructure.Mappers
{
    public static class CommentMapper
    {
        public static CommentEntity ToBllComment(this CommentViewModel model)
        {
            return new CommentEntity()
            {
                Id = model.Id,
                Text = model.Text,
                PublicationDate = model.PublicationDate,
                UserId = model.UserId,
                ArticleId = model.ArticleId
            };
        }

        public static CommentViewModel ToMvcComment(this CommentEntity model)
        {
            return new CommentViewModel()
            {
                Id = model.Id,
                Text = model.Text,
                PublicationDate = model.PublicationDate,
                UserId = model.UserId,
                ArticleId = model.ArticleId
            };
        }
    }
}