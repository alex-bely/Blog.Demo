using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class ArticleMapper
    {
        public static ArticleEntity ToBllArticle(this DalArticle dalEntity)
        {
            //if (ormEntity == null)
            //    return null;
            return new ArticleEntity()
            {
                Id = dalEntity.Id,
                Title = dalEntity.Title,
                Content = dalEntity.Content,
                PublicationDate = dalEntity.PublicationDate,
                UserId = dalEntity.UserId,
                Rating = dalEntity.Rating,
                Comments =
                    dalEntity.Comments != null
                        ? dalEntity.Comments.Select(r => r.ToBllComment()).ToList()
                        : null,
                Tags =
                    dalEntity.Tags != null
                        ? dalEntity.Tags.Select(r => r.ToBllTag()).ToList()
                        : null
            };
        }

        public static DalArticle ToDalArticle(this ArticleEntity bllEntity)
        {
            //if (dalEntity == null)
            //    return null;
            return new DalArticle()
            {
                Id = bllEntity.Id,
                Title = bllEntity.Title,
                Content = bllEntity.Content,
                PublicationDate = bllEntity.PublicationDate,
                UserId = bllEntity.UserId,
                Rating = bllEntity.Rating,
                //Comments = bllEntity.Comments.Select(r => r.ToDalComment()).ToList(),
                Tags = bllEntity.Tags != null 
                       ? bllEntity.Tags.Select(r => r.ToDalTag()).ToList()
                       : new List<DalTag>()

            };
        }
    }
}
