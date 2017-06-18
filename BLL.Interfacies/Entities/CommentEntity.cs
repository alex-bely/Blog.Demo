using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfacies.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime PublicationDate { get; set; }

        public int UserId { get; set; }

        public int ArticleId { get; set; }

        public virtual List<ArticleEntity> Articles { get; set; }

        public virtual List<UserEntity> Users { get; set; }
    }
}
