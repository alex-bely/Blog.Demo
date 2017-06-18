using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfacies.Entities
{
    public class ArticleEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublicationDate { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        public virtual List<CommentEntity> Comments { get; set; }

        public virtual List<TagEntity> Tags { get; set; }
    }
}
