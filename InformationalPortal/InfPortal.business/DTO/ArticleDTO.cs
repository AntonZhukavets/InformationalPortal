namespace InfPortal.business.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureLink { get; set; }
        public byte[] Picture { get; set; }
        public InfoDTO Details { get; set; }
        public HeadingDTO[] Headings { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public ArticleLinkDTO[] ArticleLinks { get; set; } 
    }
}
