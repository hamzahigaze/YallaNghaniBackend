using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace YallaNghani.Helpers.Pagination
{
    /// <summary>
    /// A class representing a paged items list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    
    public class PagedList<T> 
    {
        /// <summary>
        /// Represents the zero-based page index.
        /// </summary>
        
        public int PageIndex { get; private set; }

        /// <summary>
        /// Represents the total pages number.
        /// </summary>

        public int TotalPages { get; private set; }

        /// <summary>
        /// Represents the page size.
        /// </summary>

        public int PageSize { get; private set; }

        /// <summary>
        /// Represents the total items count.
        /// </summary>

        public int TotalCount { get; private set; }

        /// <summary>
        /// Indicates whether the list has a previous page or not.
        /// </summary>

        public bool HasPrevious => PageIndex > 0;

        /// <summary>
        /// Indicates whether the list has a next page or not.
        /// </summary>

        public bool HasNext => PageIndex < TotalPages - 1;

        public List<T> Items { get; set; } = new List<T>();

        /// <summary>
        /// Create a new instance of <see cref="PagedList{T}"/>.
        /// </summary>
        /// <param name="items">The paginated items.</param>
        /// <param name="count">Total items count.</param>
        /// <param name="pageIndex">The current page index.</param>
        /// <param name="pageSize">The page size.</param>
        public PagedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            Items.AddRange(items);
        }

        [JsonIgnore]
        public dynamic Metadata
        {
            get
            {
                return new
                {
                    TotalCount,
                    PageSize,
                    PageIndex,
                    TotalPages,
                    HasPrevious,
                    HasNext
                };
            }
        }
    }
}
