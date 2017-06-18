using DAL.Interfacies.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class TagMapper
    {
        public static DalTag ToDalTag(this Tag ormEntity)
        {
            if (ormEntity == null)
                return null;
            return new DalTag()
            {
                Id = ormEntity.Id,
                Name = ormEntity.Name,
                Articles=ormEntity.Articles?.Select(el=>el.ToDalArticle()).ToList()
            };
        }

        public static Tag ToOrmTag(this DalTag dalEntity)
        {
            if (dalEntity == null)
                return null;
            return new Tag()
            {
                Id = dalEntity.Id,
                Name = dalEntity.Name,
                Articles=dalEntity.Articles?.Select(el=>el.ToOrmArticle()).ToList()
            };
        }
    }
}
