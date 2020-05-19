using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabMenu2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefConn")
        {
            Database.Initialize(false);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<Physiotherapist> Physiotherapists { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
