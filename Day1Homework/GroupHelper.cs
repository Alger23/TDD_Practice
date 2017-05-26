using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1Homework
{
    public class GroupHelper: IGroupHelper
    {
        public IEnumerable<int> GroupSum<T>(IList<T> data, Func<T, int> getValueFn, int length)
        {
            var values = data.Select(getValueFn);
            return GetGroup(values, length);
        }

        public IEnumerable<int> GroupSum<T>(IList<T> data, string property, int length)
        {
            var t = typeof(T);
            var p = t.GetProperty(property);
            if (p == null) throw new MissingFieldException(property);

            var values = data.Select(o => (int)Convert.ChangeType(p.GetValue(o), TypeCode.Int32));
            return GetGroup(values, length);
        }

        public IEnumerable<int> GetGroup(IEnumerable<int> values, int length)
        {
            int index = 0;
            int total = values.Count();
            while (index < total)
            {
                yield return values.Skip(index).Take(length).Sum();
                index += length;
            }
        }

    }
    public interface IGroupHelper
    {
        IEnumerable<int> GroupSum<T>(IList<T> data, string property, int length);
        IEnumerable<int> GroupSum<T>(IList<T> data, Func<T,int> getValueFn, int length);
    }


}
