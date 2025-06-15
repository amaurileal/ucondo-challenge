using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ucondo_challenge.infrastructure.Utils
{
    public static class ListSortingUtils
    {
        public static IEnumerable<T> OrderByCode<T>(this IEnumerable<T> source, Func<T,string> codeSelector)
        {
            return source.OrderBy(x => codeSelector(x).Split('.').Select(int.Parse),
                new ComparadorHierarquico());   
        }
    }

    public class ComparadorHierarquico : IComparer<IEnumerable<int>>
    {
        public int Compare(IEnumerable<int> x, IEnumerable<int> y)
        {
            var enumX = x.GetEnumerator();
            var enumY = y.GetEnumerator();

            while (true)
            {
                var hasNextX = enumX.MoveNext();
                var hasNextY = enumY.MoveNext();

                if (!hasNextX && !hasNextY) return 0; // Iguais
                if (!hasNextX) return -1;
                if (!hasNextY) return 1;

                int cmp = enumX.Current.CompareTo(enumY.Current);
                if (cmp != 0)
                    return cmp;
            }
        }
    }
}
