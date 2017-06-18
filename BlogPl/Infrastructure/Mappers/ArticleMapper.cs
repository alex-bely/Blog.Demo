using BLL.Interfacies.Entities;
using BlogPL.Infrastructure.Mappers;
using BlogPL.Models.TagViewModel;
using BlogPL.Models.ArticleViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPL.Infrastructure.Mappers
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
                Tags = new List<TagEntity>()

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
                Tags = (model.Tags != null) ? String.Join(" ", model.Tags.Select(el =>  el.Name )):null
            };
        }

        
    }
}