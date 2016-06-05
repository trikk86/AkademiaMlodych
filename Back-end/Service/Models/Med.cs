using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Klasa pobierana ze strony

namespace Service.Models
{
    public class Med
    {
        //To jest klasa którą przekazuje się do bazy danych, taką samą dostaje javascript kiedy prosi o listę leków
        
        public int medId { get; set; }

        public int userId { get; set; }

        public string medName { get; set; }

        public string dose { get; set; }

        public string comment { get; set; }

        public DateTime Beginning_Date { get; set; }

        public DateTime The_End_Time { get; set; }

        public int Tolerance_Hour { get; set; }
        
        public int FrequencyOptionId { get; set; }
        public string FrequencyOptionValue { get; set; }

        public List<Dose> Doses { get; set; } //ta lista jest pusta kiedy przychodzi z javascriptu, serwis ją wypełnia korzystając z reszty parametrów
    }
}