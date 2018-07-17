using GraphQL.Types;
using Medihelp.Graph.Core.Model.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medihelp.Graph.Api.Model.GraphTypes
{
    public class MemberBeneficiaryType : ObjectGraphType<Beneficiary>
    {
        public MemberBeneficiaryType()
        {
            Field(x => x.DependantNumber);
            Field(x => x.FirstName);
            Field(x => x.Surname);
            Field<StringGraphType>("benefitdate", resolve: x => x.Source.BenefitDate.Value.ToString("yyyyMMdd"));
            Field(x => x.CellphoneNumber);
            Field<StringGraphType>("dateofbirth", resolve: x => x.Source.DateOfBirth.Value.ToString("yyyyMMdd"));
            Field(x => x.DependantStatus);
            Field(x => x.EmailAddress);
            Field<StringGraphType>("enrollmentdate", resolve: x => x.Source.EnrollmentDate.Value.ToString("yyyyMMdd"));
            Field(x => x.Gender);
            Field(x => x.IdNumber);
            Field(x => x.Initials);
            Field(x => x.PassportNumber, nullable: true);
            Field(x => x.Status);
            Field(x => x.Title);
        }
    }
}
