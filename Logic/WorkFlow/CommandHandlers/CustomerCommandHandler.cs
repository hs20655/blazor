using Data.DTO.Responses;
using Data.Entities;
using Data.Shared;
using Logic.Core.UnitOfWork.Configuration;
using Logic.Core.UnitOfWork.IConfiguration;
using Logic.WorkFlow.Commands.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic.WorkFlow.CommandHandlers
{
    public sealed class CustomerCommandHandler : CommandHandler,
        IRequestHandler<AddCustomerCommand, BusinessResult<string>>,
        IRequestHandler<UpdateCustomerCommand, BusinessResult<bool>>,
        IRequestHandler<DeleteCustomerCommand, BusinessResult<bool>>
    {
        private IUnitOfWork _unitOfWork;

        public CustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BusinessResult<string>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            //_unitOfWork.DisposeDbConnection(); for test
            bool isCommandValid = request.IsValid(out List<BrokenRule> brokenRules);
            if (!isCommandValid)
                return new BusinessResult<string>(string.Empty, brokenRules);

            var result = new BusinessResult<string>();

            var customer = new Customer()
            {
                CompanyName = request.CompanyName,
                ContactName = request.ContactName,
                Address = request.Address,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
                Country = request.Country,
                Phone = request.Phone
            };
            
            bool created = await _unitOfWork.Customers.Add(customer, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            if (created) result.OperationResult = customer.Id.ToString();


            return result;
        }

        public async Task<BusinessResult<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            bool isCommandValid = request.IsValid(out List<BrokenRule> brokenRules);
            if (!isCommandValid)
                return new BusinessResult<bool>(false, brokenRules);

            var result = new BusinessResult<bool>();

            var requestedCustomerExist = await _unitOfWork.Customers.GetById(request.Id);
            if (requestedCustomerExist == null)
            {
                result.BrokenRules.Add(new BrokenRule() { Property = "Id", Rule = "Customer with current id does not exist!" });
                result.OperationResult = false;
                return result;
            }

            requestedCustomerExist.CompanyName = request.CompanyName;
            requestedCustomerExist.ContactName = request.ContactName;
            requestedCustomerExist.Address = request.Address;
            requestedCustomerExist.City = request.City;
            requestedCustomerExist.Region = request.Region;
            requestedCustomerExist.PostalCode = request.PostalCode;
            requestedCustomerExist.Country = request.Country;
            requestedCustomerExist.Phone = request.Phone;

            result.OperationResult =  _unitOfWork.Customers.Update(requestedCustomerExist);

            if (!result.OperationResult)
            {
                result.BrokenRules.Add(new BrokenRule() { Property = "", Rule = "DataBase error!" });
                return result;
            }

            await _unitOfWork.CompleteAsync(cancellationToken);

            return result;
        }

        public async Task<BusinessResult<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            bool isCommandValid = request.IsValid(out List<BrokenRule> brokenRules);
            if (!isCommandValid)
                return new BusinessResult<bool>(false, brokenRules);

            var result = new BusinessResult<bool>();

            result.OperationResult = await _unitOfWork.Customers.Delete(request.Id);

            if (!result.OperationResult)
            {
                result.BrokenRules.Add(new BrokenRule() { Property = "Id", Rule = "Customer with current id does not exist!" });
                return result;
            }

            await _unitOfWork.CompleteAsync(cancellationToken);
            return result;
        }
    }
}
