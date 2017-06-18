using BLL.Interfacies.Entities;
using MVcPL.Models.ArticleViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVcPL.Infrastructure.Mappers
{
    public static class ArticleMapper
    {
        public static ArticleEntity ToBllArticle(this ArticleViewModel model)
        {
            return new ArticleEntity()
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                PublicationDate = model.PublicationDate,
                UserId = model.UserId,
                Rating = model.Rating,

            };
        }

        public static ArticleViewModel ToMvcArticle(this ArticleEntity model)
        {
            return new ArticleViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                PublicationDate = model.PublicationDate,
                UserId = model.UserId,
                Rating = model.Rating,
            };
        }
    }
}