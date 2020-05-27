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
    [Table("Visit")]
    public class Visit
    {
        [Key]
        public int IdVisit { get; set; }

        [ForeignKey("Physiotherapist")]
        public int IdPhysiotherapist { get; set; }
        [ForeignKey("Referral")]
        public int IdReferral { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitTime { get; set; }
        public DateTime DateSaved { get; set; }
        public virtual Physiotherapist Physiotherapist { get; set; }
        public virtual Referral Referral { get; set; }

        public Visit() { }
        public Visit(Physiotherapist physio, Referral referral, DateTime date, string time)
        {
            this.Physiotherapist = physio;
            this.Referral = referral;
            this.VisitDate = date;
            this.VisitTime = time;
            this.DateSaved = DateTime.Now;
        }

        public static void AddVisit(Physiotherapist physio, Referral referral, DateTime date, string time, ApplicationDbContext dbcontext)
        {
            Visit vis = new Visit(physio, referral, date, time);
            try
            {
                dbcontext.Visits.Add(vis);
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void DeleteVisit(Visit vis, ApplicationDbContext dbcontext)
        {
            try
            {
                dbcontext.Visits.Remove(vis);
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static IEnumerable<Visit> SearchVisit(Patient patient, Physiotherapist physio, DateTime? selDate, ApplicationDbContext dbcontext)
        {
            IEnumerable<Visit> results = dbcontext.Visits.Local.Where(v => patient!= null ? v.Referral.Patient.Equals(patient) : true 
                                                                        && physio!=null ? v.Physiotherapist.Equals(physio) : true
                                                                        && selDate!= null ? v.VisitDate.Equals(selDate) : true);
            return results;
        }
    }
}
