using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataaccess.Models
{
    public class Opration
    {
        public int Id { get; set; }
        public string Oprationname { get; set; }
        public string Detailes { get; set; }
        public string Tblname { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int? Tblid { get; set; }
        public int Usrid { get; set; }

        [ForeignKey("Usrid")]
        public User Users { get; set; }
    }
}
