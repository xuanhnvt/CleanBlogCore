using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanBlog.Web.Models.Blogs.ViewModels
{

    /// <summary>
    /// Request Model
    /// </summary>
    public class BlogPagingFilterModel: PagingFilterModel
    {
        /// <summary>
        /// The month.
        /// </summary>
        public string Month { get; set; }
        /// <summary>
        /// The tag.
        /// </summary>
        public string Tag { get; set; }
    }
}
