using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeMonitor.ViewModels;

namespace TimeMonitor.ViewModels
{
    public class UserViewModel
    {
        public int User_Id { get; set; }

        public string Email { get; set; }

        public string Name_1 { get; set; }

        public string Name_2 { get; set; }

        public string Name_3 { get; set; }

        public bool IsDeleted { get; set; }

        public bool EmailIsValid { get; set; }

        public bool UserIsValid { get; set; }

        public virtual List<ReportViewModel> Reports { get; set; }
    }
}
