using Dataaccess.Models;
using Repo.UOW.Reoservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.UOW.Unitofworkservices
{
    public class Unitofwork : IUnitofwork
    {
        Constractorcontext _Dbc;
        public IRepository<Partner> Partners { get; set; }
        public IRepository<Project> Projects { get; set; }
        public IRepository<TblTransaction> TblTransactions { get; set; }
        public IRepository<Opration> Oprations { get; set; }
        public IRepository<User> Users { get; set; }
        public Unitofwork()
        {
            _Dbc = new Constractorcontext();
            Partners = new Repository<Partner>(_Dbc);
            Projects = new Repository<Project>(_Dbc);
            TblTransactions = new Repository<TblTransaction>(_Dbc);
            Oprations = new Repository<Opration>(_Dbc);
            Users = new Repository<User>(_Dbc);
        }
        public int Complete()
        {
            return _Dbc.SaveChanges();
        }

        public void Dispose()
        {
            _Dbc.Dispose();
        }
    }
}
