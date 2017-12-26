using System;
using System.ComponentModel.DataAnnotations;


namespace InformationalPortal.Models
{
    public class Info
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Language Language { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "Link to video on youtube")]
        public string VideoLink { get; set; }
    }
}