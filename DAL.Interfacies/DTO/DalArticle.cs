using System;
using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalArticle : IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublicationDate { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        public virtual List<DalComment> Comments { get; set; }

        public virtual List<DalTag> Tags { get; set; }
    }
}
