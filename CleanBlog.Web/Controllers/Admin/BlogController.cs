using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanBlog.Services.Blogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nhx.Core.Entities.Blogs;
using Nhx.Core;
using CleanBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CleanBlog.Web.Controllers.Admin
{
    //[ApiConventionType(typeof(DefaultApiConventions))]
    public class BlogController : AdminControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly ILogger<BlogController> _logger;

        public BlogController(IBlogService blogService, ILogger<BlogController> logger)
        {
            _blogService = blogService;
            _logger = logger;
        }

        /// <summary>
        /// Get blog post list
        /// </summary>
        /// <returns></returns>
        [HttpGet("posts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IPagedList<BlogPost>>> List()
        {
            _logger.LogInformation("Get blog post list");
            var viewModel = await _blogService.GetAllBlogPostsAsync(pageNumber: 1, pageSize: 5);
            return Ok(viewModel);
            //return await viewModel.ToPagedListAsync();
        }

        /// <summary>
        /// Get blog post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("posts/{id}")]
        [ProducesResponseType(typeof(BlogPost), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BlogPost>> GetById(int id)
        {
            _logger.LogInformation("Get blog post");
            var viewModel = await _blogService.GetBlogPostByIdAsync(id);
            if (viewModel != null)
            {
                return viewModel;
            }
            return NotFound(NotFoundResponse(id, String.Format("No have blog post with id = {0}", id)));
        }

        //[Authorize(Roles = "Writer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ViewProjects")]
        [HttpPost("posts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<BlogPost>> Create([FromBody] CreateBlogPostDto model)
        {
            _logger.LogInformation("Create blog post");
            var entity = new BlogPost()
            {
                LanguageId = 7,
                Title = model.Title,
                Body = model.Body
            };
            await _blogService.InsertBlogPostAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("posts/{id}")]
        public async Task<ActionResult<BlogPost>> Update(int id)
        {
            _logger.LogInformation("Update blog post");
            return await _blogService.GetBlogPostByIdAsync(id);
        }

        /*[HttpDelete("posts/{id}")]
        [ProducesResponseType(typeof(ApiResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BlogPost>> Delete(int id)
        {
            _logger.LogInformation("Delete blog post.");
            var entity = await _blogService.GetBlogPostByIdAsync(id);
            if (entity != null)
            {
                await _blogService.DeleteBlogPostAsync(entity);
                return entity;
            }
            return NotFound(NotFoundResponse(id, String.Format("No have blog post with id = {0}", id)));
        }*/
    }
}
