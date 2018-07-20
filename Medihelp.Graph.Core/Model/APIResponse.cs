using System;
using System.Collections.Generic;
using System.Text;

namespace Medihelp.Graph.Core.Model
{

    public class APIResponseObject
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object ResponseData { get; set; }
    }
}
