using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationalPortal.Models
{
    public class ArticlePreView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureLink { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }        

    }
}