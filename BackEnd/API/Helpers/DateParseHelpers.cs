using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public static class DateParseHelpers
    {
        public static string GetDate()
        {
            string str = DateTime.Now.ToString().Trim();
            str = str.Substring(0, 10);
            return str;
        }

    }
}
