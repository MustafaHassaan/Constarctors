using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Transactiondata
    {
        public int ID { get; set; }
        public decimal Creditor { get; set; }
        public decimal TotCreditor { get; set; }
        public decimal Debitor { get; set; }
        public decimal TotDebitor { get; set; }
        public DateTime TDate { get; set; }
        public string Detailes { get; set; }
        public decimal Vatamount { get; set; }
        public decimal TotVat { get; set; }
        public string Projectname { get; set; }
        public string Partnername { get; set; }
        public decimal Balance { get; set; }
        public decimal TotBalance { get; set; }
        public double Remining { get; set; }
        public double TotRemining { get; set; }
    }
}
