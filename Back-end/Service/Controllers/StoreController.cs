using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;

namespace Service.Controllers
{
    public class StoreController : Controller
    {
        MedicinesStoreEntities storeDB = new MedicinesStoreEntities();
        //
        // GET: /Store/
        public ActionResult Index()
        {
            var users = storeDB.Users.ToList();
            return View(users);
        }
        //
        // GET: /Store/Browse
        public ActionResult Browse(string user)
        {
            var userModel = new User { Name = user };
            return View(userModel);
        }
        //
        // GET: /Store/Details
        public ActionResult Details(int id)
        {
            var medicine = new Medicine { Medicine_Name = "Medicine " + id };
            return View(medicine);
        }
    }
}