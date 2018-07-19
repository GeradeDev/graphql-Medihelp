using Medihelp.Graph.Core.Model.Member;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medihelp.Graph.Core.Data.Repository.Interface
{
    public interface IMemberRepository
    {
        Task<MedihelpMember> GetMember(int memberNo);

        Task<List<Beneficiary>> GetDepoendantsOnly(int memberNo);

        Task<List<MemberAddress>> GetAllAddresses(int memberNo);

        Task<List<ContactDetail>> GetContacts(int memberNo);

        Task<List<BenefitOption>> GetProductHistory(int memberNo);

        Task<List<BankingDetail>> GetBankingDetails(int memberNo);

        Task<List<Exclusion>> GetMemberExcluisions(int memberNo, int depNo);

        Task<SubscriptionBreakdown> GetSubscriptionBreakdown(int memberNo, DateTime month);

        Task<List<LateJoinerPenalty>> GetMemberPenalties(int memberNo);
    }
}
