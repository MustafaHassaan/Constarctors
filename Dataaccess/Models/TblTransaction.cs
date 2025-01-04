using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataaccess.Models
{
    public class TblTransaction
    {
        public int ID { get; set; }
        public decimal Creditor { get; set; }
        public decimal Debitor { get; set; }
        public DateTime TDate { get; set; }
        public string Detailes { get; set; }
        public decimal Vatamount { get; set; }
        public int Proid { get; set; }

        [ForeignKey("Proid")]
        public Project Projects { get; set; }
    }
}
