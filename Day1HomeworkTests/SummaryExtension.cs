using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1HomeworkTests
{
    public static class SummaryExtension
    {
        public static IEnumerable<int> GetSubTotals<T>(this IEnumerable<T> data, Func<T, int> getValueFunc, int length)
        {
            var index = 0;
            while (index < data.Count())
            {
                yield return data.Skip(index).Take(length).Select(getValueFunc).Sum();
                index += length;
            }
        }
    }
}
