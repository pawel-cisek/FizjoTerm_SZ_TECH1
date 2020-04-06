using FizjoTerm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Text;
using System.Windows;
using FizjoTerm;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace FizjoTerm.Models
{
    [Table("Patient")]
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }

        public static void DeletePatient(Patient pat, ApplicationDbContext dbcontext)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Czy usunąć pacjenta " + pat.Name + " " + pat.Surname + "?", "Usuwanie pacjenta", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dbcontext.Patients.Remove(pat);
                dbcontext.SaveChanges();
            }
        }
        public static IEnumerable<Patient> SearchPatient(Patient pat, ApplicationDbContext dbcontext)
        {
            IEnumerable<Patient> results = dbcontext.Patients.Local.Where(p => p.Name.Contains(pat.Name) && p.Surname.Contains(pat.Surname) && p.Pesel.Contains(pat.Pesel) && p.Phone.Contains(pat.Phone) && p.Adress.Contains(pat.Adress));
            return results;
        }
        public static void AddPatient(Patient pat, ApplicationDbContext dbcontext)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Czy zapisać pacjenta " + pat.Name + " " + pat.Surname + "?", "Zapis", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dbcontext.Patients.Add(pat);
                dbcontext.SaveChanges();
            }
        }
        public override string ToString()
        {
            return Surname +" " + Name + " " + Pesel;
        }
    }
}
