using GraphQL.Types;
using Medihelp.Graph.Api.Model.GraphTypes;
using Medihelp.Graph.Core.Data.Repository.Interface;

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
