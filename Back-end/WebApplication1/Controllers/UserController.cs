using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Service.Models;
using WebApplication1.Models;
using System.Web.Http.Cors;

namespace WebApplication1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private MedicinesStoreEntities db = new MedicinesStoreEntities();

        //
        // GET: /User/
        [HttpGet]
        [Route("api/User/")]
        public List<User> Index()
        {
            var users = db.Users.ToList();
            return users;
        }

        //
        // GET: /User/Details/5
        [HttpGet]
        [Route("api/User/Details/{id}")]
        public User Details(int id)
        {
            User user = db.Users.Find(id);
            return user;
        }

        //
        // POST: /User/Create
        [HttpPost]
        [Route("api/User/Create")]
        public void Create(User user)
        { 
               db.Users.Add(user);
               db.SaveChanges();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        //public jsonresult browse(int id) // funkcja dodatkowa pokazująca wszystkie dawki danego użytkownika, gdzie "id" to przekazane "userid"
        //{

        //    var medicines = db.medicines.where(x => x.userid == id);
        //    var dosestoshow = new list<dose>();

        //    foreach (var item in medicines)
        //    {
        //        foreach (var dawka in db.doses)
        //        {
        //            if (dawka.medicineid == item.medicineid)
        //                dosestoshow.add(dawka);
        //        }
        //    }

        //    return json(dosestoshow.tolist(), jsonrequestbehavior.allowget);
        //}


        [HttpGet]
        [Route("Login")]
        public User Login(string email, string password)
        {
            var user = db.Users.SingleOrDefault(a => a.Email == email && a.Password == password);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            else
            {
                return user;
            }
    }



        //public JsonResult Register(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var checkEmail = db.Users.SingleOrDefault(e => e.Email == user.Email);
        //        if (checkEmail == null)
        //        {
        //            db.Users.Add(user);
        //            db.SaveChanges();
        //            return Json(new { redirectUrl = Url.Action("Index", "User"), isRedirect = true });
        //        }
        //        else
        //            ModelState.AddModelError("", "Konto o takim e mailu już istnieje!");
        //    }

        //    return Json(user, JsonRequestBehavior.AllowGet);
        //}

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


      
        //public JsonResult Edit(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(user).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return Json(new { redirectUrl = Url.Action("Index", "User"), isRedirect = true });
        //    }
        //    return Json(user, JsonRequestBehavior.AllowGet);
        //}

        //
        // POST: /User/Delete/5

        
        //public JsonResult DeleteConfirmed(int id)
        //{
        //    User user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return Json(new { redirectUrl = Url.Action("Index", "User"), isRedirect = true });
        //}

    }
}