using System;


namespace InfPortal.service.Entities
{
    public class InfoEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public LanguageEntity Language { get; set; }
        public DateTime Date { get; set; }
        public string VideoLink { get; set; }
    }
}