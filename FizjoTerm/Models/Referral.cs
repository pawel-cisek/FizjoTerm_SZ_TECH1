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
    [Table("Referral")]
    public class Referral
    {
        [Key]
        public int IdReferral { get; set; }
        [ForeignKey("Patient")]
        public int IdPatient { get; set; }
        public string Diagnosis { get; set; }
        public string Icd10 { get; set; }
        public int NbOfDays { get; set; }
        public DateTime DateReferral { get; set; }
        public DateTime DateSaved { get; set; }
        public virtual Patient Patient { get; set; }

        public Referral() { }

        public Referral(string diagnosis, string icd10, int nbofdays, DateTime datereferral, Patient patient)
        {
            this.Diagnosis = diagnosis;
            this.Icd10 = icd10;
            this.NbOfDays = nbofdays;
            this.DateReferral = datereferral;
            this.DateSaved = DateTime.Now;
            this.Patient = patient;
        }

        public static void AddReferral(string diagnosis, string icd10, int nbofdays, DateTime datereferral, Patient patient, ApplicationDbContext dbcontext)
        {
            Referral r1 = new Referral(diagnosis, icd10, nbofdays, datereferral, patient);
            try
            {
                dbcontext.Referrals.Add(r1);
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void DeleteReferral(Referral referral, ApplicationDbContext dbcontext)
        {
            try
            {
                dbcontext.Referrals.Remove(referral);
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static IEnumerable<Referral> SearchReferral(string diagnosis, string icd10, int nbofdays, DateTime datereferral, Patient patient, ApplicationDbContext dbcontext)
        {
            //Referral r1 = new Referral(diagnosis, icd10, nbofdays, datereferral, patient);
            IEnumerable<Referral> results = dbcontext.Referrals.Local.Where(r => patient!=null ? r.Patient.Equals(patient) : true && r.Diagnosis.Contains(diagnosis) && r.Icd10.Contains(icd10));
            return results;
        }

        public override string ToString()
        {
            return Patient.Name + " " + Patient.Surname + " " + Diagnosis + " " + DateReferral.Date.ToShortDateString();
        }
    }
}
