using GraphQL.Types;
using Medihelp.Graph.Core.Data;
using Medihelp.Graph.Core.Model.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Linq;
using Medihelp.Graph.Core.Data.Repository.Interface;

namespace Medihelp.Graph.Api.Model.GraphTypes
{
    public class MedihelpMemberType : ObjectGraphType<MedihelpMember>
    {
        public MedihelpMemberType(IMemberRepository memberRepo)
        {
            Field(x => x.MemberNumber).Description("");
            Field(x => x.Title).Description(""); ;
            Field(x => x.Init).Description(""); ;
            Field(x => x.FirstName).Description(""); ;
            Field(x => x.Surname).Description(""); ;
            Field(x => x.Gender).Description(""); ;
            Field(x => x.Status).Description(""); ;

            Field(x => x.IdNumber).Description(""); ;
            Field(x => x.Language).Description(""); ;
            Field(x => x.EmailAddress).Description(""); ;
            Field(x => x.Product).Description(""); ;
            Field(x => x.ProductDescription).Description(""); ;
            Field(x => x.IsNetwork).Description(""); ;
            Field(x => x.EmployerGroup).Description(""); ;


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
