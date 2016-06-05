using Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace Service.Controllers
{
    public class MedsController : apiController
    {
        private MedicinesStoreEntities db = new MedicinesStoreEntities();

        //
        // GET: /Meds/

        public string Get(int id)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(db.Meds.Select(a => a.userId == id).ToList());
        }

        //
        // POST: /Meds/Create

        [HttpPost]
        public void Add(Med medicine)
        {
            medicine.Doses = CreateDoses(medicine);

            db.Meds.Add(medicine);
            db.SaveChanges();

            return;
        }
        
        [HttpPost]
        public void Edit(Med medicine)
        {
            var medtoremove = from b in db.Meds
                                where b.medId.Equals(medicine.medId)
                                select b;
            db.Meds.Remove(medtoremove.Single());
            var dosestoremove = from b in db.Doses
                        where b.parentId.Equals(medicine.medId)
                        select b;
            db.Doses.RemoveRange(dosestoremove);
            db.Doses.AddRange(medicine.Doses);
            db.Meds.Add(medicine);
            db.SaveChanges();
            return;
        }

        public List<Dose> CreateDoses(Med medicine)
        {
            TimeSpan Difference = medicine.The_End_Time - medicine.Beginning_Date;
            int HowManyDays = (int)Difference.TotalDays;

            List<Dose> doses = new List<Dose> { };

            for (int i = 0; i < HowManyDays; i++)
            {
                Dose dose = new Dose() { parentId = medicine.medId, date = new DateTime(medicine.Beginning_Date.Year, medicine.Beginning_Date.Month, medicine.Beginning_Date.Day + i, medicine.Beginning_Date.Hour, medicine.Beginning_Date.Minute, medicine.Beginning_Date.Second), ifTaken = false, parent = medicine};
                doses.Add(dose);
            }

            return doses;
        }

        public List<Dose> GetDoses(int id)
        {
            var doses = db.Doses.Where(x => x.parentId == id).ToList();


            return doses;
        }

        //
        // POST: /Meds/Delete/5

        [HttpPost, ActionName("Delete")]
        public void DeleteConfirmed(int id)
        {            
            Med medicine = db.Meds.SingleOrDefault(a => a.medId == id);
            db.Meds.Remove(medicine);
            db.SaveChanges();
            return;
        }
    }
}