using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Service.Models;

namespace parents.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<MedicinesStoreEntities>
    {
        protected override void Seed(MedicinesStoreEntities context)
        {
            new List<User>
            {
                
                new User { Name = "Janusz"},
                new User { Name = "Agnieszka" },
                new User { Name = "Danuta"},
                new User { Name = "Krzysztof" },
            }.ForEach(x => context.Users.Add(x));


            var medicines = new List<Med>
            {
                new Med { medName = "Omega3", comment="nic", startDate=new DateTime(2014, 6, 14, 6, 32, 0), endDate=new DateTime(2014, 6, 14, 6, 32, 0) },
                new Med { medName = "Magnez",comment="nic", startDate=new DateTime(2014, 6, 14, 6, 32, 0), endDate=new DateTime(2014, 6, 14, 6, 32, 0) },
                new Med { medName = "Witamina A", comment="nic", startDate=new DateTime(2014, 6, 14, 6, 32, 0), endDate=new DateTime(2014, 6, 14, 6, 32, 0) },
                new Med { medName = "Witamina B", comment="nic", startDate=new DateTime(2014, 6, 14, 6, 32, 0), endDate=new DateTime(2014, 6, 14, 6, 32, 0) },
                new Med { medName = "Witamina C", comment="nic", startDate=new DateTime(2014, 6, 14, 6, 32, 0), endDate=new DateTime(2014, 6, 14, 6, 32, 0) },
                new Med { medName = "Witamina D", comment="nic", startDate=new DateTime(2014, 6, 14, 6, 32, 0), endDate=new DateTime(2014, 6, 14, 6, 32, 0) },

                new Med { medName = "TermlineFast", comment="nic", startDate=new DateTime(2014, 6, 14, 6, 32, 0), endDate=new DateTime(2014, 6, 14, 6, 32, 0) },
                new Med { medName = "TermlineMen", comment="nic", startDate=new DateTime(2014, 6, 14, 6, 32, 0), endDate=new DateTime(2014, 6, 14, 6, 32, 0) },
                new Med { medName = "NieChrap", comment="nic", startDate=new DateTime(2014, 6, 14, 6, 32, 0), endDate=new DateTime(2014, 6, 14, 6, 32, 0) },

                new Med { medName = "Omega3", comment="nic", startDate=new DateTime(2014, 6, 14, 6, 32, 0), endDate=new DateTime(2014, 6, 14, 6, 32, 0) },
            };

            new List<Dose> 
            {
                new Dose { date = new DateTime(2013, 5, 15, 5, 25, 0), parent = medicines.Single(x => x.medName == "Omega3")},
                new Dose { date = new DateTime(2013, 5, 15, 5, 25, 0), parent = medicines.Single(x => x.medName == "Magnez")},
                new Dose { date = new DateTime(2013, 5, 15, 5, 25, 0), parent = medicines.Single(x => x.medName == "Witamina A")},
                new Dose { date = new DateTime(2013, 5, 15, 5, 25, 0), parent = medicines.Single(x => x.medName == "Witamina B")},
                new Dose { date = new DateTime(2013, 5, 15, 5, 25, 0), parent = medicines.Single(x => x.medName == "Witamina C")},
                new Dose { date = new DateTime(2013, 5, 15, 5, 25, 0), parent = medicines.Single(x => x.medName == "Witamina D")},
                new Dose { date = new DateTime(2013, 5, 15, 5, 25, 0), parent = medicines.Single(x => x.medName == "TermlineFast")},
                new Dose { date = new DateTime(2013, 5, 15, 5, 25, 0), parent = medicines.Single(x => x.medName == "TermlineMen")},
                new Dose { date = new DateTime(2013, 5, 15, 5, 25, 0), parent = medicines.Single(x => x.medName == "NieChrap")},
                
            }.ForEach(a => context.Doses.Add(a));
        }
    }
}