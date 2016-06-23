using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Frequency
    {
        [Key]
        public int FrequencyOptionId { get; set; }
        public string Description { get; set; }
    }
}