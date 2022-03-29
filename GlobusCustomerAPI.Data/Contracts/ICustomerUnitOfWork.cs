using System;
using System.Collections.Generic;
using System.Text;

namespace GlobusCustomerAPI.Data.Contracts
{
    public interface ICustomerUnitOfWork
    {
        // Save pending changes to the data store.
        void Commit();


        //Am adding my Repositories from here
        ICustomerRepository Customer { get; }
        
    }
}



