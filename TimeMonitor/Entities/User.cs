using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeMonitor.Entities
{
    public class User
    {
        public int User_Id { get; set; }

        public string Email { get; set; }

        public string Name_1 { get; set; }

        public string Name_2 { get; set; }

        public string Name_3 { get; set; }

        public bool IsDeleted { get; set; }

        public virtual List<Report> Reports { get; set; }
    }
}