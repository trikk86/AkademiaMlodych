using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    class Dose
    {
        public int doseId { get; set; }
        public int userId { get; set; }

        public string drug_name { get; set; }
        public string dose { get; set; }
        public string what_time { get; set; }
        public string how_long { get; set; }
        public string start_day { get; set; }
        public string end_day { get; set; }
        public string comment { get; set; }
        public int freq { get; set; }
        public int[] freq_opts { get; set; }
    }
}
