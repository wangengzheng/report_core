using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report_NetCore.Factory
{
    public class DatabaseSection
    {
        public string ConnectionName { get; set; }
        
        public Dictionary<string,string> ConnectionDict { get; set; }
    }
}
