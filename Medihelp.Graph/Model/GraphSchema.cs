using GraphQL;
using GraphQL.Types;

namespace Medihelp.Graph.Api.Model
{
    public class GraphSchema : Schema
    {
        public GraphSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<MemberGraphQuery>();
        }
    }
}
