using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class TagMapper
    {
        public static TagEntity ToBllTag(this DalTag dalEntity)
        {
            if (dalEntity == null)
                return null;
            return new TagEntity()
            {
                Id = dalEntity.Id,
                Name = dalEntity.Name,
                
            };
        }

        public static DalTag ToDalTag(this TagEntity bllEntity)
        {
            return new DalTag()
            {
                Id = bllEntity.Id,
                Name = bllEntity.Name,
                Articles = bllEntity.Articles?.Select(el => el.ToDalArticle()).ToList()
            };
        }

        public static DalTag ToDalTag(this string tag)
        {
            return new DalTag { Name = tag, Articles=new List<DalArticle>() };
        }
    }
}
