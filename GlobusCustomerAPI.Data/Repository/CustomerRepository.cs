using System.Linq;
using System.Text;
using GlobusCustomerAPI.Data.Contracts;
using GlobusCustomerAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobusCustomerAPI.Data.Repository
{
    public class CustomerRepository : EFRepository<TblCustomerDetails>, ICustomerRepository
    {
        public CustomerRepository(GlobusDbContext context) : base(context) { }

        public IQueryable<TblCustomerDetails> ValidateCustomer(int customerID)
        {
            return DbSet.Where(t => t.Id == customerID);
        }
    }
}


