using DAL.Interfacies.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class ArticleMapper
    {
        public static DalArticle ToDalArticle(this Article ormEntity)
        {
            //if (ormEntity == null)
            //    return null;
            return new DalArticle()
            {
                Id = ormEntity.Id,
                Title = ormEntity.Title,
                Content = ormEntity.Content,
                PublicationDate = ormEntity.PublicationDate,
                UserId = ormEntity.UserId,
                Rating = ormEntity.Rating,
                Comments = ormEntity.Comments.Select(r => r.ToDalComment()).ToList(),
                Tags = ormEntity.Tags.Select(r => new DalTag { Id = r.Id, Name = r.Name }).ToList()
            };
        }

        public static Article ToOrmArticle(this DalArticle dalEntity)
        {
            //if (dalEntity == null)
            //    return null;
            return new Article()
            {
                Id = dalEntity.Id,
                Title = dalEntity.Title,
                Content = dalEntity.Content,
                PublicationDate = dalEntity.PublicationDate,
                UserId = dalEntity.UserId,
                Rating = dalEntity.Rating,
                Comments = dalEntity.Comments != null
                        ? dalEntity.Comments.Select(r => r.ToOrmComment()).ToList()
                        : null,
                Tags = dalEntity.Tags != null
                        ? dalEntity.Tags.Select(r => r.ToOrmTag()).ToList()
                        : null

            };
        }

        
    }
}
