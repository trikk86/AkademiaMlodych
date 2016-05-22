using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Medicines.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<MedicinesStoreEntities>
    {
        protected override void Seed(MedicinesStoreEntities context)
        {
            var users = new List<User>
            {
                
                new User { Name = "Janusz", Date_of_Birth = DateTime.Parse("2002-09-01")},
                new User { Name = "Agnieszka", Date_of_Birth = DateTime.Parse("2002-09-01") },

                new User { Name = "Danuta", Date_of_Birth = DateTime.Parse("2002-09-01")},
                new User { Name = "Krzysztof", Date_of_Birth = DateTime.Parse("2002-09-01") },
            };

            var doctors = new List<Doctor>
            {
                new Doctor { Name = "Alergolog" },
                new Doctor { Name = "Chirurg" },
                new Doctor { Name = "Internista" },
               
            };

            new List<Medicine>
            {
                new Medicine { Medicine_Name = "Omega3", User = users.Single(g => g.Name == "Krzysztof"), Dose = "2", Doctor = doctors.Single(a => a.Name == "Alergolog"), MedicineArtUrl = "/Content/Images/placeholder.gif" },
                new Medicine { Medicine_Name = "Magnez", User = users.Single(g => g.Name == "Janusz"), Dose = "3", Doctor = doctors.Single(a => a.Name == "Alergolog"), MedicineArtUrl = "/Content/Images/placeholder.gif" },
                new Medicine { Medicine_Name = "Witamina A", User = users.Single(g => g.Name == "Krzysztof"), Dose = "4", Doctor = doctors.Single(a => a.Name == "Chirurg"), MedicineArtUrl = "/Content/Images/placeholder.gif" },
                new Medicine { Medicine_Name = "Witamina B", User = users.Single(g => g.Name == "Danuta"), Dose = "5", Doctor = doctors.Single(a => a.Name == "Chirurg"), MedicineArtUrl = "/Content/Images/placeholder.gif" },
                new Medicine { Medicine_Name = "Witamina C", User = users.Single(g => g.Name == "Krzysztof"), Dose = "6", Doctor = doctors.Single(a => a.Name == "Internista"), MedicineArtUrl = "/Content/Images/placeholder.gif" },
                new Medicine { Medicine_Name = "Witamina D", User = users.Single(g => g.Name == "Danuta"), Dose = "7", Doctor = doctors.Single(a => a.Name == "Internista"), MedicineArtUrl = "/Content/Images/placeholder.gif" },

                new Medicine { Medicine_Name = "TermlineFast", User = users.Single(g => g.Name == "Agnieszka"), Dose = "8", Doctor = doctors.Single(a => a.Name == "Alergolog"), MedicineArtUrl = "/Content/Images/placeholder.gif" },
                new Medicine { Medicine_Name = "TermlineMen", User = users.Single(g => g.Name == "Agnieszka"), Dose = "7", Doctor = doctors.Single(a => a.Name == "Chirurg"), MedicineArtUrl = "/Content/Images/placeholder.gif" },
                new Medicine { Medicine_Name = "NieChrap", User = users.Single(g => g.Name == "Agnieszka"), Dose = "6", Doctor = doctors.Single(a => a.Name == "Internista"), MedicineArtUrl = "/Content/Images/placeholder.gif" },

                new Medicine { Medicine_Name = "Omega3", User = users.Single(g => g.Name == "Janusz"), Dose = "6", Doctor = doctors.Single(a => a.Name == "Internista"), MedicineArtUrl = "/Content/Images/placeholder.gif" },
            }.ForEach(x => context.Medicines.Add(x));
        }
    }
}