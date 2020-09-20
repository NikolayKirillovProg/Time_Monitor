using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeMonitor.ViewModels
{
    public class ReportViewModel
    {
        public Guid Rep_id { get; set; }

        public int User_id { get; set; }

        public string Summary { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsValid { get; set; }

        public DateTime Date { get; set; }
    }
}