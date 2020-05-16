using bookcity.entitys.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace bookcity.entitys.ViewModel
{
    public class bookResult_ViewModel
    {

        public string id { get; set; }

        public string bookName { get; set; }

        public string author { get; set; }

        public string intro { get; set; }

        public string category { get; set; }

        public string bookImg { get; set; }

        public DateTime? lastUpdate { get; set; }

        public object addbox { get; set; }
    }
}
