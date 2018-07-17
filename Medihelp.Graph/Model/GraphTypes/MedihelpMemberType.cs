using GraphQL.Types;
using Medihelp.Graph.Core.Data;
using Medihelp.Graph.Core.Model.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Medihelp.Graph.Api.Model.GraphTypes
{
    public class MedihelpMemberType : ObjectGraphType<MedihelpMember>
    {
        public MedihelpMemberType()
        {
            Field(x => x.MemberNumber);
            Field(x => x.Title);
            Field(x => x.Init);
            Field(x => x.FirstName);
            Field(x => x.Surname);
            Field(x => x.Gender);
            Field(x => x.Status);

            Field(x => x.IdNumber);
            Field(x => x.Language);
            Field(x => x.EmailAddress);
            Field(x => x.Product);
            Field(x => x.ProductDescription);
            Field(x => x.IsNetwork);
            Field(x => x.EmployerGroup);


            //Dependants
            Field<ListGraphType<MemberBeneficiaryType>>(
                "dependants",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "dependantNo" }),
                resolve: context =>
                {
                    int? depNo = (int?)context.GetArgument<int?>("dependantNo");

                    return (depNo == null ? context.Source.Dependents : context.Source.Dependents.Where(x => x.DependantNumber == depNo));
                },
                description: "Member dependants/beneficiaries");
        }
    }
}
