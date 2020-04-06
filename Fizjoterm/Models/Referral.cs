using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FizjoTerm;
//using Domain.Models;


namespace FizjoTerm.Models
{
    [Table("Referral")]
    public class Referral
    {
        [Key]
        public int ReferralID { get; set; }
        [ForeignKey("Patient")]
        public int PatientID { get; set; }
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

    }
   
}
