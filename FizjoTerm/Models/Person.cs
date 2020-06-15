using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabMenu2.Models
{
    /// <summary>
    /// Klasa bazowa dla Patient i Physiotherapist
    /// </summary>
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }

        public Person() { }
        public Person(string name, string surname, string adress, string phone)
        {
            this.Name = name;
            this.Surname = surname;
            this.Adress = adress;
            this.Phone = phone;
        }

       
    }
}
