using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1.Models
{
    class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection2")
        {
            Database.Initialize(false);
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
