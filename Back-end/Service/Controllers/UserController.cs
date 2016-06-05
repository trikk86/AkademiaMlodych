using System.Linq;
using Service.Models;
using System.Web.Http;
using System;

namespace Service.Controllers
{
    public class UsersController : ApiController
    {
        private MedicinesStoreEntities db = new MedicinesStoreEntities();

        //
        // GET: /User/Details/5

        public string Get(int id)
        {
            User user = db.Users.Find(id);
            return Newtonsoft.Json.JsonConvert.SerializeObject(user);
        }

        // POST: /User/Create

        [HttpPost]
        public bool Create(User user)
        {
                db.Users.Add(user);
                db.SaveChanges();
                return true;  
        }

        [HttpPost]
        public string Login([FromBody]string mail, string password)
        {

                var v = db.Users.SingleOrDefault(a => a.Email == mail && a.Password == password);

                if (v != null)
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(v);
                }
                else
                {
                    throw new Exception("Złe dane logowania!");
                }
            
        }

        [HttpPost]
        public void Register([FromBody]User user)
        {

                db.Users.Add(user);
                db.SaveChanges();

            return;
        }
    }
}