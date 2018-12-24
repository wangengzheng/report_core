using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report_NetCore.Infrastructure
{
    public static class StringExtend
    {
        /// <summary>
        /// 是否存储过程
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static bool IsStoredProcedure(this string @string)
        {
            return @string?.IndexOf("exec ", StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}
