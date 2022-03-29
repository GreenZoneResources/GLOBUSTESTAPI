using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GlobusCustomerAPI.Data.Contracts
{
    public interface IRepository<T> where T : class
    {

        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
    }
}
