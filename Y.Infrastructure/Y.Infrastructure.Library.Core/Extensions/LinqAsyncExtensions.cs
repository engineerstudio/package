using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class LinqAsyncExtensions
    {
        public static async Task<bool> AnyAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> func)
        {
            foreach (var element in source)
            {
                if (await func(element))
                    return true;
            }

            return false;
        }

        public static async Task<TResult[]> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, Task<TResult>> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return await Task.WhenAll(source.Select(selector));
        }
    }
}