using System;
using System.Collections.Generic;
using System.Text;

namespace Medihelp.Graph.Core.Model.Member
{
    public class Benefit
    {
        public string LimitCode;

        public DateTime? DateEffective;

        public string Description;

        public string LimitType;

        public string FamilyIndividual;

        public string DependantName;

        public decimal? LimitValue;

        public decimal? Used;

        public decimal? PmbUsed;

        public decimal? AmtReserved;

        public decimal? Available;

        public string DateExpire;

        public string AgeGen;

        public int ConsultationCount;

        public string OnMouseOverDesc;

        public bool IsFemaleBenefit { get; set; }
        public bool IsMaleBenefit { get; set; }
        public Int32 AgeResctriction { get; set; }
    }
}
