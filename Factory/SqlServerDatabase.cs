using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Report_NetCore.Factory
{
    public class SqlServerDatabase : IDatabase
    {
        DatabaseSection _options;
        const string CONNECTIONNAME = "CONNECTIONNAME";
        IHttpContextAccessor _httpContext;
        public SqlServerDatabase(IOptions<DatabaseSection> options, IHttpContextAccessor httpContext) {
            _options = options.Value;
            _httpContext = httpContext;
        }

        public IDbConnection CreateConnection()
        {
            if (_httpContext.HttpContext.Request.Cookies.TryGetValue(CONNECTIONNAME, out string connectionName)
                && _options.ConnectionDict.ContainsKey(_options.ConnectionName)) {
                var connName = new SqlConnection(_options.ConnectionDict[connectionName]);
                connName.Open();
                return connName;
            }

            if (!_options.ConnectionDict.ContainsKey(_options.ConnectionName)) {
                throw new ArgumentException($"连接字符串字典没有对应{_options.ConnectionName}的值");
            }


            var conn = new SqlConnection(_options.ConnectionDict[_options.ConnectionName]);
            conn.Open();
            return conn;
        }

        public string GetLimitRowResultSql(string sql , int skip, int take, string orderByName, string orderByMode = "desc")
        {
            return $"{sql} order by {orderByName} {orderByMode} OFFSET {skip} ROWS  FETCH NEXT {take} ROWS ONLY";
        }

        public string GetOneRowResultSql(string sql)
        {
            return $"select top 1 * from ({sql}) as a ";
        }

        public string GetRowCountSql(string sql)
        {
            return $"select count(*) from (select * from ({sql}) as b) as a";
        }
    }
}
