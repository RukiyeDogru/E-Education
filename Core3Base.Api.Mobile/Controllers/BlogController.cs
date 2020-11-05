using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Core3Base.Api.Mobile.Model;
using Core3Base.Domain.Model.Filters;
using Core3Base.Domain.Services.Impl.Helper;
using Core3Base.Domain.Services.Services;
using Core3Base.Infra.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Core3Base.Api.Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private IBlogService blogService;
        private ImageHelper ImageHelper;
        private readonly ILogger<BlogController> _logger;

        public BlogController(IBlogService blogService, ImageHelper ımageHelper, ILogger<BlogController> logger)
        {
            this.blogService = blogService;
            ImageHelper = ımageHelper;
            _logger = logger;
        }
        [HttpGet("")]
        [SwaggerResponse(200, "", typeof(List<BlogViewModel>))]
        public IActionResult Get([FromQuery] BlogFilterModel filter)
        {
            var blogResponse = blogService.GetBlogs(filter);
            LogHelper.Info(_logger, new LogHelperModel
            {
                UserNo = "1",
                UserName = "Log Test",
                Message = "Test User Blog Getirdi.",
                ActionTaken = "GetBlog"
            });
            if (blogResponse.IsSucceeded && !blogResponse.Result.IsNullOrEmpty())
            {
               
                return Ok(blogResponse.Result.Select(x => new BlogViewModel()
                {
                    Id = x.Id,
                    CategoryTitle = x.Category.Title,
                    Title = x.Title,
                    ShortContent = x.ShortContent,
                    PublishDate = x.PublishDate,
                    //Content = x.Content,
                    ImageUrl = ImageHelper.GetImageUrlById(x.ImageId)

                }).ToList());
            }


            return blogResponse.HttpGetResponse();

        }

        [HttpGet("categories")]
        [SwaggerResponse(200, "", typeof(List<BlogCategory>))]
        public IActionResult GetCategories()
        {
            var blogResponse = blogService.GetBlogCategories(new Domain.Model.Filters.BlogCategoryFilterModel());

            return blogResponse.HttpGetResponse();

        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(Blog))]
        public IActionResult Get(int id)
        {
            var blogResponse = blogService.GetBlogById(id);

            if (blogResponse.IsSucceeded && blogResponse.Result != null)
            {
                return Ok(new BlogViewModel()
                {
                    Id = blogResponse.Result.Id,
                    CategoryTitle = blogResponse.Result.Category.Title,
                    Title = blogResponse.Result.Title,
                    Content = blogResponse.Result.Content,
                    PublishDate = blogResponse.Result.PublishDate,
                    ImageUrl = ImageHelper.GetImageUrlById(blogResponse.Result.ImageId)

                });
            }

            return blogResponse.HttpGetResponse();

        }

        [HttpGet("add-blog")]
        [SwaggerResponse(200, "", typeof(Blog))]
        public IActionResult Add([FromQuery] Blog blog)
        {
            var blogResponse = blogService.Add(blog);

            if (blogResponse.IsSucceeded && blogResponse.Result != null)
            {
                LogHelper.Info(_logger, new LogHelperModel
                {
                    UserNo = "1",
                    UserName = "Log Test",
                    Message = $"{blog.Title} Başlıklı Blog Eklendi.",
                    ActionTaken = "AddBlog"
                });
                return Ok(new BlogViewModel()
                {
                    Id = blogResponse.Result.Id,
                    CategoryTitle = blogResponse.Result.Category.Title,
                    Title = blogResponse.Result.Title,
                    Content = blogResponse.Result.Content,
                    PublishDate = blogResponse.Result.PublishDate,
                    ImageUrl = ImageHelper.GetImageUrlById(blogResponse.Result.ImageId)

                });
            }

            return blogResponse.HttpGetResponse();

        }
    }
}
