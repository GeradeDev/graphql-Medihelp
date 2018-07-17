using Medihelp.Graph.Core.Data;
using Medihelp.Graph.Core.Model.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medihelp.Graph.Data.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public WebsiteIntergation.WebsiteIntegrationPortTypeClient _detailRequest;

        public MemberRepository()
        {
            _detailRequest = new WebsiteIntergation.WebsiteIntegrationPortTypeClient(WebsiteIntergation.WebsiteIntegrationPortTypeClient.EndpointConfiguration.WebsiteIntegrationPort);
        }

        public async Task<MedihelpMember> GetMember(int memberNo)
        {
            var details = await _detailRequest.MemberDetailRequestAsync(new WebsiteIntergation.MemberDetailInput() { Scheme = "MH", MemberNumber = memberNo });

            var dependants = (from d in details.MemberDetailResponse.Dependants.Skip(1)
                              select new Beneficiary
                              {
                                  DependantNumber = d.DependantNumber.Value,
                                  FirstName = d.FirstName,
                                  Surname = d.Surname,
                                  BenefitDate = d.DateBenefitStart,
                                  EnrollmentDate = d.DateEnrollStart,
                                  CellphoneNumber = d.DependantCellNumber,
                                  DateOfBirth = d.DateBirth,
                                  DependantStatus = d.Status,
                                  EmailAddress = d.DependantEmailAddress,
                                  Gender = d.Gender,
                                  IdNumber = d.IdNumber,
                                  Initials = d.Init,
                                  PassportNumber = d.PassportNumber,       
                                  Title = d.Title
                              }).ToList();            

            return new MedihelpMember
            {
                MemberNumber = memberNo,
                Title = details.MemberDetailResponse.Dependants[0].Title,
                Init = details.MemberDetailResponse.Dependants[0].Init,
                FirstName = details.MemberDetailResponse.Dependants[0].FirstName,
                Surname = details.MemberDetailResponse.Dependants[0].Surname,
                Gender = details.MemberDetailResponse.Dependants[0].Gender,
                Status = details.MemberDetailResponse.Dependants[0].Status,
                IdNumber = details.MemberDetailResponse.IdNumber,
                Language = details.MemberDetailResponse.EmailAddress,
                EmailAddress = details.MemberDetailResponse.EmailAddress,
                Product = details.MemberDetailResponse.Product,
                ProductDescription = details.MemberDetailResponse.ProductDescription,
                IsNetwork = details.MemberDetailResponse.DspNetwork,
                EmployerGroup = details.MemberDetailResponse.EmplGroup,
                Dependents = dependants
            };
        }
    }
}
