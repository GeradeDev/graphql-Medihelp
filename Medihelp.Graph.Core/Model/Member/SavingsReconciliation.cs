using System;
using System.Collections.Generic;
using System.Text;

namespace Medihelp.Graph.Core.Model.Member
{
    public class SavingsReconciliation
    {
        public decimal OpeningBalance { get; set; }
        public decimal SavingsAvailable { get; set; }

        public List<SavingsReconciliationLine> Lines { get; set; }

        public SavingsReconciliation()
        {
            Lines = new List<SavingsReconciliationLine>();
        }
    }

    public class SavingsReconciliationLine
    {
        public string Month { get; set; }
        public string Description { get; set; }
        public string DateService { get; set; }
        public string Patient { get; set; }
        public string DateStatement { get; set; }
        public string AmountDebit { get; set; }
        public string AmountCredit { get; set; }
        public string AmountCumulative { get; set; }
    }
}
