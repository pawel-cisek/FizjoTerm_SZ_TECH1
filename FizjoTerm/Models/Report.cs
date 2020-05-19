using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabMenu2.Models
{
    [Table("Report")]
    public class Report
    {
        [Key]
        public int IdReport { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
