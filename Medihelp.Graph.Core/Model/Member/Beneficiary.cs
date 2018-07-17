using System;
using System.Collections.Generic;
using System.Text;

namespace Medihelp.Graph.Core.Model.Member
{
    public class Beneficiary
    {
        public int DependantNumber { get; set; }
        public DateTime? BenefitDate { get; set; }
        public string IdNumber { get; set; }
        public bool isValidIdNumber { get; set; }
        public string PassportNumber;
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string CellphoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Status { get; set; }
        public string DependantStatus { get; set; }
        public string Gender { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public DateTime? TerminateDate { get; set; }
        public string Title { get; set; }
        public string Initials { get; set; }
        public bool IsTerminated { get; set; }
        public string ProductDescription { get; set; }
        public bool Suspended { get; set; }
        public string SuspendReason { get; set; }
        public string SuspendReasonDesccription { get; set; }
        public DateTime? SuspendStartDate { get; set; }
        public DateTime? SuspendEndDate { get; set; }
    }
}
