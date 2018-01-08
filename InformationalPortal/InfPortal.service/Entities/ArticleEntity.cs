using System;


namespace InfPortal.service.Entities
{
    public class ArticleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureLink { get; set; }
        public byte[] Picture { get; set; }
        public InfoEntity Details { get; set; }
        public HeadingEntity[] Headings { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public ArticleLinkEntity[] ArticleLinks { get; set; }
       
    }
}