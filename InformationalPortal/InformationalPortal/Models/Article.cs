using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InformationalPortal.Models
{
    public class Article
    {
        public int AutherId { get; set; }
        
        public string AutherName { get; set; }
        public int Id { get; set; }
        [Display(Name = "Title of the article")]
        [Required(ErrorMessage="Article name is required")]
        public string Name { get; set; }

        public string PictureLink { get; set; }

        public Info Details { get; set; }
        public Heading Headings { get; set; }
    }
}