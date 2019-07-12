using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CleanBlog.Services.Blogs;
using CleanBlog.Web.Models.Blogs.ViewModels;
using CleanBlog.Web.Helpers;
using Microsoft.AspNetCore.Routing;

namespace CleanBlog.Web.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogService _blogService;
        private readonly IRazorPartialToStringRenderer _razorPartialToStringRenderer;

        public IndexModel(ILogger<IndexModel> logger, IBlogService blogService, IRazorPartialToStringRenderer razorPartialToStringRenderer)
        {
            _logger = logger;
            _blogService = blogService;
            _razorPartialToStringRenderer = razorPartialToStringRenderer;
        }

        public BlogPagingFilteringViewModel PagingFilteringViewModel { get; set; }
        public List<BlogPostListRowViewModel> PreviewPostList { get; set; }

        public async Task<ActionResult> OnGetAsync()
        {
            PagingFilteringViewModel = new BlogPagingFilteringViewModel();
            PreviewPostList = new List<BlogPostListRowViewModel>();

            var posts = await _blogService.GetAllBlogPostsAsync(pageSize: 5);
            PagingFilteringViewModel.LoadPagedList(posts);
            foreach (var post in posts)
            {
                var item = new BlogPostListRowViewModel
                {
                    Slug = post.Title,
                    AuthorName = "Xuan Nguyen",
                    Title = post.Title,
                    BodyOverview = post.BodyOverview,
                    CreatedOnUtc = post.CreatedOnUtc,
                };
                item.Tags.Add(new BlogPostListRowViewModel.Tag
                {
                    Slug = "fdaf-dfadfa",
                    Name = "test"
                });
                item.Tags.Add(new BlogPostListRowViewModel.Tag
                {
                    Slug = "faafd-kgkg",
                    Name = "hahhah"
                });
                PreviewPostList.Add(item);
            }
            return Page();
        }

        public async Task<ActionResult> OnGetLoadMoreAsync(int pageNumber)
        {
            // set default page size, it should be set in admin page
            // for testing, set here
            int pagesize = 5;
            var posts = await _blogService.GetAllBlogPostsAsync(pageNumber: pageNumber, pageSize: 5);
            var list = new List<BlogPostListRowViewModel>();
            foreach (var post in posts)
            {
                var item = new BlogPostListRowViewModel
                {
                    Slug = post.Title,
                    AuthorName = "Xuan Nguyen",
                    Title = post.Title,
                    BodyOverview = post.BodyOverview,
                    CreatedOnUtc = post.CreatedOnUtc,
                };
                item.Tags.Add(new BlogPostListRowViewModel.Tag
                {
                    Slug = "fdaf-dfadfa",
                    Name = "test"
                });
                list.Add(item);
            }

            PostListPartialViewModel partialViewModel = new PostListPartialViewModel();
            partialViewModel.NoMoredata = list.Count < pagesize;
            partialViewModel.HtmlString = await _razorPartialToStringRenderer.RenderPartialToStringAsync("_PostListPartial", list);

            return new JsonResult(partialViewModel);
        }
    }
}