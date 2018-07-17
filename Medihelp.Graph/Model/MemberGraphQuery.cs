using GraphQL.Types;
using Medihelp.Graph.Api.Model.GraphTypes;
using Medihelp.Graph.Core.Data;
using Medihelp.Graph.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medihelp.Graph.Api.Model
{
    public class MemberGraphQuery : ObjectGraphType
    {
        public MemberGraphQuery(IMemberRepository memberRepo)
        {
            Field<MedihelpMemberType>(
                "member", 
                "Query principal member details",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "memberNo" }),
                resolve: context => memberRepo.GetMember(context.GetArgument<int>("memberNo")) );
        }
    }
}
