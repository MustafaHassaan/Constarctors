using Dataaccess.Models;
using Repo.UOW.Reoservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.UOW.Unitofworkservices
{
    public interface IUnitofwork : IDisposable
    {
        IRepository<Partner> Partners { get; set; }
        IRepository<Project> Projects { get; set; }
        IRepository<TblTransaction> TblTransactions { get; set; }
        IRepository<Opration> Oprations { get; set; }
        IRepository<User> Users { get; set; }
        int Complete();
    }
}
