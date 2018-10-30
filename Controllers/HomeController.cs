using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Report_NetCore.Factory;
using Report_NetCore.Models.RequestModels;
using Dapper;
using Newtonsoft.Json;
using System.Data;
using Microsoft.AspNetCore.Http;
using Report_NetCore.Interfaces;

namespace Report_NetCore.Controllers
{    
    public class HomeController : Controller
    {
        const string SQLSESSIONNAME = "SQLSESSIONNAME";
        const string CONNECTIONNAME = "CONNECTIONNAME";
        IGod _god;
        public HomeController(IGod god) {
            _god = god;
        }

        [HttpGet]
        public IActionResult Index(string sqlValue)
        {
            ViewBag.SqlValue = sqlValue;
            if (!string.IsNullOrEmpty(sqlValue)) {
                HttpContext.Session.SetString(SQLSESSIONNAME, sqlValue);                
                var list = _god.GetOneResult(sqlValue);
                if (list.Count() > 0) {
                    ViewBag.ColumnName = ((System.Collections.Generic.IDictionary<string, object>)list.FirstOrDefault())?.Keys;
                    ViewBag.排序字段 = ViewBag.ColumnName[0];
                    ViewBag.排序方式 = "desc";                    
                }
            }

            if (Request.Query.ContainsKey("c")) {
                HttpContext.Response.Cookies.Append(
                    key:CONNECTIONNAME, 
                    value:Request.Query["c"].ToString().ToUpper(),
                    options:new CookieOptions() { Expires = new DateTimeOffset(DateTime.Now.AddYears(1)), HttpOnly = true}
                );
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm]HomeIndexRequestModel data)
        {
            return Index(data.SqlValue);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public string CheckSQLSuccess([FromForm]HomeIndexRequestModel data)
        {
            try
            {
                if (_god.GetIsCanRun(data.SqlValue)){
                    return "True";
                }
                else{
                    return "False";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        [HttpPost]
        public JsonResult GetJsonResult([FromForm]GetJsonResultRequestModel data)
        {
            int total = 0;

            string sql = HttpContext.Session.GetString(SQLSESSIONNAME);
            dynamic rows = null;
            if (!string.IsNullOrEmpty(sql))
            {
                data.Sql = sql;
                total = _god.GetCount(sql);
                rows = _god.GetLimitRowResult(data);
            }
            return Json(new { total = total, rows = rows });            
        }
               
    }
}