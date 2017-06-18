using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class CommentMapper
    {
        public static CommentEntity ToBllComment(this DalComment dalEntity)
        {
            if (dalEntity == null)
                return null;
            return new CommentEntity()
            {
                Id = dalEntity.Id,
                Text = dalEntity.Text,
                PublicationDate = dalEntity.PublicationDate,
                UserId = dalEntity.UserId,
                ArticleId = dalEntity.ArticleId
            };
        }

        public static DalComment ToDalComment(this CommentEntity bllEntity)
        {
            return new DalComment()
            {
                Id = bllEntity.Id,
                Text = bllEntity.Text,
                PublicationDate = bllEntity.PublicationDate,
                UserId = bllEntity.UserId,
                ArticleId = bllEntity.ArticleId
            };
        }
    }
}
