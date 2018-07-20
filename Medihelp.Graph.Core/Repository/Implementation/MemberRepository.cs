using Medihelp.Graph.Core.Data;
using Medihelp.Graph.Core.Model.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medihelp.Graph.Core.Data.Repository.Interface;
using System.Threading;
using System.Globalization;
using Medihelp.Graph.Core.Utils.Extensions;

namespace Medihelp.Graph.Data.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public WebsiteIntergation.WebsiteIntegrationPortTypeClient _detailRequest;
        public BankingDetailsIntegration.MemberBankDetailIntegrationPortTypeClient _BankingRequest;
        public EmployerIntegrationService.EmployerWebsiteIntegrationPortTypeClient _EmployerRequest;

        public MemberRepository()
        {
            _detailRequest = new WebsiteIntergation.WebsiteIntegrationPortTypeClient(WebsiteIntergation.WebsiteIntegrationPortTypeClient.EndpointConfiguration.WebsiteIntegrationPort);
            _BankingRequest = new BankingDetailsIntegration.MemberBankDetailIntegrationPortTypeClient(BankingDetailsIntegration.MemberBankDetailIntegrationPortTypeClient.EndpointConfiguration.MemberBankDetailIntegrationPort12);
            _EmployerRequest = new EmployerIntegrationService.EmployerWebsiteIntegrationPortTypeClient(EmployerIntegrationService.EmployerWebsiteIntegrationPortTypeClient.EndpointConfiguration.EmployerWebsiteIntegrationPort12);
        }



        
        public async Task<MedihelpMember> GetMember(int memberNo)
        {
            var details = await _detailRequest.MemberDetailRequestAsync(new WebsiteIntergation.MemberDetailInput() { Scheme = "MH", MemberNumber = memberNo });          

            var deps = (from d in details.MemberDetailResponse.Dependants.Skip(1)
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
                EmployerGroupDescription = details.MemberDetailResponse.EmplGroupDescription,
                MaritalStatus = details.MemberDetailResponse.MaritalStatus,
                MaritalStatusDescription = details.MemberDetailResponse.MaritalStatusDescription,
                DateMarital = details.MemberDetailResponse.DateMarital,
                VisuallyImpaired = details.MemberDetailResponse.VisuallyImpaired,
                MonthlyContribution = details.MemberDetailResponse.MonthlyContribution,
                HoldPost = details.MemberDetailResponse.HoldPost,
                ArrearNotification = details.MemberDetailResponse.ArrearNotification,
                TerminationNotification = details.MemberDetailResponse.TerminationNotification,
                Dependents = deps
            };
        }

        public async Task<List<Beneficiary>> GetDepoendantsOnly(int memberNo, int depNo = 0)
        {
            var details = await _detailRequest.MemberDetailRequestAsync(new WebsiteIntergation.MemberDetailInput() { Scheme = "MH", MemberNumber = memberNo });

            return (from d in details.MemberDetailResponse.Dependants.Skip(1)
                    where (depNo != 0 ? d.DependantNumber == depNo : true)
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

        public async Task<List<MemberAddress>> GetAllAddresses(int memberNo, string addressType)
        {
            var details = await _detailRequest.MemberDetailRequestAsync(new WebsiteIntergation.MemberDetailInput() { Scheme = "MH", MemberNumber = memberNo });

            return (from d in details.MemberDetailResponse.Addresses
                    where (addressType != "all" ? d.Type.ToLower() == addressType : true)
                    select new MemberAddress
                    {
                        Type = d.Type,
                        AddressLine1 = d.Line1,
                        AddressLine2 = d.Line2,
                        AddressLine3 = d.Line3,
                        AddressLine4 = d.Line4
                    }).ToList();
        }

        public async Task<List<ContactDetail>> GetContacts(int memberNo, string type)
        {
            var details = await _detailRequest.MemberDetailRequestAsync(new WebsiteIntergation.MemberDetailInput() { Scheme = "MH", MemberNumber = memberNo });

            return (from d in details.MemberDetailResponse.PhoneNumbers
                    where (type != "all" ? d.Type.ToLower().Contains(type) : true)
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

        public async Task<List<Exclusion>> GetMemberExcluisions(int memberNo, int depNo)
        {
            var exclusions = await _detailRequest.ExclusionDetailRequestAsync(new WebsiteIntergation.ExclusionDetailInput { Scheme = "MH", MemberNumber = memberNo });

            return (from exc in exclusions.ExclusionDetailResponse.Exclusions
                    where (depNo > 0 ? exc.DependantNumber == depNo : true)
                    select new Exclusion
                    {
                        Diagnosis = exc.DiagnosisCodeDescription,
                        DiagnosisCode = exc.DiagnosisCode.ToString(),
                        StartDate = exc.ExclusionStartDate,
                        EndDate = exc.ExclusionEndDate,
                        Type = exc.ExclusionType,
                        PMBCat = exc.PmbCategory,
                        DependantNumber = exc.DependantNumber.ToString()
                    }).ToList();
        }
        
        public async Task<SubscriptionBreakdown> GetSubscriptionBreakdown(int memberNo, DateTime month){

            var breakdown = await _EmployerRequest.SubscriptionBreakdownRequestAsync(new EmployerIntegrationService.SubscriptionBreakdownInput { Scheme = "MH",  MemberNumber = memberNo, DateEffective = month });

            SubscriptionBreakdown sub = new SubscriptionBreakdown()
            {
                Dependants = (from d in breakdown.SubscriptionBreakdownResponse.Dependants
                              select new Dependant
                              {
                                  DescriptionofRelations = d.DependantType,
                                  SavingsContribution = (!string.IsNullOrEmpty(d.SavingsAmount.ToString()) ? d.SavingsAmount.Value : (decimal)0),
                                  LJPAmount = (!string.IsNullOrEmpty(d.LateJoinerPenalty.ToString()) ? d.LateJoinerPenalty.Value : (decimal)0),
                                  Subscription = d.CoreSubscription,
                                  MonthlySubscription = d.TotMonthlySubscription
                              }).ToList(),

                MaxSubsCalculate = (!string.IsNullOrEmpty(breakdown.SubscriptionBreakdownResponse.MaxSubsidy.ToString()) ? breakdown.SubscriptionBreakdownResponse.MaxSubsidy.Value : (decimal)0),
                MemberPortion = breakdown.SubscriptionBreakdownResponse.MemberPortion,
                Subsidy = (!string.IsNullOrEmpty(breakdown.SubscriptionBreakdownResponse.Subsidy.ToString()) ? breakdown.SubscriptionBreakdownResponse.Subsidy.Value : (decimal)0),
                SubsidyFactor = breakdown.SubscriptionBreakdownResponse.SubsidyFactor,
                TotalSubscription = breakdown.SubscriptionBreakdownResponse.TotalSubscription,
            };

            return sub;
        }
        
        public async Task<List<LateJoinerPenalty>> GetMemberPenalties(int memberNo, int depNo = 0)
        {
            var details = await _detailRequest.MemberLateJoinerPenaltyRequestAsync(new WebsiteIntergation.MemberLateJoinerPenaltyInput() { Scheme = "MH", MemberNumber = memberNo });

            return (from ljp in details.MemberLateJoinerPenaltyResponse.LateJoinerPenalties
                    where (depNo > 0 ? ljp.DependantNumber == depNo : true)
                    select new LateJoinerPenalty
                    {
                        LjpPercentage = ljp.AddonPercentage.ToString(),
                        DepNumber = ljp.DependantNumber.ToString(),
                        EffectiveDate = ljp.DateEffective,
                        EndDate = (ljp.DateEnd ?? Convert.ToDateTime("0001/01/01")),
                        ReasonCode = ljp.ReasonCode,
                        LJPAmount = -1
                    }).ToList();
        }

        public async Task<List<Benefit>> GetAvailableBenefits(int memberNo, int depNo = 0)
        {
            var benefits = await _detailRequest.MemberLimitRequestAsync(new WebsiteIntergation.MemberLimitInput() { Scheme = "MH", MemberNumber = memberNo });

            return (from b in benefits.MemberLimitResponse.Limits
                    select new Benefit
                    {
                        AgeGen = b.AgeGen,
                        AmtReserved = b.AmtReserved,
                        Available = b.Available,
                        ConsultationCount = b.ConsultationCount,
                        DateEffective = b.DateEffective,
                        DateExpire = b.DateExpire,
                        DependantName = "",
                        Description = b.Description,
                        FamilyIndividual = b.FamilyIndividual,
                        LimitCode = b.LimitCode,
                        LimitType = b.LimitType,
                        LimitValue = b.LimitValue,
                        PmbUsed = b.PmbUsed,
                        Used = b.Used
                    }).ToList();
        }
        
        public async Task<SavingsReconciliation> GetSavingsReconciliation(int memberNo)
        {
            var Savingsrecon = await _detailRequest.MemberSavingsReconRequestAsync(new WebsiteIntergation.MemberSavingsReconInput() { Scheme = "MH", MemberNumber = memberNo });

            SavingsReconciliation savings = new SavingsReconciliation();

            savings.OpeningBalance = Savingsrecon.MemberSavingsReconResponse.OpeningBalance;
            savings.SavingsAvailable = Savingsrecon.MemberSavingsReconResponse.SavingsAvailable;


            savings.Lines.AddRange((from recon in Savingsrecon.MemberSavingsReconResponse.SavingsReconLines
                                    select new SavingsReconciliationLine
                                    {
                                        Month = recon.Month,
                                        Description = recon.Description,
                                        DateService = recon.DateService.HasValue ? recon.DateService.Value.ToString(StringFormatExtension.DATE_FORMAT) : string.Empty,
                                        Patient = recon.Patient,
                                        DateStatement = recon.DateStatement.ToString(StringFormatExtension.DATE_FORMAT),
                                        AmountDebit = StringFormatExtension.FormatRands(Math.Abs(recon.AmountDebit).ToString().Replace(",", ".")),
                                        AmountCredit = StringFormatExtension.FormatRands(Math.Abs(recon.AmountCredit).ToString().Replace(",", ".")),
                                        AmountCumulative = StringFormatExtension.FormatRands(Math.Abs(recon.AmountCumulative).ToString().Replace(",", ".")),
                                    }).ToList());

            return savings;
        }
    }
}
