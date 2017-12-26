using System;


namespace InfPortal.data.Entities
{
    public class Info
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Language Language { get; set; }
        public DateTime Date { get; set; }
        public string VideoLink { get; set; }
    }
}
