using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{ 
    [Serializable]
    public class Medicine
    {
        public int MedicineID { get; set; }

        public int UserID { get; set; }

        public string Medicine_Name { get; set; }

        public string Amount { get; set; }

        public string Comment { get; set; }
        public DateTime Beginning_Date { get; set; }
        public DateTime The_End_Time { get; set; }
        public int Tolerance_Hour { get; set; }

        public int FrequencyOptionId { get; set; }
        public string FrequencyOptionValue { get; set; }

        public virtual List<Dose> Doses { get; set; }
    }
}