using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfacies.Entities
{
    public class TagEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<ArticleEntity> Articles { get; set; }
    }
}
