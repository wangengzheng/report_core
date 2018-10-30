using Report_NetCore.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report_NetCore.Interfaces
{
    public interface IGod
    {
        IEnumerable<dynamic> GetOneResult(string sql);

        IEnumerable<dynamic> GetAllResult(string sql);

        IEnumerable<dynamic> GetLimitRowResult(GetJsonResultRequestModel data);

        int GetCount(string sql);

        bool GetIsCanRun(string sql);
    }
}
