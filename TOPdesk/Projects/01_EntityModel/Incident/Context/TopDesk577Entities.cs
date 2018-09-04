using EntityModel.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOPdesk.Context
{
    public partial class TopDesk577Entities
    {
        public TopDesk577Entities(string configurationSetting)
                : base("name=" + configurationSetting)
        {
            
            //base.Database.Connection = SQLConnection.GetSQLConnection(configurationSetting);
        }

    }
}
