using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfPortal.data.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PictureLink { get; set; }

        public Info Details { get; set; }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
