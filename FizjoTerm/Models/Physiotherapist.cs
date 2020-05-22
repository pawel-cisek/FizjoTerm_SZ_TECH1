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
    [Table("Physiotherapist")]
    public class Physiotherapist : Person
    {
        public string Email { get; set; }
        public int Npwz { get; set; }

        public Physiotherapist() { }

        public Physiotherapist(string name, string surname, string adress, string phone, string email, int npwz) : base(name, surname, adress, phone)
        {
            this.Email = email;
            this.Npwz = npwz;
        }

        public static void AddPhysiotherapist(string name, string surname, string adress, string phone, string email, int npwz, ApplicationDbContext dbcontext)
        {
            Physiotherapist physio = new Physiotherapist(name, surname, adress, phone, email, npwz);
            try
            {
                dbcontext.Physiotherapists.Add(physio);
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public static void DeletePhysiotherapist(Physiotherapist physio, ApplicationDbContext dbcontext)
        {
            try
            {
                dbcontext.Physiotherapists.Remove(physio);
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static IEnumerable<Physiotherapist> SearchPhysiotherapist(string name, string surname, string adress, string phone, string email, string npwz, ApplicationDbContext dbcontext)
        {
            //Patient pat = new Patient(name, surname, pesel, adress, phone);
            IEnumerable<Physiotherapist> results = dbcontext.Physiotherapists.Local.Where(p => p.Name.Contains(name) && p.Surname.Contains(surname) && p.Npwz.ToString().Contains(npwz) && p.Phone.Contains(phone) && p.Adress.Contains(adress));
            return results;
        }
        public static bool NwpzValidation(string nwpz)
        {
            if (Int32.TryParse(nwpz, out var outParse))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

    }
}
