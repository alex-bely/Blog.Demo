using DAL.Interfacies.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class RoleMapper
    {
        public static DalRole ToDalRole(this Role ormEntity)
        {
            //if (ormEntity == null)
            //    return null;
            return new DalRole()
            {
                Id = ormEntity.Id,
                Name = ormEntity.Name,                
            };
        }

        public static Role ToOrmRole(this DalRole dalEntity)
        {
            //if (dalEntity == null)
            //    return null;
            return new Role()
            {
                Id = dalEntity.Id,
                Name = dalEntity.Name
            };
        }
    }
}
