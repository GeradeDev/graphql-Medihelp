using System;
using System.Collections.Generic;
using System.Text;

namespace Medihelp.Graph.Core.Model.Member
{
    public class LateJoinerPenalty
    {
        public string LjpPercentage { get; set; }
        public string DepNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReasonCode { get; set; }
        public Decimal LJPAmount { get; set; }
    }
}
