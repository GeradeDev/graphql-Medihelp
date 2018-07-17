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
    }
}
