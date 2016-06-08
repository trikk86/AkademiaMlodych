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

        public string amount { get; set; }

        public string comment { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int tolerance { get; set; }
        
        public int freqId { get; set; }
        public string freqValue { get; set; }

        public List<Dose> doses { get; set; } //ta lista jest pusta kiedy przychodzi z javascriptu, serwis ją wypełnia korzystając z reszty parametrów
    }
}