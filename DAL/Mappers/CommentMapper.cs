using DAL.Interfacies.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class CommentMapper
    {
        public static DalComment ToDalComment(this Comment ormEntity)
        {
            //if (ormEntity == null)
            //    return null;
            return new DalComment()
            {
                Id = ormEntity.Id,
                Text = ormEntity.Text,
                PublicationDate = ormEntity.PublicationDate,
                UserId = ormEntity.UserId,
                ArticleId = ormEntity.ArticleId
            };
        }

        public static Comment ToOrmComment(this DalComment dalEntity)
        {
            if (dalEntity == null)
                return null;
            return new Comment()
            {
                Id = dalEntity.Id,
                Text = dalEntity.Text,
                PublicationDate = dalEntity.PublicationDate,
                UserId = dalEntity.UserId,
                ArticleId = dalEntity.ArticleId
            };
        }
    }
}
