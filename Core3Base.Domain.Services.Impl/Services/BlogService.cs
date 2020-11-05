using Core3Base.Domain.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core3Base.Domain.Model.Base;
using Core3Base.Domain.Model.Filters;
using Core3Base.Domain.Services.Impl.Helper;
using Core3Base.Domain.Validations;
using Core3Base.Infra.Data.Entity;
using Core3Base.Infra.Data.Repository;

namespace Core3Base.Domain.Services.Impl.Services
{
   public class BlogService : IBlogService
    {
        private readonly IRepository<Blog> blogRepository;
        private readonly IRepository<BlogCategory> blogCategoryRepository;

        public BlogService(IRepository<Blog> blogRepository, IRepository<BlogCategory> blogCategoryRepository)
        {
            this.blogRepository = blogRepository;
            this.blogCategoryRepository = blogCategoryRepository;
        }

        public ServiceResponse<Blog> Add(Blog blog)
        {
            var response = new ServiceResponse<Blog>();
            if (response.Validation(new BlogValidation().Validate(blog)))
            {
                response.Result = blogRepository.Insert(blog);
            }

            return response;
        }
        public ServiceResponse<Blog> Update(Blog blog)
        {
            var response = new ServiceResponse<Blog>();
            if (response.Validation(new BlogValidation().Validate(blog)))
            {
                blogRepository.Detach(blog);

                var repositoryResponse = blogRepository.GetById(blog.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.ImageId = blog.ImageId;
                    repositoryResponse.Title = blog.Title;
                    repositoryResponse.Content = blog.Content;
                    repositoryResponse.ShortContent = blog.ShortContent;
                    repositoryResponse.CategoryId = blog.CategoryId;
                    repositoryResponse.Slug = blog.Slug;
                    repositoryResponse.IsPublished = blog.IsPublished;
                    repositoryResponse.PublishDate = blog.PublishDate;

                    response.Result = blogRepository.Update(repositoryResponse);
                }
                else
                {
                    response.SetError("Veri Bulunamadı");
                }

            }

            return response;

        }
        public ServiceResponse<Blog> GetBlogById(int id)
        {
            var response = new ServiceResponse<Blog>();

            response.IsSucceeded = true;
            response.Result = blogRepository.GetById(id);

            return response;
        }

        public ServiceResponse<Blog> GetBlogBySlug(string slug)
        {
            var response = new ServiceResponse<Blog>();

            response.IsSucceeded = true;
            response.Result = blogRepository.ListQueryable
                .FirstOrDefault(x => x.Slug == slug);

            return response;
        }
        public ServiceResponse<List<Blog>> GetBlogs(BlogFilterModel filter)
        {

            var response = new ServiceResponse<List<Blog>>();
            response.IsSucceeded = true;
            response.RecordsTotal = blogRepository.ListQueryable.Count();
            response.RecordsFiltered = blogRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = blogRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();

            return response;
        }
        public ServiceResponse<List<Blog>> GetBlogsCount(BlogFilterModel filter)
        {

            var response = new ServiceResponse<List<Blog>>();

            response.IsSucceeded = true;

            response.RecordsTotal = blogRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).Count();
            response.RecordsFiltered = blogRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = new List<Blog>();

            return response;
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = blogRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                blogRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;

        }

        //Blog Category Services
        public ServiceResponse<List<BlogCategory>> GetBlogCategories(BlogCategoryFilterModel filter)
        {
            var response = new ServiceResponse<List<BlogCategory>>();

            response.IsSucceeded = true;

            response.RecordsTotal = blogCategoryRepository.ListQueryable.Count();
            response.RecordsFiltered = blogCategoryRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = blogCategoryRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();


            return response;
        }
        public ServiceResponse<List<BlogCategory>> GetBlogCategoriesNotDuyutuAndBizdenHaberler(BlogCategoryFilterModel filter)
        {
            var response = new ServiceResponse<List<BlogCategory>>();

            response.IsSucceeded = true;

            response.RecordsTotal = blogCategoryRepository.ListQueryable.Count();
            response.RecordsFiltered = blogCategoryRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = blogCategoryRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).Where(x => !(x.Title.Contains("Duyuru")) && !(x.Title.Contains("Bizden Haberler"))).ToList();


            return response;
        }

        public ServiceResponse<BlogCategory> GetBlogCategoryById(int id)
        {
            var response = new ServiceResponse<BlogCategory>();

            response.IsSucceeded = true;
            response.Result = blogCategoryRepository.GetById(id);

            return response;
        }

        public ServiceResponse<BlogCategory> AddCategory(BlogCategory blog)
        {
            var response = new ServiceResponse<BlogCategory>();
            if (response.Validation(new BlogCategoryValidation().Validate(blog)))
            {
                response.Result = blogCategoryRepository.Insert(blog);
            }

            return response;
        }

        public ServiceResponse<BlogCategory> UpdateCategory(BlogCategory blog)
        {
            var response = new ServiceResponse<BlogCategory>();
            if (response.Validation(new BlogCategoryValidation().Validate(blog)))
            {
                response.Result = blogCategoryRepository.Update(blog);
            }

            return response;
        }

        public ServiceResponse<bool> DeleteCategory(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = blogCategoryRepository.GetById(id);

            if (repoResponse != null && repoResponse.IsDeletable == true)
            {
                var blogRepoResponse = blogRepository.ListQueryable.Any(x => x.CategoryId == id);
                if (!blogRepoResponse)
                {
                    blogCategoryRepository.Delete(repoResponse);

                }
                else
                {
                    response.SetError("Kategoriye bağlı blog yazıları bulunmakta,önce bunları düzeltiniz.");
                }

            }
            else
            {
                response.SetError("Kayıt Silinemedi");
            }

            return response;

        }

    }
}
