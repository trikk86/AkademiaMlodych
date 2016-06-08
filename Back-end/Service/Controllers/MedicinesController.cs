using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;

namespace Service.Controllers
{
    public class MedicinesController : Controller
    {
        private MedicinesStoreEntities db = new MedicinesStoreEntities();

        //
        // GET: /Medicines/

        public ActionResult Index()
        {
            return Json(db.Medicines.ToList(), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Medicines/Details/5

        public JsonResult Details(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            return Json(medicine, JsonRequestBehavior.AllowGet);
        }


        //
        // POST: /Medicines/Create

        [HttpPost]
        public JsonResult Create(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                medicine.Doses = CreateDoses(medicine);
                db.Medicines.Add(medicine);
                db.SaveChanges();
                return Json(new { redirectUrl = Url.Action("Index", "Medicines"), isRedirect = true });
            }

            return Json(medicine, JsonRequestBehavior.AllowGet);
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        public JsonResult GetDoses(int id)  //pobiera dawki dla danego leku
        {
            var doses = db.Medicines.Include("Doses").Single(g => g.MedicineID == id);

            return Json(doses, JsonRequestBehavior.AllowGet);
        }


        public List<Dose> CreateDoses(Medicine medicine)    // tworzy dawki dla danego leku
        {

            List<Dose> DoseList = new List<Dose>();

            TimeSpan Difference = medicine.The_End_Time - medicine.Beginning_Date;
            int HowManyDays = (int)Difference.TotalDays;

            medicine.FrequencyOptionValue = "Monday";

            if (medicine.FrequencyOptionValue != null && medicine.FrequencyOptionId == 0)
            {
                for (int i = 0; i < HowManyDays; i++)
                {
                    DateTime DoseDay = new DateTime(medicine.Beginning_Date.Year, medicine.Beginning_Date.Month, medicine.Beginning_Date.Day + i, medicine.Beginning_Date.Hour, medicine.Beginning_Date.Minute, medicine.Beginning_Date.Second);
                    string Option = DoseDay.DayOfWeek.ToString();
                    if (medicine.FrequencyOptionValue.Contains(Option))
                    {
                        Dose dawka = new Dose() { Date = new DateTime(medicine.Beginning_Date.Year, medicine.Beginning_Date.Month, medicine.Beginning_Date.Day + i, medicine.Beginning_Date.Hour, medicine.Beginning_Date.Minute, medicine.Beginning_Date.Second), ifTaken = false };
                        DoseList.Add(dawka);
                    }
                }

            }
            else if (medicine.FrequencyOptionId != 0 && medicine.FrequencyOptionValue == null)
            {
                for (int i = 0; i < HowManyDays; i += medicine.FrequencyOptionId)
                {
                    Dose dawka = new Dose() { Date = new DateTime(medicine.Beginning_Date.Year, medicine.Beginning_Date.Month, medicine.Beginning_Date.Day + i, medicine.Beginning_Date.Hour, medicine.Beginning_Date.Minute, medicine.Beginning_Date.Second), ifTaken = false };
                    DoseList.Add(dawka);

                }


            }
            return DoseList;
        }

        public JsonResult DeleteDoses(int id)   //kasuje dawki dla danego leku
        { 
            var DosesToDelete =
            from details in db.Doses
            where details.MedicineID == id
            select details;

            foreach (var detail in DosesToDelete)
            {
                db.Doses.Remove(detail);
            }

            db.SaveChanges();
            return Json(new { redirectUrl = Url.Action("Index", "Medicines"), isRedirect = true });
        }

        public JsonResult TakeDose(int id) //funkcja "Weź lek - z aplikacji mobilnej"
        {
            db.Doses.Find(id).ifTaken = true;
            db.SaveChanges();

            return null;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  

        //
        // POST: /Medicines/Edit/5

        [HttpPost]
        public JsonResult Edit(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicine).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { redirectUrl = Url.Action("Index", "Medicines"), isRedirect = true });
            }
            return Json(medicine, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Medicines/Delete/5

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            db.Medicines.Remove(medicine);
            db.SaveChanges();
            return Json(new { redirectUrl = Url.Action("Index", "Medicines"), isRedirect = true });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}