using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfacies.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<UserEntity> Users { get; set; }
    }
}
