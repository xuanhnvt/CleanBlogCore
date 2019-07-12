using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanBlog.Web.Models.Blogs.ViewModels
{
    public class BlogPostViewModel
    {
        /// <summary>
        /// Gets or sets post id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets user name
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets post title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets post subtitle
        /// </summary>
        public string SubTitle { get; set; }


        public DateTime CreatedOnUtc { get; set; }
    }
}
