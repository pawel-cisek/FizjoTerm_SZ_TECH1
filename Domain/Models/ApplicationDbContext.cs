using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Domain.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            Database.Initialize(false);
        }

        public DbSet<Physioterapist> Physio { get; set; }
    }
}
