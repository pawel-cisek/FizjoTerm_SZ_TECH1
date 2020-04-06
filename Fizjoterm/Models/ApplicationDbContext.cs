//using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizjoTerm.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConn")
        {
            Database.Initialize(false);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Referral> Referrals { get; set; }
    }
}
