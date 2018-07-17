using Newtonsoft.Json.Linq;
using System;

namespace Medihelp.Graph.Api.Model
{
    public class GraphQLQuery
    {
        public String OperationName { get; set; }
        public String NamedQuery { get; set; }
        public String Query { get; set; }
        public JObject Variables { get; set; }
    }
}
