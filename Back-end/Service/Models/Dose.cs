using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class Dose
    {
        public int DoseId { get; set; }
        public DateTime Beginning_Date { get; set; }
        public DateTime The_End_Time { get; set; }
        public int Tolerance_Hour { get; set; }

        //public DateTimeOffset Iteration { get; set; }
        //public int FrequencyOptionId { get; set; }
        //public int FrequencyOptionValue { get; set; }

        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thurday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}