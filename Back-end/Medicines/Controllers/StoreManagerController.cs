using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Medicines.Models;

namespace Medicines.Controllers
{ 
    public class StoreManagerController : Controller
    {
        private MedicinesStoreEntities db = new MedicinesStoreEntities();

        //
        // GET: /StoreManager/

        public ViewResult Index()
        {
            var medicines = db.Medicines.Include(m => m.User).Include(m => m.Doctor);
            return View(medicines.ToList());
        }

        //
        // GET: /StoreManager/Details/5

        public ViewResult Details(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            return View(medicine);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name");
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "Name");
            return View();
        } 

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Medicines.Add(medicine);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", medicine.UserId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "Name", medicine.DoctorId);
            return View(medicine);
        }
        
        //
        // GET: /StoreManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", medicine.UserId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "Name", medicine.DoctorId);
            return View(medicine);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", medicine.UserId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "Name", medicine.DoctorId);
            return View(medicine);
        }

        //
        // GET: /StoreManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            return View(medicine);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Medicine medicine = db.Medicines.Find(id);
            db.Medicines.Remove(medicine);
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