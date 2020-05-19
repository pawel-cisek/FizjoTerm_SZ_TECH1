using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
