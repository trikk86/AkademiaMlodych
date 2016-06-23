using System;
using System.Collections.Generic;
using System.Data.Entity;
using Service.Models;

namespace WebApplication1.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<MedicinesStoreEntities>
    {
        protected override void Seed(MedicinesStoreEntities context)
        {
            var users = new List<User>
            {

                new User { Name = "Janusz", Date_of_Birth = new DateTime(2014, 6, 14) },
                new User { Name = "Agnieszka", Date_of_Birth = new DateTime(2014, 6, 14) },

                new User { Name = "Danuta", Date_of_Birth = new DateTime(2014, 6, 14) },
                new User { Name = "Krzysztof", Date_of_Birth = new DateTime(2014, 6, 14) },
            };


            var medicines = new List<Medicine>
            {
                new Medicine { Medicine_Name = "Omega3", Beginning_Date=new DateTime(2014, 6, 14), The_End_Date=new DateTime(2014, 6, 14)  },
                new Medicine { Medicine_Name = "Witamina A", Beginning_Date=new DateTime(2014, 6, 14), The_End_Date=new DateTime(2014, 6, 14)  },
                new Medicine { Medicine_Name = "Witamina D", Beginning_Date=new DateTime(2014, 6, 14), The_End_Date=new DateTime(2014, 6, 14)  },
            };


            new List<Dose>
            {
                new Dose { Date=new DateTime(2015, 5, 15), ifTaken = false },
            }.ForEach(a => context.Doses.Add(a));

            new List<Frequency>
            {
                new Frequency { Description = "1"},
                new Frequency { Description = "2"},
                new Frequency { Description = "3"},
            }.ForEach(f => context.Frequency.Add(f));
        }

    }
}

