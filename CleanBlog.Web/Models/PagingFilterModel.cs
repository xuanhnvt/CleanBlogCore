using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanBlog.Web.Models
{

    /// <summary>
    /// Request Model
    /// </summary>
    public class PagingFilterModel
    {
        /// <summary>
        /// The request page number (starts from 1)
        /// </summary>
        int PageNumber { get; set; }
        /// <summary>
        /// The request number of items in each page.
        /// </summary>
        int PageSize { get; set; }
    }
}
