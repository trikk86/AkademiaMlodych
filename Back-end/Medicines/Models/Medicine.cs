using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicines.Models
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public int UserId { get; set; }
        public int DoctorId { get; set; }

        public string Medicine_Name { get; set; }
        public string Dose { get; set; }
        //public string Additional_Information { get; set; }
        //public DateTime Beginning_Date { get; set; }
        //public DateTime The_End_Time { get; set; }
        //public int Tolerance_Hour { get; set; }
        ////public DateTimeOffset Iteration { get; set; }
        //public int FrequencyOptionId { get; set; }
        //public int FrequencyOptionValue { get; set; }
        public string MedicineArtUrl { get; set; }

        public User User { get; set; }
        public Doctor Doctor { get; set; }
    }
}