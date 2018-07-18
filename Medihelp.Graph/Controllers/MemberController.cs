using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medihelp.Graph.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medihelp.Graph.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepo;

        public MemberController(IMemberRepository memberRepo)
        {
            _memberRepo = memberRepo;
        }

        /// <summary>
        /// Member Endpoint. Get principle member details
        /// </summary>
        /// <param name="membershipNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> Get(int membershipNo)
        {
            var memberDetail = await _memberRepo.GetMember(membershipNo).ConfigureAwait(false);

            return Ok(memberDetail);
        }


        [Route("Dependants")]
        public async Task<IActionResult> GetDependants(int membershipNo)
        {
            var memberDependants = await _memberRepo.GetDepoendantsOnly(membershipNo).ConfigureAwait(false);

            return Ok(memberDependants);
        }

        [Route("Addresses")]
        public async Task<IActionResult> MemberAddresses(int membershipNo)
        {
            var memberDependants = await _memberRepo.GetAllAddresses(membershipNo).ConfigureAwait(false);

            return Ok(memberDependants);
        }

        [Route("Contacts")]
        public async Task<IActionResult> MemberContacts(int membershipNo)
        {
            var memberDependants = await _memberRepo.GetContacts(membershipNo).ConfigureAwait(false);

            return Ok(memberDependants);
        }

        [Route("ProductHistory")]
        public async Task<IActionResult> MemberBenefitOptionHistory(int membershipNo)
        {
            var memberDependants = await _memberRepo.GetProductHistory(membershipNo).ConfigureAwait(false);

            return Ok(memberDependants);
        }



    }
}