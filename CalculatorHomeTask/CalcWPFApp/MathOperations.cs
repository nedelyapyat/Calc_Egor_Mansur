using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcWPFApp
{
    internal static class MathOperations
    {
        public static string Sum(double a, double b)
        {
            string result;

            var res = a + b;
            result = res.ToString();

            return result;
        }

        public static string Divide(double a, double b)
        {
            if (b == 0)
            {
                return "0";
            }
            var res = a / b;
            return res.ToString();
        }
    }
}
