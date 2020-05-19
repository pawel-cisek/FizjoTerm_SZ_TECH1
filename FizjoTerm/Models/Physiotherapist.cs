using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabMenu2.Models
{
    [Table("Physiotherapist")]
    public class Physiotherapist : Person
    {
        public string Email { get; set; }
        public int Npwz { get; set; }

        public Physiotherapist() { }

        public Physiotherapist(string name, string surname, string pesel, string adress, string phone, string email, int npwz) : base(name, surname, adress, phone)
        {
            this.Email = email;
            this.Npwz = npwz;
        }
    }
}
