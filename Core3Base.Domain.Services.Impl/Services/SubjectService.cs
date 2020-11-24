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
    public class SubjectService : ISubjectService
    {
        private readonly IRepository<Subjects> SubjectsRepository;

        public SubjectService(IRepository<Subjects> SubjectsRepository)
        {
            this.SubjectsRepository = SubjectsRepository;
        }


        public ServiceResponse<Subjects> Add(Subjects Subjects)
        {

            var response = new ServiceResponse<Subjects>();
            if (response.Validation(new SubjectValidation().Validate(Subjects)))
            {
                response.Result = SubjectsRepository.Insert(Subjects);
            }

            return response;
        }

        public ServiceResponse<bool> Delete(int id)
        {

            var response = new ServiceResponse<bool>();
            var repoResponse = SubjectsRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                SubjectsRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;
        }

        public ServiceResponse<List<Subjects>> GetSubjects(SubjectFilterModel filter)
        {
            var response = new ServiceResponse<List<Subjects>>();

            response.IsSucceeded = true;

            response.RecordsTotal = SubjectsRepository.ListQueryable.Count();
            response.RecordsFiltered = SubjectsRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = SubjectsRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;

        }

        public ServiceResponse<Subjects> GetSubjectById(int id)
        {

            var response = new ServiceResponse<Subjects>();

            response.IsSucceeded = true;
            response.Result = SubjectsRepository.GetById(id);
            return response;

        }

        public ServiceResponse<Subjects> Update(Subjects Subjects)
        {
            var response = new ServiceResponse<Subjects>();
            if (response.Validation(new SubjectValidation().Validate(Subjects)))
            {
                SubjectsRepository.Detach(Subjects);

                var repositoryResponse = SubjectsRepository.GetById(Subjects.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.LessonId=Subjects.LessonId;
                    repositoryResponse.IsActive = Subjects.IsActive;
                    repositoryResponse.SubjectName = Subjects.SubjectName;
                    response.Result = SubjectsRepository.Update(repositoryResponse);
                }
                else
                {
                    response.SetError("Veri Bulunamadı");
                }
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

            var repoResponse = SubjectsRepository.AllListQueryable(r => !r.IsDelete).Select(x => new
            {
                Id = x.Id,
                IsActive = x.IsActive,
                SubjectName=x.SubjectName,

            });

            var totalResultsCount = repoResponse.Count();
            var filteredResultsCount = repoResponse.Count();

            if (!string.IsNullOrEmpty(searchBy))
            {
                repoResponse = repoResponse.Where(r => r.SubjectName.Contains(searchBy));
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

        public ServiceResponse<List<Subjects>> GetAllActiveSubject()
        {

            var response = new ServiceResponse<List<Subjects>>();
            response.IsSucceeded = true;
            response.RecordsTotal = SubjectsRepository.ListQueryable.Count();
            response.RecordsFiltered = SubjectsRepository.ListQueryable.AddSearchFilters(new SubjectFilterModel
            {
                Active = true,
                Deleted = false
            }).Count();
            response.Result = SubjectsRepository.ListQueryable.AddSearchFilters(new SubjectFilterModel
            {
                Active = true,
                Deleted = false
            }).AddOrderAndPageFilters(new SubjectFilterModel
            {
                Active = true,
                Deleted = false
            }).ToList();
            return response;

        }
    }
}
