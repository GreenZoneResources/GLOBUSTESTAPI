using System;
using System.Collections.Generic;
using System.Text;
using GlobusCustomerAPI.Data.Contracts;
using GlobusCustomerAPI.Data.Helpers;
using GlobusCustomerAPI.Data.Models;

namespace GlobusCustomerAPI.Data
{
    public class CustomerUnitOfWork : ICustomerUnitOfWork, IDisposable
    {
        private GlobusDbContext DbContext = new GlobusDbContext();

        public CustomerUnitOfWork(IRepositoryProvider repositoryProvider)
        {
            //CreateDbContext();

            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
        }

        protected IRepositoryProvider RepositoryProvider { get; set; }

        // Code Camper repositories 

        //public ICustomerAccountTypeRepository CustomerAccountTypes => GetEntityRepository<ICustomerAccountTypeRepository>();
        //public ICustomerRepository Customers => GetEntityRepository<ICustomerRepository>();
      
        public ICustomerRepository Customer { get { return GetEntityRepository<ICustomerRepository>(); } }
        

        public void Commit()
        {
            //System.Diagnostics.Debug.WriteLine("Committed");

            DbContext.SaveChanges();
        }


        private IRepository<T> GetStandardRepository<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }
        private T GetEntityRepository<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (DbContext != null)
                //{
                //    DbContext.Dispose();
                //}
            }
        }

        #endregion
    }
}
