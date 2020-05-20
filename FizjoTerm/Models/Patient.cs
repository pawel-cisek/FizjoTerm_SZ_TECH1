using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TabMenu2.Models
{
    [Table("Patient")]
    public class Patient : Person

    {
        public string Pesel { get; set; }

        public Patient() { }

        public Patient(string name, string surname, string adress, string phone, string pesel) : base(name, surname, adress, phone)
        {
            this.Pesel = pesel;
        }

        public static void AddPatient(string name, string surname, string pesel, string adress, string phone, ApplicationDbContext dbcontext)
        {
            Patient pat = new Patient(name, surname, pesel, adress, phone);
            try
            {
                dbcontext.Patients.Add(pat);
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public static void DeletePatient(Patient pat, ApplicationDbContext dbcontext)
        {
            try
            {
                dbcontext.Patients.Remove(pat);
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static IEnumerable<Patient> SearchPatient(string name, string surname, string pesel, string adress, string phone, ApplicationDbContext dbcontext)
        {
            Patient pat = new Patient(name, surname, pesel, adress, phone);
            IEnumerable<Patient> results = dbcontext.Patients.Local.Where(p => p.Name.Contains(pat.Name) && p.Surname.Contains(pat.Surname) && p.Pesel.Contains(pat.Pesel) && p.Phone.Contains(pat.Phone) && p.Adress.Contains(pat.Adress));
            return results;
        }

        public static bool PeselValidation(string pesel)
        {
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            bool result = false;
            if (pesel.Length == 11)
            {
                int controlSum = CalculateControlSum(pesel, weights);
                int controlNum = controlSum % 10;
                controlNum = 10 - controlNum;
                if (controlNum == 10)
                {
                    controlNum = 0;
                }
                int lastDigit = int.Parse(pesel[pesel.Length - 1].ToString());
                result = controlNum == lastDigit;
            }
            return result;
        }

        private static int CalculateControlSum(string input, int[] weights, int offset = 0)
        {
            int controlSum = 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                controlSum += weights[i + offset] * int.Parse(input[i].ToString());
            }
            return controlSum;
        }

        public override string ToString()
        {
            return Surname + " " + Name + " " + Pesel;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Patient p = (Patient)obj;
                return (Name == p.Name) && (Surname == p.Surname) && (Pesel == p.Pesel) && (Adress == p.Adress) && (Phone == p.Phone);
            }
        }
    }
}
