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
    public class DosesController : Controller
    {
        private MedicinesStoreEntities db = new MedicinesStoreEntities();

        //
        // GET: /Doses/

        public ViewResult Index()
        {
            var doses = db.Doses.Include(d => d.Medicine);
            return View(doses.ToList());
        }

        //
        // GET: /Doses/Details/5

        public ViewResult Details(int id)
        {
            Dose dose = db.Doses.Find(id);
            return View(dose);
        }

        //
        // GET: /Doses/Create

        public ActionResult Create()
        {
            ViewBag.MedicineID = new SelectList(db.Medicines, "MedicineID", "Medicine_Name");
            return View();
        } 

        //
        // POST: /Doses/Create

        [HttpPost]
        public ActionResult Create(Dose dose)
        {
            if (ModelState.IsValid)
            {
                db.Doses.Add(dose);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MedicineID = new SelectList(db.Medicines, "MedicineID", "Medicine_Name", dose.MedicineID);
            return View(dose);
        }
        
        //
        // GET: /Doses/Edit/5
 
        public ActionResult Edit(int id)
        {
            Dose dose = db.Doses.Find(id);
            ViewBag.MedicineID = new SelectList(db.Medicines, "MedicineID", "Medicine_Name", dose.MedicineID);
            return View(dose);
        }

        //
        // POST: /Doses/Edit/5

        [HttpPost]
        public ActionResult Edit(Dose dose)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dose).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MedicineID = new SelectList(db.Medicines, "MedicineID", "Medicine_Name", dose.MedicineID);
            return View(dose);
        }

        //
        // GET: /Doses/Delete/5
 
        public ActionResult Delete(int id)
        {
            Dose dose = db.Doses.Find(id);
            return View(dose);
        }

        //
        // POST: /Doses/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Dose dose = db.Doses.Find(id);
            db.Doses.Remove(dose);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}