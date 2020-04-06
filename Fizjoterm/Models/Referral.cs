using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



        
    }
   
}
