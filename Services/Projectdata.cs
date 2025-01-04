using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Projectdata
    {
        public int ID { get; set; }
        public string Projectname { get; set; }
        public decimal Amount { get; set; }
        public decimal Amountvat { get; set; }
        public string Partnername { get; set; }
        public string Date { get; set; }
        public decimal Creditor { get; set; }
        public decimal Debitor { get; set; }
        public string Note { get; set; }
    }
}
