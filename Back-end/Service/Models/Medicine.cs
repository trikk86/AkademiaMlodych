using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class Medicine
    {
        public int MedicineID { get; set; }
        public int UserID { get; set; }

        public string Medicine_Name { get; set; }
        public string Additional_Information { get; set; }

        public virtual User User { get; set; }
        public List<Dose> Doses { get; set; }
    }
}