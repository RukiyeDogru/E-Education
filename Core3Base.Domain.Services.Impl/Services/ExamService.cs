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
    public class ExamService : IExamService
    {
        private readonly IRepository<Exam> ExamRepository;

        public ExamService(IRepository<Exam> ExamRepository)
        {
            this.ExamRepository = ExamRepository;
        }

        public ServiceResponse<Exam> Add(Exam Exam)
        {
            var response = new ServiceResponse<Exam>();
            if (response.Validation(new ExamValidation().Validate(Exam)))
            {
                response.Result = ExamRepository.Insert(Exam);
            }
            return response;
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = ExamRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                ExamRepository.Delete(repoResponse);
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

            var repoResponse = ExamRepository.AllListQueryable(r => !r.IsDelete).Select(x => new
            {
                Id = x.Id,
                IsActive = x.IsActive,
                ExamName = x.ExamName,
            });

            var totalResultsCount = repoResponse.Count();
            var filteredResultsCount = repoResponse.Count();

            if (!string.IsNullOrEmpty(searchBy))
            {
                repoResponse = repoResponse.Where(r => r.ExamName.Contains(searchBy));
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

        public ServiceResponse<Exam> GetExamById(int id)
        {
            var response = new ServiceResponse<Exam>();

            response.IsSucceeded = true;
            response.Result = ExamRepository.GetById(id);
            return response;

        }

        public ServiceResponse<List<Exam>> GetExams(ExamFilterModel filter)
        {
            var response = new ServiceResponse<List<Exam>>();
            response.IsSucceeded = true;
            response.RecordsTotal = ExamRepository.ListQueryable.Count();
            response.RecordsFiltered = ExamRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = ExamRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;
        }

        public ServiceResponse<Exam> Update(Exam Exam)
        {

            var response = new ServiceResponse<Exam>();
            if (response.Validation(new ExamValidation().Validate(Exam)))
            {
                ExamRepository.Detach(Exam);

                var repositoryResponse = ExamRepository.GetById(Exam.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.ExamName=Exam.ExamName;
                    repositoryResponse.LessonId = Exam.LessonId;

                    response.Result = ExamRepository.Update(repositoryResponse);
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
