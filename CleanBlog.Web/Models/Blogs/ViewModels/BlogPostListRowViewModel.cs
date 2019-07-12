using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanBlog.Web.Models.Blogs.ViewModels
{
    public class BlogPostListRowViewModel
    {
        public BlogPostListRowViewModel ()
        {
            Tags = new List<Tag>();
        }

        /// <summary>
        /// Gets or sets slug (SEO friendly url)
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets author name
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets post title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets post subtitle
        /// </summary>
        public string BodyOverview { get; set; }


        public DateTime CreatedOnUtc { get; set; }

        public List<Tag> Tags { get; set; }

        public class Tag
        {
            public string Name { get; set; }
            public string Slug { get; set; }

        }
    }
}
