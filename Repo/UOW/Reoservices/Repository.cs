using Dataaccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repo.UOW.Reoservices
{
    public class Repository<T> : IRepository<T> where T : class
    {
        Constractorcontext _Dbc;
        public Repository(Constractorcontext Dbc)
        {
            _Dbc = Dbc;
        }
        public IEnumerable<T> GetAll()
        {
            return _Dbc.Set<T>().ToList();
        }
        public T Get(int id)
        {
            return _Dbc.Set<T>().Find(id);
        }
        public void Add(T entity)
        {
            _Dbc.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _Dbc.Set<T>().AddOrUpdate(entity);
        }
        public void Delbyid(int id)
        {
            var Entdel = _Dbc.Set<T>().Find(id);
            _Dbc.Entry(Entdel).State = EntityState.Deleted;
        }
        public IEnumerable<T> Search(Expression<Func<T, bool>> entity)
        {
            return _Dbc.Set<T>().Where(entity).ToList();
        }
    }
}