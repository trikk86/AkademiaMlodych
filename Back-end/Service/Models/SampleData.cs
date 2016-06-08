using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Service.Models
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
                new Medicine { Medicine_Name = "Omega3", Beginning_Date=new DateTime(2014, 6, 14), The_End_Time=new DateTime(2014, 6, 14)  },
                new Medicine { Medicine_Name = "Witamina A", Beginning_Date=new DateTime(2014, 6, 14), The_End_Time=new DateTime(2014, 6, 14)  },
                new Medicine { Medicine_Name = "Witamina D", Beginning_Date=new DateTime(2014, 6, 14), The_End_Time=new DateTime(2014, 6, 14)  },
            };


            new List<Dose>
            {
                new Dose { Date=new DateTime(2015, 5, 15), ifTaken = false },
            }.ForEach(a => context.Doses.Add(a));
        }
    }
}

