using System;


namespace InfPortal.business.DTO
{
    public class InfoDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public LanguageDTO Language { get; set; }
        public DateTime Date { get; set; }
        public string VideoLink { get; set; }
    }
}
