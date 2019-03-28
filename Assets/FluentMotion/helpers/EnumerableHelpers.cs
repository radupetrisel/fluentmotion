using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.FluentMotion.helpers
{
    public static class EnumerableHelpers
    {

        public static void ForEach<T>(this IEnumerable<T> ts, Action<T> action)
        {
            foreach (var elem in ts)
                action(elem);
        }

    }
}
