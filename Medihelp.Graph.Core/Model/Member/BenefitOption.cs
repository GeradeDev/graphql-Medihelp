using System;
using System.Collections.Generic;
using System.Text;

namespace Medihelp.Graph.Core.Model.Member
{
    public class BenefitOption
    {
        public String Code { get; set; }
        public String Description { get; set; }
        public Boolean? IsNetworkOption { get; set; }
        public DateTime? DateEffective { get; set; }
        public DateTime? DateEnded { get; set; }
    }
}
