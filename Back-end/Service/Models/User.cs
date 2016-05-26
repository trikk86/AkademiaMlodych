using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    class User
    {
        public int UserID { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Date_of_Birth { get; set; }

        public List<Dose> Medicines { get; set; }
    }

}