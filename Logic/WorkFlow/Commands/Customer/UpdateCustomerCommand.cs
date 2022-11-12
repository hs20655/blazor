using Data.DTO.Responses;
using Data.Shared;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.WorkFlow.Commands.Customer
{
    public class UpdateCustomerCommand: Command, IRequest<BusinessResult<bool>>
    {
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }

        public override bool IsValid(out List<BrokenRule> brokenRules)
        {
            brokenRules = new List<BrokenRule>();

            if (this.CompanyName.IsNullOrEmpty())
                brokenRules.Add(new BrokenRule()
                {
                    Property = "CompanyName",
                    Rule = "Company Name cannot be empty"
                });
            if (this.CompanyName.IsNullOrEmpty())
                brokenRules.Add(new BrokenRule()
                {
                    Property = "ContactName",
                    Rule = "Contact Name cannot be empty"
                });
            if (this.CompanyName.IsNullOrEmpty())
                brokenRules.Add(new BrokenRule()
                {
                    Property = "Address",
                    Rule = "Address cannot be empty"
                });
            if (this.CompanyName.IsNullOrEmpty())
                brokenRules.Add(new BrokenRule()
                {
                    Property = "City",
                    Rule = "City cannot be empty"
                });
            if (this.CompanyName.IsNullOrEmpty())
                brokenRules.Add(new BrokenRule()
                {
                    Property = "Region",
                    Rule = "Region cannot be empty"
                });
            if (this.CompanyName.IsNullOrEmpty())
                brokenRules.Add(new BrokenRule()
                {
                    Property = "PostalCode",
                    Rule = "PostalCode cannot be empty"
                });
            if (this.CompanyName.IsNullOrEmpty())
                brokenRules.Add(new BrokenRule()
                {
                    Property = "Country",
                    Rule = "Country cannot be empty"
                });
            if (this.CompanyName.IsNullOrEmpty())
                brokenRules.Add(new BrokenRule()
                {
                    Property = "Phone",
                    Rule = "Phone cannot be empty"
                });

            return brokenRules.Count > 0 ? false : true;
        }
    }
}
