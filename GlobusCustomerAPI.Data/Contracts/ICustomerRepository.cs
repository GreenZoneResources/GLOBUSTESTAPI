using System;
using System.Linq;
using GlobusCustomerAPI.Data.Models;

namespace GlobusCustomerAPI.Data.Contracts
{
    public interface ICustomerRepository : IRepository<TblCustomerDetails>
    {
        IQueryable<TblCustomerDetails> ValidateCustomer(int Id);
    }
}
