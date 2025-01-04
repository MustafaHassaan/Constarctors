using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataaccess.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Projectname { get; set; }
        public decimal Amount { get; set; }
        public decimal Amountvat { get; set; }
        public string Note { get; set; }
        public int Prtid { get; set; }

        [ForeignKey("Prtid")]
        public Partner Partners { get; set; }
    }
}
