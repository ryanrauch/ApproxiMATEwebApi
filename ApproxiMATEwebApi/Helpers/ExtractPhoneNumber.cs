using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Helpers
{
    public static class ExtractPhoneNumber
    {
        public static string RemoveNonNumeric(string input)
        {
            string result = string.Empty;
            foreach (char c in input)
            {
                if (Char.IsNumber(c))
                    result += c;
            }
            return result;
        }
    }
}
