using System;
using System.Collections.Generic;
using System.Text;

namespace Medihelp.Graph.Core.Model.Member
{
    public class SubscriptionBreakdown
    {
        public decimal TotalSubscription { get; set; }
        public decimal Subsidy { get; set; }
        public decimal MemberPortion { get; set; }
        public String SubsidyFactor { get; set; }
        public decimal MaxSubsCalculate { get; set; }
        public decimal MaxSubsidy { get; set; }

        public List<Dependant> Dependants { get; set; }
        public SubscriptionBreakdown()
        {
            Dependants = new List<Dependant>();
        }
    }







    public class Dependant
    {
        public String DescriptionofRelations { get; set; }
        public decimal Subscription { get; set; }
        public decimal SavingsContribution { get; set; }
        public decimal LJPAmount { get; set; }
        public decimal MonthlySubscription { get; set; }
    }
}
