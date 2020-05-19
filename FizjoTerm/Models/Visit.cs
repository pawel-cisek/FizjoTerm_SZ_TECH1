using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabMenu2.Models
{
    [Table("Visit")]
    public class Visit
    {
        [Key]
        public int IdVisit { get; set; }

        [ForeignKey("Physiotherapist")]
        public int IdPhysiotherapist { get; set; }
        [ForeignKey("Referral")]
        public int IdReferral { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitTime { get; set; }
        public DateTime DateSaved { get; set; }
        public virtual Physiotherapist Physiotherapist { get; set; }
        public virtual Referral Referral { get; set; }
    }
}
