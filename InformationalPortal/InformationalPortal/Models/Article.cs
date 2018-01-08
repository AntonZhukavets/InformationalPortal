using System.ComponentModel.DataAnnotations;


namespace InformationalPortal.Models
{
    public class Article
    {
        private string name;
        public int AutherId { get; set; }
        [Display(Name = "Autor: ")]
        public string AutherName { get; set; }
        public int Id { get; set; }
        [Display(Name = "Title of the article")]
        [Required(ErrorMessage="Article name is required")]
        public string Name 
        {
            get
            {
                if(name.Length<30)
                {
                    return name;
                }
                else
                {
                    string shortName = string.Empty;
                    for(int i=0;i<27;i++)
                    {
                        shortName += name[i];
                    }
                    shortName += "...";
                    return shortName;
                }
            }
            set 
            {
                name = value;
            } 
        }

        [Display(Name = "Picture")]
        public string PictureLink { get; set; }

        public byte[] Picture { get; set; }
        public Info Details { get; set; }
        public Heading[] Headings { get; set; }
        [Display(Name = "Links to another articles")]
        public ArticleLink[] ArticleLinks { get; set; }
    }
}