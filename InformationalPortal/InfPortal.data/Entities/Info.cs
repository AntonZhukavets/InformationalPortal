﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfPortal.data.Entities
{
    public class Info
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Language { get; set; }
        public DateTime Date { get; set; }
        public string VideoLink { get; set; }
    }
}
