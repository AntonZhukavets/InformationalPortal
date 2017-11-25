using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfPortal.business.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PictureLink { get; set; }

        public InfoDTO Details { get; set; }
    }
}
