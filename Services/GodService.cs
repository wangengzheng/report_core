using Report_NetCore.Factory;
using Report_NetCore.Interfaces;
using Report_NetCore.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Report_NetCore.Services
{
    public class GodService : IGod
    {
        IDatabase _database;
        public GodService(IDatabase database) {
            _database = database;
        }

        public IEnumerable<dynamic> GetAllResult(string sql)
        {
            using (var conn = _database.CreateConnection())
            using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    return conn.Query(sql, transaction: tran).ToList();
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    return new List<dynamic>();
                }
                finally
                {
                    tran.Rollback();
                }
            }
        }

        public int GetCount(string sql)
        {
            var countSql = _database.GetRowCountSql(sql);
            using (var conn = _database.CreateConnection())
            using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    return conn.ExecuteScalar<int>(countSql, transaction: tran);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    return -1;
                }
                finally
                {
                    tran.Rollback();
                }
            }
        }

        public bool GetIsCanRun(string sql)
        {
            using (var conn = _database.CreateConnection())
            using (var tran = conn.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    using (var reader = conn.ExecuteReader(sql, transaction: tran))
                    {
                        return reader.Read();
                    }
                }               
                finally
                {
                    tran.Rollback();
                }
            }
        }

        public IEnumerable<dynamic> GetOneResult(string sql)
        {
            var oneResultSql = _database.GetOneRowResultSql(sql);
            return GetAllResult(oneResultSql);
        }

        public IEnumerable<dynamic> GetLimitRowResult(GetJsonResultRequestModel data)
        {
            var pageResultSql = _database.GetLimitRowResultSql(
                sql: data.Sql, 
                skip: (data.Page - 1) * data.Rows, 
                take: data.Rows, 
                orderByName: data.Sort, 
                orderByMode: data.Order);

            return GetAllResult(pageResultSql);
        }
    }
}
