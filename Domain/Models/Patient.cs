﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
    }
}
