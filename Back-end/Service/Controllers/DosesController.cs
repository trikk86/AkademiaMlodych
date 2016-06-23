using Service.Models;
using System.Web.Http;

namespace Service.Controllers
{
    public class DosesController : ApiController
    {
        private MedicinesStoreEntities db = new MedicinesStoreEntities();
        public void TakeDose(int id)
        {
            //Dose dose = db.Doses.Find(id);
            db.Doses.Find(id).ifTaken = true;
            db.SaveChanges();


            return;
        }
    }
}
