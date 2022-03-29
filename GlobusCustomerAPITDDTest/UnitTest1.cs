using GlobusCustomerAPI.Controllers;
using GlobusCustomerAPI.Data.Contracts;
using GlobusCustomerAPI.Data.Models;
using GlobusCustomerAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;

namespace GlobusCustomerAPITDDTest
{
    [TestClass]
    public class UnitTest1
    {
       
        [Fact]
        public void TestGetCustomers()
        {
            ICustomerUnitOfWork _customerUnitOfWork = null;
        var customerUnitWork = new Mock<ICustomerUnitOfWork>();
        CustomerController customerTest = new CustomerController(_customerUnitOfWork);
            //var getAllCustomers = customerTest.GetAllCustomers().Count;
            //Assert.AreEqual(getAllCustomers, customerTest.GetAllCustomers());
            customerTest.GetAllCustomers();
            customerUnitWork.Verify(x => x.Customer.GetAll());
        }

        [Fact]
        public void TestAddCustomers()
        {
            ICustomerUnitOfWork _customerUnitOfWork = null;
            CustomerInformation _customerInfo = new CustomerInformation();
            TblCustomerDetails customer = new TblCustomerDetails();
            var customerUnitWorkMock = new Mock<ICustomerUnitOfWork>();
            customerUnitWorkMock.Setup(x => x.Customer.Add(It.Is<TblCustomerDetails>(y => y == customer)));
            CustomerController customerTest = new CustomerController(_customerUnitOfWork);
            
            customerTest.PostCustomerDetails(_customerInfo);
            customerUnitWorkMock.Verify(x => x.Customer.Add(It.Is<TblCustomerDetails>(y => y == customer)));
        }

    }
}
