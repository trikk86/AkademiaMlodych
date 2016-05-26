using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Klasa pobierana ze strony

namespace Service.Models
{
    public class Med
    {
        public int DoseId { get; set; }
        public DateTime Beginning_Date { get; set; }
        public DateTime The_End_Time { get; set; }
        public int Tolerance_Hour { get; set; }
        
        //public DateTimeOffset Iteration { get; set; }
        public int FrequencyOptionId { get; set; }
        public int FrequencyOptionValue { get; set; }
    }
}