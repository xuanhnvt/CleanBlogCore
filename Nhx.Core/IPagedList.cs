using System.Collections.Generic;

namespace Nhx.Core
{
    /// <summary>
    /// Paged list interface
    /// </summary>
    public interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// Page index
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Page size
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Total count
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Total pages
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Has previous page
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Has next age
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        /// Is first page
        /// </summary>
        bool IsFirstPage { get; }

        /// <summary>
        /// Is last page
        /// </summary>
        bool IsLastPage { get; }

        /// <summary>
        /// First item on this page
        /// </summary>
        int FirstItemOnPage { get; }

        /// <summary>
        /// Last item on this page
        /// </summary>
        int LastItemOnPage { get; }
    }
}
