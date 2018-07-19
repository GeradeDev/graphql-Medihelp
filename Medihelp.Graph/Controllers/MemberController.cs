using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Medihelp.Graph.Core.Data;
using Medihelp.Graph.Core.Data.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet]
        /// <summary>
        /// Member Endpoint. Get principle member details
        /// </summary>
        /// <param name="membershipNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> Get([FromQuery, SwaggerParameter("Medihelp Membership number", Required = true)]int membershipNo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var memberDetail = await _memberRepo.GetMember(membershipNo).ConfigureAwait(false);

            return Ok(memberDetail);
        }


        [HttpGet]
        [Route("Dependants")]
        public async Task<IActionResult> GetDependants([FromQuery, SwaggerParameter("Medihelp Membership number", Required = true)]int membershipNo)
        {
            var memberDependants = await _memberRepo.GetDepoendantsOnly(membershipNo).ConfigureAwait(false);

            return Ok(memberDependants);
        }

        [HttpGet]
        [Route("Addresses")]
        public async Task<IActionResult> MemberAddresses([FromQuery, SwaggerParameter("Medihelp Membership number", Required = true)]int membershipNo)
        {
            var memberDependants = await _memberRepo.GetAllAddresses(membershipNo).ConfigureAwait(false);

            return Ok(memberDependants);
        }

        [HttpGet]
        [Route("Contacts")]
        public async Task<IActionResult> MemberContacts([FromQuery, SwaggerParameter("Medihelp Membership number", Required = true)]int membershipNo)
        {
            var memberDependants = await _memberRepo.GetContacts(membershipNo).ConfigureAwait(false);

            return Ok(memberDependants);
        }

        [HttpGet]
        [Route("ProductHistory")]
        public async Task<IActionResult> MemberBenefitOptionHistory([Required]int membershipNo)
        {
            var memberDependants = await _memberRepo.GetProductHistory(membershipNo).ConfigureAwait(false);

            return Ok(memberDependants);
        }

        [HttpGet]
        [Route("Banking")]
        public async Task<IActionResult> GetMemberBankingDetails([FromQuery, SwaggerParameter("Medihelp Membership number", Required = true)]int membershipNo)
        {
            var bankingDetails = await _memberRepo.GetBankingDetails(membershipNo).ConfigureAwait(false);

            return Ok(bankingDetails);
        }

        [HttpGet]
        [Route("SubscriptionBreakdown")]
        public async Task<IActionResult> GetMemberSubscriptionBreakdown(
            [FromQuery, SwaggerParameter("Medihelp Membership number", Required = true)]int membershipNo,
            [FromQuery, SwaggerParameter("The Month (YYYYMMDD) you are requesting the subscription breadown for. If no Month is specificed, the current month's breakdown will be retieved", Required = true)]string month)
        {
            var reqMonth = DateTime.Now;

            if (!string.IsNullOrEmpty(month))
                reqMonth = DateTime.ParseExact(month, "yyyyMMdd", CultureInfo.InvariantCulture);

            var subBreakdown = await _memberRepo.GetSubscriptionBreakdown(membershipNo, reqMonth).ConfigureAwait(false);

            return Ok(subBreakdown);
        }

        [HttpGet]
        [Route("Exclusions")]
        public async Task<IActionResult> GetMemberExclusionsList([FromQuery, SwaggerParameter("Medihelp Membership number", Required = true)]int membershipNo, int dependantNo)
        {
            var waitingPeriods = await _memberRepo.GetMemberExcluisions(membershipNo, dependantNo).ConfigureAwait(false);

            return Ok(waitingPeriods);
        }

        [HttpGet]
        [Route("Penalties")]
        public async Task<IActionResult> GetMemberLatePenalties([FromQuery, SwaggerParameter("Medihelp Membership number", Required = true)]int membershipNo)
        {
            var lateJoinerPens = await _memberRepo.GetMemberPenalties(membershipNo).ConfigureAwait(false);

            return Ok(lateJoinerPens);
        }
        
    }
}