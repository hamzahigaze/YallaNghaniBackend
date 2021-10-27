using System;
using System.Collections.Generic;
using System.Text;

namespace YallaNghani.Helpers.Pagination
{
    /// <summary>
    /// A class representing the pagination parameters.
    /// </summary>
    public class PaginationParameters
    {
        /// <summary>
        /// Represents the zero-based page index, the default value is 0.
        /// </summary>
        public int PageIndex { get; set; } = 0;

        /// <summary>
        /// Represents the page size, the default value is <see cref="int.MaxValue"/>.
        /// </summary>
        public int PageSize { get; set; } = int.MaxValue;
    }
}
