using Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace Service.Controllers
{
    public class MedsController : ApiController
    {
        private MedicinesStoreEntities db = new MedicinesStoreEntities();

        //
        // GET: /Meds/

        public string Get(int userId)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(db.Meds.Select(a => a.userId == userId).ToList());
        }

        //
        // POST: /Meds/Create

        [HttpPost]
        public void Add(Med medicine)
        {
            medicine.doses = CreateDoses(medicine);

            db.Meds.Add(medicine);
            db.SaveChanges();

            return;
        }
        //przeliczyć dawki
        [HttpPost]
        public void Edit(Med medicine)
        {
            var medtoremove = db.Meds.SingleOrDefault(a => a.medId == medicine.medId);
            db.Meds.Remove(medtoremove);
            db.Meds.Add(medicine);
            db.SaveChanges();
            return;
        }
        
        public List<Dose> CreateDoses(Med medicine)
        {
            TimeSpan Difference = medicine.endDate - medicine.startDate;
            int HowManyDays = (int)Difference.TotalDays;

            List<Dose> doses = new List<Dose> { };

            for (int i = 0; i < HowManyDays; i++)
            {
                Dose dose = new Dose() { parentId = medicine.medId, date = new DateTime(medicine.startDate.Year, medicine.startDate.Month, medicine.startDate.Day + i, medicine.startDate.Hour, medicine.startDate.Minute, medicine.startDate.Second), ifTaken = false, parent = medicine};
                doses.Add(dose);
            }

            return doses;
        }

        //
        // POST: /Meds/Delete/5

        [HttpDelete]
        public void Delete(int id)
        {            
            Med medicine = db.Meds.SingleOrDefault(a => a.medId == id);
            db.Meds.Remove(medicine);
            db.SaveChanges();
            return;
        }
    }
}