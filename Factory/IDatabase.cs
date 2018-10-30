using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Report_NetCore.Factory
{
    public interface IDatabase
    {
        IDbConnection CreateConnection();

        string GetOneRowResultSql(string sql);

        string GetLimitRowResultSql(string sql, int skip, int take, string orderByName = "", string orderByMode = "desc");

        string GetRowCountSql(string sql);
    }
}
