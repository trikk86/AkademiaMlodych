using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Service.Models;
using System.Web.Http.Cors;

namespace WebApplication1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MedicineController : ApiController
    {

        private MedicinesStoreEntities db = new MedicinesStoreEntities();

        //
        // GET: /Medicines/
        [HttpGet]
        [Route("api/Medicine/")]
        public List<Medicine> Index()
        {
            return db.Medicines.ToList();
        }

        [HttpGet]
        [Route("api/Medicine/GetMedicinesForUser/{id}")]
        public List<Medicine> GetMedicines(int id)
        {
            var medicines = db.Medicines.Where(x => x.UserID == id);

            return medicines.ToList();
        }

        //
        // GET: /Medicines/Details/5
        [HttpGet]
        [Route("api/Medicine/Details/{id}")]
        public Medicine Details(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            return medicine;
        }


        //
        // POST: /Medicines/Create
        [HttpPost]
        [Route("api/Medicine/Create")] 
        public void Create([FromBody]Medicine medicine)
        {
            medicine.Doses = CreateDoses(medicine);
            db.Medicines.Add(medicine);
            db.SaveChanges();
        }

        [HttpGet]
        [Route("api/Medicine/GetDoses/{id}")]
        public List<Dose> GetDoses(int id)  //pobiera dawki dla danego leku
        {
            var doses = db.Doses.Where(g => g.MedicineID == id);

            return doses.ToList();
        }

        private List<Dose> CreateDoses(Medicine medicine)    // tworzy dawki dla danego leku

        {
            DateTime doseDate = medicine.Beginning_Date;
            TimeSpan difference = medicine.The_End_Date - medicine.Beginning_Date;

            DateTime doseDateNow;

            List<Dose> doseList = new List<Dose>();

            int howManyDays = (int)difference.TotalDays;

            switch (medicine.FrequencyOptionId)
            {
                case 1:
                    for (int i = 0; i <= howManyDays; i++)
                    {
                        doseDateNow = doseDate.AddDays(i);
                        Dose dawka = new Dose() { Date = doseDateNow, ifTaken = false };
                        doseList.Add(dawka);
                    }
                    break;

                case 2:
                    for (int i = 0; i <= howManyDays; i += 7)
                    {
                        doseDateNow = doseDate.AddDays(i);
                        Dose dawka = new Dose() { Date = doseDateNow, ifTaken = false };
                        doseList.Add(dawka);
                    }
                    break;

                case 3:
                    for (int i = 0; i <= howManyDays; i += int.Parse(medicine.FrequencyOptionValue))
                    {
                        doseDateNow = doseDate.AddDays(i);
                        Dose dawka = new Dose() { Date = doseDateNow, ifTaken = false };
                        doseList.Add(dawka);
                    }
                    break;

                default:        // gdy FrequencyOptionId >= 4, to są wybierane dni z pola "Description", np przy id==4 -> Description == "1,4", a przy id==5 -> Description =="5,6,7"
                    for (int i = 0; i <= howManyDays; i++)
                    {
                        doseDateNow = doseDate.AddDays(i);
                        var option = (int)doseDateNow.DayOfWeek;
                        if (option == 0) option = 7; //niedziela konwertowana intem zwraca 0

                        if (medicine.FrequencyOptionValue.Contains(option.ToString()))
                        {
                            Dose dawka = new Dose { Date = doseDateNow, ifTaken = false };
                            doseList.Add(dawka);
                        }
                    }
                    break;
            }

            return doseList;
        }

        [HttpGet]
        [Route("api/Medicine/DeleteDoses/{id}")]
        public void DeleteDoses(int id)   //kasuje dawki dla danego leku
        {
            var dosesToDelete =
            from details in db.Doses
            where details.MedicineID == id
            select details;

            foreach (var detail in dosesToDelete)
            {
                db.Doses.Remove(detail);
            }

            db.SaveChanges();
        
        }
        [HttpGet]
        [Route("api/Medicine/TakeDose/{id}")]
        public void TakeDose(int id) //funkcja "Weź lek - z aplikacji mobilnej"
        {
            db.Doses.Find(id).ifTaken = true;
            db.SaveChanges();   
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  

        //
        // POST: /Medicines/Edit/5
        [HttpPost]
        [Route("api/Medicine/Edit/{id}")]
        public void Edit(Medicine medicine)
        {
            db.Entry(medicine).State = EntityState.Modified;
            db.SaveChanges();
        }

        //
        // POST: /Medicines/Delete/5
        [HttpGet]
        [Route("api/Medicine/Delete/{id}")]
        public void DeleteConfirmed(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            DeleteDoses(id);
            db.Medicines.Remove(medicine);
            db.SaveChanges();
        }

    }
}