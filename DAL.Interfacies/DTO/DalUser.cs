using System;
using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalUser : IEntity
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime? RegistrationDate { get; set; }
        
        public byte[] Avatar { get; set; }
        

        public virtual List<DalArticle> Articles { get; set; }

        public virtual List<DalComment> Comments { get; set; }

        public virtual List<DalRole> Roles { get; set; } 
    }
}
