using System;
using System.Collections.Generic;
using System.Text;

namespace YallaNghani.Helpers.Pagination
{
    public static class PaginationExtensions
    {
        /// <summary>
        /// Converts the <paramref name="items"/> to a paged list.
        /// </summary>
        /// <typeparam name="T">Items type.</typeparam>
        /// <param name="items">A list of items to convert to <see cref="PagedList{T}"/>.</param>
        /// <param name="count">Total items count.</param>
        /// <param name="pageIndex">The zero-based page index.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
