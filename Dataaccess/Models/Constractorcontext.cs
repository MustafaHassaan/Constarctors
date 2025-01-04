using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Dataaccess.Models
{
    public class Constractorcontext : DbContext
    {
        public static string Con()
        {
            string Connectionstring;
            try
            {
                Connectionstring = @"Data Source=LAPTOP-UKSQ4LM8;Initial Catalog=Constractors2024;Persist Security Info=True;User ID=IE;Password=@Dbadmin;";
            }
            catch (Exception ex)
            {
                Connectionstring = "";
                string Msg = ex.Message;
            }
            return Connectionstring;
        }
        public Constractorcontext() : base(Con())
        {
        }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Opration> Oprations { get; set; }
        public DbSet<TblTransaction> TblTransactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
