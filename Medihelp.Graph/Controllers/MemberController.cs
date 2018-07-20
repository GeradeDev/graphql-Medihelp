using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Medihelp.Graph.Core.Data;
using Medihelp.Graph.Core.Data.Repository.Interface;
using Medihelp.Graph.Core.Model;
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

        [HttpGet("{membershipNo}")]
        /// <summary>
        /// Member Endpoint. Get principle member details
        /// </summary>
        /// <param name="membershipNo"></param>
        /// <returns></returns>
        public async Task<IActionResult> Get([SwaggerParameter("Medihelp Membership number", Required = true)]int membershipNo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var memberDetail = await _memberRepo.GetMember(membershipNo).ConfigureAwait(false);

            return Ok(new APIResponseObject { Success = true, Message = "", ResponseData = memberDetail });
        }


        [HttpGet]
        [Route("{membershipNo}/Dependants/{dependantNo=0}")]
        public async Task<IActionResult> GetDependants([Required]int membershipNo, int dependantNo)
        {
            var memberDependants = await _memberRepo.GetDepoendantsOnly(membershipNo, dependantNo).ConfigureAwait(false);

            return Ok(new APIResponseObject { Success = true, Message = "", ResponseData = memberDependants });
        }

        [HttpGet]
        [Route("{membershipNo}/Addresses/{type=all}")]
        public async Task<IActionResult> MemberAddresses([Required]int membershipNo, string type)
        {
            string[] addressTypes = new [] { "postal", "work", "street", "executors", "all" };

            if (!String.IsNullOrEmpty(type) && (addressTypes.Where(x => !addressTypes.Contains(type)).Count() > 0))
                return BadRequest(new APIResponseObject { Success = false, Message = "Adress type is invalid. Must be 'post', 'work', 'street' or 'executors'" });

            var memberDependants = await _memberRepo.GetAllAddresses(membershipNo, type).ConfigureAwait(false);

            return Ok(new APIResponseObject { Success = true, Message = "", ResponseData = memberDependants });
        }

        [HttpGet]
        [Route("{membershipNo}/Contacts/{type=all}")]
        public async Task<IActionResult> MemberContacts([Required]int membershipNo, string type)
        {
            string[] contactTypes = new[] { "work", "home", "fax", "cell", "all" };

            if (!String.IsNullOrEmpty(type) && (contactTypes.Where(x => !contactTypes.Contains(type)).Count() > 0))
                return BadRequest(new APIResponseObject { Success = false, Message = "Adress type is invalid. Must be 'work', 'home', 'fax' or 'cell'" });

            var memberDependants = await _memberRepo.GetContacts(membershipNo, type).ConfigureAwait(false);

            return Ok(new APIResponseObject { Success = true, Message = "", ResponseData = memberDependants });
        }

        [HttpGet]
        [Route("{membershipNo}/ProductHistory")]
        public async Task<IActionResult> MemberBenefitOptionHistory([Required]int membershipNo)
        {
            var optionHistory = await _memberRepo.GetProductHistory(membershipNo).ConfigureAwait(false);

            return Ok(new APIResponseObject { Success = true, Message = "", ResponseData = optionHistory });
        }


        [HttpGet]
        [Route("{membershipNo}/Banking")]
        public async Task<IActionResult> GetMemberBankingDetails([Required]int membershipNo)
        {
            var bankingDetails = await _memberRepo.GetBankingDetails(membershipNo).ConfigureAwait(false);

            return Ok(new APIResponseObject { Success = true, Message = "", ResponseData = bankingDetails });
        }

        [HttpGet]
        [Route("{membershipNo}/SubscriptionBreakdown/{month=}")]
        public async Task<IActionResult> GetMemberSubscriptionBreakdown([Required]int membershipNo, string month)
        {
            var reqMonth = DateTime.Now;

            if (!string.IsNullOrEmpty(month))
                reqMonth = DateTime.ParseExact(month, "yyyyMMdd", CultureInfo.InvariantCulture);

            var subBreakdown = await _memberRepo.GetSubscriptionBreakdown(membershipNo, reqMonth).ConfigureAwait(false);

            return Ok(new APIResponseObject { Success = true, Message = "", ResponseData = subBreakdown });
        }

        [HttpGet]
        [Route("{membershipNo}/Exclusions")]
        public async Task<IActionResult> GetMemberExclusionsList([Required]int membershipNo, int dependantNo)
        {
            var waitingPeriods = await _memberRepo.GetMemberExcluisions(membershipNo, dependantNo).ConfigureAwait(false);

            return Ok(new APIResponseObject { Success = true, Message = "", ResponseData = waitingPeriods });
        }

        [HttpGet]
        [Route("{membershipNo}/Penalties")]
        public async Task<IActionResult> GetMemberLatePenalties([Required]int membershipNo)
        {
            var lateJoinerPens = await _memberRepo.GetMemberPenalties(membershipNo).ConfigureAwait(false);

            return Ok(new APIResponseObject { Success = true, Message = "", ResponseData = lateJoinerPens });
        }
        
        [HttpGet]
        [Route("{membershipNo}/Benefits")]
        public async Task<IActionResult> GetMemberAvailableBenefits([Required]int membershipNo, int dependantNo)
        {
            var availBenefits = await _memberRepo.GetAvailableBenefits(membershipNo).ConfigureAwait(false);

            return Ok(new APIResponseObject { Success = true, Message = "", ResponseData = availBenefits });
        }

        [HttpGet]
        [Route("{membershipNo}/SavingsRecon")]
        public async Task<IActionResult> GetMemberSavingsRecon([Required]int membershipNo)
        {
            var savingsRecon = await _memberRepo.GetSavingsReconciliation(membershipNo).ConfigureAwait(false);

            return Ok(new APIResponseObject { Success = true, Message = "", ResponseData = savingsRecon });
        }
        
    }
}