using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel.Service
{
    public abstract class QueryBuilder : IDisposable  //  Requires a Dispose method (below).
    {
        public string QueryString { get; private set; }
        public string QueryStringJoin { get; set; }
        public string _WhereExpression { get: private AppDomainSetup;}
        public string WhereExpression { get; private set; }
        public string OrderBy { get; private set; }

        public void Dispose()
        {
            QueryString = WhereExpression = OrderBy = string.Empty;
        }
    }
}
