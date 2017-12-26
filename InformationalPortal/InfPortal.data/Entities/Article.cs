namespace InfPortal.data.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PictureLink { get; set; }

        public Info Details { get; set; }
        public Heading[] Headings { get; set; }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public ArticleLink[] ArticleLinks { get; set; }
    }
}
