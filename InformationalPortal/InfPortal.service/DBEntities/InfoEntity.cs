using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfPortal.service.DBEntities
{
    public class InfoEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Language { get; set; }
        public DateTime Date { get; set; }
        public string VideoLink { get; set; }
    }
}