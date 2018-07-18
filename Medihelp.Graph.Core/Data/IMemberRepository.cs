using Medihelp.Graph.Core.Model.Member;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Medihelp.Graph.Core.Data
{
    public interface IMemberRepository
    {
        Task<MedihelpMember> GetMember(int memberNo);
        Task<List<Beneficiary>> GetDepoendantsOnly(int memberNo);
        Task<List<MemberAddress>> GetAllAddresses(int memberNo);
        Task<List<ContactDetail>> GetContacts(int memberNo);
        Task<List<BenefitOption>> GetProductHistory(int memberNo);

        Task<BankingDetail> GetBankingDetails(int memberNo);
    }
}
