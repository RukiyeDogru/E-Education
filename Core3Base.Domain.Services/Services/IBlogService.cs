using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Core3Base.Domain.Model.Filters;

namespace Core3Base.Domain.Services.Services
{
    public interface IBlogService
    {
        ServiceResponse<List<Blog>> GetBlogs(BlogFilterModel filter);
        ServiceResponse<List<Blog>> GetBlogsCount(BlogFilterModel filter);
        ServiceResponse<Blog> GetBlogById(int id);
        ServiceResponse<Blog> GetBlogBySlug(string slug);
        ServiceResponse<Blog> Add(Blog blog);
        ServiceResponse<Blog> Update(Blog blog);
        ServiceResponse<bool> Delete(int id);
        //BlogCategory
        ServiceResponse<List<BlogCategory>> GetBlogCategories(BlogCategoryFilterModel filter);
        ServiceResponse<BlogCategory> GetBlogCategoryById(int id);
        ServiceResponse<BlogCategory> AddCategory(BlogCategory blog);
        ServiceResponse<BlogCategory> UpdateCategory(BlogCategory blog);
        ServiceResponse<bool> DeleteCategory(int id);

    }
}
