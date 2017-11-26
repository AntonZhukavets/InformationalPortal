using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationalPortal.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PictureLink { get; set; }

        public Info Details { get; set; }
    }
}