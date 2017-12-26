using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformationalPortal.Models
{
    public class AdvancedSearchModel
    {
        public DateTime SearchFrom { get; set; }
        public DateTime SearchTo { get; set; }
        public int? HeadingId { get; set; }
    }
}