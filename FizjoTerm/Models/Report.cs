using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TabMenu2.Models
{
    /// <summary>
    /// Klasa reprezentująca dane raportu
    /// </summary>
    [Table("Report")]
    public class Report
    {
        [Key]
        public int IdReport { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }

        public Report() { }

        public Report(string name, ICollection<Visit> visits) 
        {
            this.Name = name;
            this.Visits = visits;

        }

        public static void AddReport(string name, ICollection<Visit> visits, ApplicationDbContext dbcontext)
        {
            Report r1 = new Report(name, visits);
            try
            {
                dbcontext.Reports.Add(r1);
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }       
}
