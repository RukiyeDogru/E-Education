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
    public class LessonService : ILessonService
    {
        private readonly IRepository<Lesson> lessonRepository;

        public LessonService(IRepository<Lesson> lessonRepository)
        {
            this.lessonRepository = lessonRepository;

        }

        public ServiceResponse<Lesson> Add(Lesson lesson)
        {
            var response = new ServiceResponse<Lesson>();
            if (response.Validation(new LessonValidation().Validate(lesson)))
            {
                response.Result = lessonRepository.Insert(lesson);
            }

            return response;

        }

        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = lessonRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                lessonRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;

        }

        public ServiceResponse<List<Lesson>> GetAllActiveLesson()
        {
            var response = new ServiceResponse<List<Lesson>>();
            response.IsSucceeded = true;
            response.RecordsTotal = lessonRepository.ListQueryable.Count();
            response.RecordsFiltered = lessonRepository.ListQueryable.AddSearchFilters(new LessonFilterModel
            {
                Active = true,
                Deleted = false
            }).Count();
            response.Result = lessonRepository.ListQueryable.AddSearchFilters(new LessonFilterModel
            {
                Active = true,
                Deleted = false
            }).AddOrderAndPageFilters(new LessonFilterModel
            {
                Active = true,
                Deleted = false
            }).ToList();
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

            var repoResponse = lessonRepository.AllListQueryable(r => !r.IsDelete).Select(x => new
            {
                Id = x.Id,
                IsActive = x.IsActive,
                LessonName=x.LessonName

            });

            var totalResultsCount = repoResponse.Count();
            var filteredResultsCount = repoResponse.Count();

            if (!string.IsNullOrEmpty(searchBy))
            {
                repoResponse = repoResponse.Where(r => r.LessonName.Contains(searchBy));
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

        public ServiceResponse<Lesson> GetLessonById(int id)
        {
            var response = new ServiceResponse<Lesson>();

            response.IsSucceeded = true;
            response.Result = lessonRepository.GetById(id);

            return response;

        }

        public ServiceResponse<List<Lesson>> GetLessons(LessonFilterModel filter)
        {
            var response = new ServiceResponse<List<Lesson>>();
            response.IsSucceeded = true;

            response.RecordsTotal = lessonRepository.ListQueryable.Count();
            response.RecordsFiltered = lessonRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = lessonRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;
        }
        public ServiceResponse<Lesson> Update(Lesson lesson)
        {
            var response = new ServiceResponse<Lesson>();
            if (response.Validation(new LessonValidation().Validate(lesson)))
            {
                lessonRepository.Detach(lesson);

                var repositoryResponse = lessonRepository.GetById(lesson.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.LessonName = lesson.LessonName;
                   
                    response.Result = lessonRepository.Update(repositoryResponse);
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
