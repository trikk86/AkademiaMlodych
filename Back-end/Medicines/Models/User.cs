using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicines.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Date_of_Birth { get; set; }

        public List<Medicine> Medicines { get; set; }
    }
}