using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Medicines.Models;

namespace Medicines.Controllers
{
    public class StoreController : Controller
    {
        MedicinesStoreEntities storeDB = new MedicinesStoreEntities();
        //
        // GET: /Store/
        public ActionResult Index(string searchString)
        {
            var us = from s in storeDB.Users select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                us = us.Where(s => s.Surname.Contains(searchString) || s.Name.Contains(searchString));
                return View(us);
            }


            var users = storeDB.Users.ToList();
            return View(users);
        }
        //
        // GET: /Store/Browse
        public ActionResult Browse(string user)
        {
            // Retrieve Genre and its Associated Albums from database
            var userModel = storeDB.Users.Include("Medicines").Single(g => g.Name == user);

            return View(userModel);
        }
        //
        // GET: /Store/Details
        public ActionResult Details(int id)
        {
            var medicine = storeDB.Medicines.Find(id);

            return View(medicine);
        }

       
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            if (ModelState.IsValid)
            {
                var v = storeDB.Users.SingleOrDefault(a => a.Name == u.Name && a.Password == u.Password);

               // if (u.Name == "admin") return RedirectToAction("Index", "StoreManager");

                if (v != null)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    //throw new Exception("zle podane dane logowania!");
                    ModelState.AddModelError("", "Niepoprawny login lub hasło");
                }
            }
            return View(u);
        }

        public ActionResult Register()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                storeDB.Users.Add(user);
                storeDB.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }
    }
}