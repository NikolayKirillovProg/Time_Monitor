using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeMonitor.Entities
{
    public class Report
    {
        public Guid Rep_id { get; set; }

        public int User_id { get; set; }

        public string Summary { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
    }
}