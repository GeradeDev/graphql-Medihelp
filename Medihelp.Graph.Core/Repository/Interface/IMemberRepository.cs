using Medihelp.Graph.Core.Model.Member;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medihelp.Graph.Core.Data.Repository.Interface
{
    public interface IMemberRepository
    {
        Task<MedihelpMember> GetMember(int memberNo);

        Task<List<Beneficiary>> GetDepoendantsOnly(int memberNo, int depNo = 0);

        Task<List<MemberAddress>> GetAllAddresses(int memberNo, string type);

        Task<List<ContactDetail>> GetContacts(int memberNo, string type);

        Task<List<BenefitOption>> GetProductHistory(int memberNo);

        Task<List<BankingDetail>> GetBankingDetails(int memberNo);

        Task<List<Exclusion>> GetMemberExcluisions(int memberNo, int depNo = 0);

        Task<SubscriptionBreakdown> GetSubscriptionBreakdown(int memberNo, DateTime month);

        Task<List<LateJoinerPenalty>> GetMemberPenalties(int memberNo, int depNo = 0);

        Task<List<Benefit>> GetAvailableBenefits(int memberNo, int depNo = 0);

        Task<SavingsReconciliation> GetSavingsReconciliation(int memberNo);
    }
}
