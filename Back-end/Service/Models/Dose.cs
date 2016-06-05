using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class Dose
    {
        //to jest klasa pomocnicza - pojedyncze wystąpienie leku, nie ma info o leku bo wszystkie są w "klasie matce" - Med

        public DateTime date { get; set; }
        public bool ifTaken { get; set; }

        public int parentId { get; set; }
        public virtual Med parent { get; set; }
    }
}