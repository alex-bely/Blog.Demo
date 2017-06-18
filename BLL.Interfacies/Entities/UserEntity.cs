using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfacies.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public byte[] Avatar { get; set; }


        public virtual List<ArticleEntity> Articles { get; set; }

        public virtual List<CommentEntity> Comments { get; set; }

        public virtual List<RoleEntity> Roles { get; set; }
    }
}
