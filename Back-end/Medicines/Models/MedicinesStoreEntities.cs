using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Medicines.Models
{
    public class MedicinesStoreEntities : DbContext
    {
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
    }
}