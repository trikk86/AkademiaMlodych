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
    public class UserController : Controller
    {
        private MedicinesStoreEntities db = new MedicinesStoreEntities();

        //
        // GET: /User/

        public JsonResult Index()
        {
            var users = db.Users.ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /User/Details/5

        public JsonResult Details(int id)
        {
            User user = db.Users.Find(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /User/Create

        [HttpPost]
        public JsonResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Json(new { redirectUrl = Url.Action("Index", "User"), isRedirect = true });
            }

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public JsonResult GetMedicines(int id)
        {
            var medicines = db.Medicines.Where(x => x.UserID == id);

            return Json(medicines, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetDoses(int id)
        { 
            var doses = db.Medicines.Include("Doses").Single(g => g.MedicineID == id);

            return Json(doses, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Browse(int id) // funkcja dodatkowa pokazująca wszystkie dawki danego użytkownika, gdzie "id" to przekazane "UserID"
        {

            var medicines = db.Medicines.Where(x => x.UserID == id);
            var dosesToShow = new List<Dose>();

            foreach (var item in medicines)
            {
                foreach (var dawka in db.Doses)
                {
                    if (dawka.MedicineID == item.MedicineID)
                        dosesToShow.Add(dawka);
                }
            }

            return Json(dosesToShow.ToList(), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Login(User u)
        {
            if (ModelState.IsValid)
            {
                var v = db.Users.SingleOrDefault(a => a.Email == u.Email && a.Password == u.Password);

                if (v != null)
                {
                    return Json(new { redirectUrl = Url.Action("Index", "User"), isRedirect = true });
                }
                else
                {
                    //throw new Exception("zle podane dane logowania!");
                    ModelState.AddModelError("", "Niepoprawny email lub hasło");
                }
            }
            return Json(u, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = db.Users.SingleOrDefault(e => e.Email == user.Email);
                if (checkEmail == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return Json(new { redirectUrl = Url.Action("Index", "User"), isRedirect = true });
                }
                else
                    ModelState.AddModelError("", "Konto o takim e mailu już istnieje!");
            }

            return Json(user, JsonRequestBehavior.AllowGet);
        }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        [HttpPost]
        public JsonResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { redirectUrl = Url.Action("Index", "User"), isRedirect = true });
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return Json(new { redirectUrl = Url.Action("Index", "User"), isRedirect = true });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}