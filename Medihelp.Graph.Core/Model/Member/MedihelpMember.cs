using System;
using System.Collections.Generic;
using System.Text;

namespace Medihelp.Graph.Core.Model.Member
{
    public class MedihelpMember
    {
        public Int32 MemberNumber { get; set; }
        public string Title { get; set; }
        public string Init { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ResignReason { get; set; }
        public string ResignDesc { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }



        public string IdNumber { get; set; }
        public string Language { get; set; }
        public string EmailAddress { get; set; }
        public string Product { get; set; }
        public string ProductDescription { get; set; }
        public Boolean IsNetwork { get; set; }
        public string EmployerGroup { get; set; }

        public string EmployerGroupDescription { get; set; }
        public string MaritalStatus { get; set; }
        public string MaritalStatusDescription { get; set; }
        public DateTime? DateMarital { get; set; }
        public Boolean VisuallyImpaired { get; set; }
        public decimal? MonthlyContribution { get; set; }
        public Boolean HoldPost { get; set; }

        public Boolean ArrearNotification { get; set; }
        public Boolean TerminationNotification { get; set; }
        

        public List<Beneficiary> Dependents { get; set; }
        public ContactDetail HomeAddress { get; set; }
        public ContactDetail PostalAddress { get; set; }
        public ContactDetail WorkAddress { get; set; }
        public ContactDetail ContactDetails { get; set; }

        public BankingDetail Banking { get; set; }
    }
}
