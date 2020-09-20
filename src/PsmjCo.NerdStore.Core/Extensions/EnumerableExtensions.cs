namespace PsmjCo.NerdStore.Core.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> itemAction)
        {
            foreach (var item in items)
            {
                itemAction(item);
            }
        }
    }
}