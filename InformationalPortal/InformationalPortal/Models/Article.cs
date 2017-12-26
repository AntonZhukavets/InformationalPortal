using System.ComponentModel.DataAnnotations;


namespace InformationalPortal.Models
{
    public class Article
    {
        public int AutherId { get; set; }
        [Display(Name = "Autor: ")]
        public string AutherName { get; set; }
        public int Id { get; set; }
        [Display(Name = "Title of the article")]
        [Required(ErrorMessage="Article name is required")]
        public string Name { get; set; }

        [Display(Name = "Picture")]
        public string PictureLink { get; set; }

        public Info Details { get; set; }
        public Heading[] Headings { get; set; }
        [Display(Name = "Links to another articles")]
        public ArticleLink[] ArticleLinks { get; set; }
    }
}