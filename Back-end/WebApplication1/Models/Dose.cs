using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class Dose
    {
        public int DoseID { get; set; }
        public int MedicineID { get; set; }

        public DateTime Date { get; set; }
        public bool ifTaken { get; set; }

        //public virtual Medicine Medicine { get; set; }
    }
}