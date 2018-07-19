using System;
using System.Collections.Generic;
using System.Text;

namespace Medihelp.Graph.Core.Model.Member
{
    public class Exclusion
    {
        public string Diagnosis { get; set; }
        public string DiagnosisCode { get; set; }
        public string DependantNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public string PMBCat { get; set; }
    }
}
