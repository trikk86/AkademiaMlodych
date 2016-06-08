using System.Data.Entity;
using Service.Models;

namespace Service.Models
{
    public class MedicinesStoreEntities : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Dose> Doses { get; set; }
    }
}
