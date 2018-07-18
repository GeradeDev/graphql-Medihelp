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
        public BankingDetailsIntegration.MemberBankDetailIntegrationPortTypeClient _BankingRequest;

        public MemberRepository()
        {
            _detailRequest = new WebsiteIntergation.WebsiteIntegrationPortTypeClient(WebsiteIntergation.WebsiteIntegrationPortTypeClient.EndpointConfiguration.WebsiteIntegrationPort);
            _BankingRequest = new BankingDetailsIntegration.MemberBankDetailIntegrationPortTypeClient(BankingDetailsIntegration.MemberBankDetailIntegrationPortTypeClient.EndpointConfiguration.MemberBankDetailIntegrationPort12);
        }

        public async Task<MedihelpMember> GetMember(int memberNo)
        {
            var details = await _detailRequest.MemberDetailRequestAsync(new WebsiteIntergation.MemberDetailInput() { Scheme = "MH", MemberNumber = memberNo });          

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
                EmployerGroupDescription = details.MemberDetailResponse.EmplGroupDescription,
                MaritalStatus = details.MemberDetailResponse.MaritalStatus,
                MaritalStatusDescription = details.MemberDetailResponse.MaritalStatusDescription,
                DateMarital = details.MemberDetailResponse.DateMarital,
                VisuallyImpaired = details.MemberDetailResponse.VisuallyImpaired,
                MonthlyContribution = details.MemberDetailResponse.MonthlyContribution,
                HoldPost = details.MemberDetailResponse.HoldPost,
                ArrearNotification = details.MemberDetailResponse.ArrearNotification,
                TerminationNotification = details.MemberDetailResponse.TerminationNotification
            };
        }

        public async Task<List<Beneficiary>> GetDepoendantsOnly(int memberNo)
        {
            var details = await _detailRequest.MemberDetailRequestAsync(new WebsiteIntergation.MemberDetailInput() { Scheme = "MH", MemberNumber = memberNo });

            return (from d in details.MemberDetailResponse.Dependants.Skip(1)
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
        }

        public async Task<List<MemberAddress>> GetAllAddresses(int memberNo)
        {
            var details = await _detailRequest.MemberDetailRequestAsync(new WebsiteIntergation.MemberDetailInput() { Scheme = "MH", MemberNumber = memberNo });

            return (from d in details.MemberDetailResponse.Addresses
                    select new MemberAddress
                    {
                        Type = d.Type,
                        AddressLine1 = d.Line1,
                        AddressLine2 = d.Line2,
                        AddressLine3 = d.Line3,
                        AddressLine4 = d.Line4
                    }).ToList();
        }

        public async Task<List<ContactDetail>> GetContacts(int memberNo)
        {
            var details = await _detailRequest.MemberDetailRequestAsync(new WebsiteIntergation.MemberDetailInput() { Scheme = "MH", MemberNumber = memberNo });

            return (from d in details.MemberDetailResponse.PhoneNumbers
                    select new ContactDetail
                    {
                        Type = d.Type,
                        DialCode = d.DialCode,
                        Number = d.Number,
                        Extension = d.Extension
                    }).ToList();
        }

        public async Task<List<BenefitOption>> GetProductHistory(int memberNo)
        {
            var details = await _detailRequest.MemberDetailRequestAsync(new WebsiteIntergation.MemberDetailInput() { Scheme = "MH", MemberNumber = memberNo });

            return (from d in details.MemberDetailResponse.Products
                    select new BenefitOption
                    {
                        Code = d.ProductCode,
                        Description = d.ProductDescription,
                        IsNetworkOption = d.DspNetwork,
                        DateEffective = (!String.IsNullOrEmpty(d.DateEffective) ? DateTime.Parse(d.DateEffective) : (DateTime?)null),
                        DateEnded = (!String.IsNullOrEmpty(d.DateEnd) ? DateTime.Parse(d.DateEnd) : (DateTime?)null)
                    }).ToList();
        }


        public async Task<List<BankingDetail>> GetBankingDetails(int memberNo)
        {
            var bankingDetails = await _BankingRequest.MemberBankDetailRequestAsync(new BankingDetailsIntegration.MemberBankDetailInput { MemberNumber = memberNo, Scheme = "MH" });

            return (from d in bankingDetails.MemberBankDetailResponse.BankDetails
                    select new BankingDetail
                    {
                        AccountDescription = d.Description,
                        AccDepWithdraw = d.AccDepWithdraw,
                        AccountHolder = d.AccountHolder,
                        AccountNumber = d.AccountNumber,
                        AccountType = d.AccountType,
                        BankName = d.BankName,
                        BankTransferEffective = d.BankXferEff,
                        BranchCode = d.BranchCode,
                        EffectiveDate = d.EffDate,
                        EmployerGroup = d.EmplGroup,
                        EmployerGroupDescription = d.EmplDescription,
                        EndDate = d.EndDate,
                        RateChargeIndicator = d.RateChargeInd
                    }).ToList();
        }
    }
}
