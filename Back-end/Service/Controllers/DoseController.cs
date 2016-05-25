using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Models;

namespace Service.Controllers
{
    public class DoseController : ApiController
    {
        //BLA BLA BLA BLA BLA BLA BLA BLA BLA BLA BLA BLA BLA BLA BLA BLA BLA BLA 

        List<Dose> doses = new List<Dose>
        {
            new Dose()
            {
                doseId = 1,
                userId = 1,

                drug_name = "Witamina C",
                dose = "2 tabletki",
                what_time = "12:00",
                how_long = "10",
                start_day = "05/01/2016",
                end_day = "",
                comment = "",
                freq = 1,
                freq_opts = new int[] {}
            }
        };
        
        // GET: api/Dose/5
        public string Get(int id)
        {
            List<Dose> userDoses = doses.FindAll(dose => dose.userId == id);

            return Newtonsoft.Json.JsonConvert.SerializeObject(userDoses);
        }

        // POST: api/Dose
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/Dose/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Dose/5
        public void Delete(int id)
        {
        }
    }
}
