using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfPortal.service.Entities
{
    public class ArticleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PictureLink { get; set; }

        public InfoEntity Details { get; set; }
    }
}