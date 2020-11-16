using Core3Base.Domain.Filters;
using Core3Base.Domain.Model;
using Core3Base.Domain.Model.Base;
using Core3Base.Domain.Services.Impl.Helper;
using Core3Base.Domain.Services.Services;
using Core3Base.Domain.Validations;
using Core3Base.Infra.Data.Entity;
using Core3Base.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Core3Base.Domain.Services.Impl.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepository<Teacher> teacherRepository;

        public TeacherService(IRepository<Teacher> teacherRepository)
        {
            this.teacherRepository = teacherRepository;

        }
        public ServiceResponse<Teacher> Add(Teacher teacher)
        {
            var response = new ServiceResponse<Teacher>();
            if (response.Validation(new TeacherValidation().Validate(teacher)))
            {
                response.Result = teacherRepository.Insert(teacher);
            }
            return response;
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = teacherRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                teacherRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;
        }

        public ServiceResponse<DataTablesModel.DataTableReturnModel> GetAllForDatatables(DataTablesModel.DataTableAjaxPostModel model)
        {
            var searchBy = model.search?.value;
            var take = model.length;
            var skip = model.start;

            var sortBy = "Id";
            var sortDir = "desc";
            if (model.order != null)
            {
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower();
            }

            var response = new ServiceResponse<DataTablesModel.DataTableReturnModel>();

            var repoResponse = teacherRepository.AllListQueryable(r => !r.IsDelete).Select(x => new
            {
                Id = x.Id,
                IsActive = x.IsActive,
                Name = x.Name,
                Surname=x.SurName,
                Email=x.Email
            });

            var totalResultsCount = repoResponse.Count();
            var filteredResultsCount = repoResponse.Count();

            if (!string.IsNullOrEmpty(searchBy))
            {
                repoResponse = repoResponse.Where(r => r.Name.Contains(searchBy));
                filteredResultsCount = repoResponse.Count();
            }
            repoResponse = repoResponse.OrderBy($"{sortBy} {sortDir}").Skip(skip).Take(take);

            if (repoResponse != null)
            {
                response.Result = new DataTablesModel.DataTableReturnModel
                {
                    draw = model.draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    data = repoResponse.ToList()
                };

            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }

            return response;
        }

        public ServiceResponse<Teacher> GetTeacherById(int id)
        {
            var response = new ServiceResponse<Teacher>();

            response.IsSucceeded = true;
            response.Result = teacherRepository.GetById(id);

            return response;
        }

        public ServiceResponse<List<Teacher>> GetTeachers(TeacherFilterModel filter)
        {
            var response = new ServiceResponse<List<Teacher>>();

            response.IsSucceeded = true;

            response.RecordsTotal = teacherRepository.ListQueryable.Count();
            response.RecordsFiltered = teacherRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = teacherRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;
        }

        public ServiceResponse<Teacher> Update(Teacher teacher)
        {
            var response = new ServiceResponse<Teacher>();
            if (response.Validation(new TeacherValidation().Validate(teacher)))
            {
                teacherRepository.Detach(teacher);

                var repositoryResponse = teacherRepository.GetById(teacher.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.Name = teacher.Name;
                    repositoryResponse.SurName = teacher.SurName;
                    repositoryResponse.Email = teacher.Email;
                    repositoryResponse.LessonId = teacher.LessonId;
                    repositoryResponse.IsActive = teacher.IsActive;

                    response.Result = teacherRepository.Update(repositoryResponse);
                }
                else
                {
                    response.SetError("Veri Bulunamadı");
                }
            }

            return response;
        }
    }
}
