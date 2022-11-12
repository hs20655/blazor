using Data;
using Data.DTO.Responses;
using Logic.Core.UnitOfWork.Configuration;
using Logic.WorkFlow.CommandHandlers;
using Logic.WorkFlow.Commands.Customer;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AppLogic.Tests
{
    [TestClass]
    public class CustomerTest
    {
        private static UnitOfWork unitOfWork;
        private static CustomerCommandHandler customersCommandsHandler;

        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            // method will run once at the beginign of all test methods
            unitOfWork = new UnitOfWork(new ModelContext(), new LoggerFactory());
            customersCommandsHandler = new CustomerCommandHandler(unitOfWork);

        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            // method will run once at the end of all test methods
            unitOfWork.Dispose();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            //method will be called before each test method
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //method will be called after each test method
        }

        [TestMethod]
        public async Task AddCustomer()
        {
            //Arange  
            var customer = NewCustomer();

            //Act
            var result = await customersCommandsHandler.Handle(customer, new System.Threading.CancellationToken());

            //Assert
            Assert.AreEqual(0, result.BrokenRules.Count,
                result.BrokenRules.Count == 0 ? string.Empty : result.BrokenRules[0].Property + '_' + result.BrokenRules[0].Rule);
            Assert.AreNotEqual(string.Empty, result.OperationResult);

        }

        [TestMethod]
        public async Task UpdateCustomer()
        {
            //CREATE CUSTOMER
            //Arange  
            var customer = NewCustomer();

            //Act
            var result = await customersCommandsHandler.Handle(customer, new System.Threading.CancellationToken());

            //Assert
            Assert.AreEqual(0, result.BrokenRules.Count,
                result.BrokenRules.Count == 0 ? string.Empty : result.BrokenRules[0].Property + '_' + result.BrokenRules[0].Rule);
            Assert.AreNotEqual(string.Empty, result.OperationResult);

            //UPDATE CUSTOMER

            var customerToUpdate = new UpdateCustomerCommand()
            {
                Id = Guid.Parse(result.OperationResult ?? string.Empty),
                CompanyName = "CompanyName UPDATED",
                ContactName = "ContactName UPDATED",
                Address = "Address UPDATED",
                City = "City UPDATED",
                Region = "Region UPDATED",
                PostalCode = "PostalCode UPDATED",
                Country = "Country UPDATED",
                Phone = "Phone UPDATED"

            };
            var resultOfUpdate = await customersCommandsHandler.Handle(customerToUpdate, new System.Threading.CancellationToken());

            //Assert
            Assert.AreEqual(0, resultOfUpdate.BrokenRules.Count,
                resultOfUpdate.BrokenRules.Count == 0 ? string.Empty : resultOfUpdate.BrokenRules[0].Property + '_' + resultOfUpdate.BrokenRules[0].Rule);
            Assert.AreEqual(true, resultOfUpdate.OperationResult);


        }

        [TestMethod]
        public async Task DeleteCustomer()
        {
            //CREATE CUSTOMER
            //Arange  
            var customer = NewCustomer();

            //Act
            var result = await customersCommandsHandler.Handle(customer, new System.Threading.CancellationToken());

            //Assert
            Assert.AreEqual(0, result.BrokenRules.Count,
                result.BrokenRules.Count == 0 ? string.Empty : result.BrokenRules[0].Property + '_' + result.BrokenRules[0].Rule);
            Assert.AreNotEqual(string.Empty, result.OperationResult);


            // DELETE CUSTOMER
            //Arange
            var deleteClient = new DeleteCustomerCommand() { Id = Guid.Parse(result.OperationResult ?? string.Empty)};

            //Act
            var resultOfDelete = await customersCommandsHandler.Handle(deleteClient, new System.Threading.CancellationToken());

            //Assert
            Assert.AreEqual(0, resultOfDelete.BrokenRules.Count,
                resultOfDelete.BrokenRules.Count == 0 ? string.Empty : resultOfDelete.BrokenRules[0].Property + '_' + resultOfDelete.BrokenRules[0].Rule);
            Assert.AreEqual(true, resultOfDelete.OperationResult);

        }

        //HELPERS
        private static AddCustomerCommand NewCustomer()
        {
            var client = new AddCustomerCommand()
            {
                CompanyName = "CompanyName",
                ContactName = "ContactName",
                Address = "Address",
                City = "City",
                Region = "Region",
                PostalCode = "PostalCode",
                Country = "Country",
                Phone = "Phone"

            };
            return client;
        }

    }
}