using Data.DTO.Responses;
using Data.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.WorkFlow.Commands.Customer
{
    public class DeleteCustomerCommand: Command, IRequest<BusinessResult<bool>>
    {
        public override bool IsValid(out List<BrokenRule> brokenRules)
        {
            brokenRules = new List<BrokenRule>();

            return brokenRules.Count > 0 ? false : true;
        }
    }
}
