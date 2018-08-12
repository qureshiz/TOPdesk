using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EntityModel

{
    public class Incident
    {
        public string IncidentNumber { get; set; }
        public bool SLARespondBreached { get; set; }
        public bool SLAResolveBreached { get; set; }
        public bool Escalation { get; set; }
        public string ChangerOfTheCard { get; set; }
    }
}
