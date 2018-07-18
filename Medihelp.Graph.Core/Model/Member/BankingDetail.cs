using System;
using System.Collections.Generic;
using System.Text;

namespace Medihelp.Graph.Core.Model.Member
{
    public class BankingDetail
    {
        public String AccountDescription { get; set; }
        public String AccDepWithdraw { get; set; }
        public String AccountHolder { get; set; }
        public String BankName { get; set; }
        public String BranchCode { get; set; }
        public String AccountType { get; set; }
        public String AccountNumber { get; set; }
        public String EffectiveDate { get; set; }
        public String EndDate { get; set; }
        public String BankTransferEffective { get; set; }
        public String RateChargeIndicator { get; set; }
        public String EmployerGroup { get; set; }
        public String EmployerGroupDescription { get; set; }
    }
}
