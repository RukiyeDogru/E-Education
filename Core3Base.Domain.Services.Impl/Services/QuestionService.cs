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
    public class QuestionService : IQuestionService
    {
        private readonly IRepository<Question> questionRepository;

        public QuestionService(IRepository<Question> questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public ServiceResponse<Question> Add(Question question)
        {

            var response = new ServiceResponse<Question>();
            if (response.Validation(new QuestionValidation().Validate(question)))
            {
                response.Result = questionRepository.Insert(question);
            }

            return response;

        }

        public ServiceResponse<bool> Delete(int id)
        {
            var responce = new ServiceResponse<bool>();

            var repoResponse = questionRepository.GetById(id);

            responce.Result = false;
            if (repoResponse != null)
            {
                questionRepository.Delete(repoResponse);
                responce.Result = true;
            }
            else
            {
                responce.SetError("Kayıt bulunamadı");
            }
            return responce;

        }

        public ServiceResponse<List<Question>> GetAllActiveQuestion()
        {
            var response = new ServiceResponse<List<Question>>();
            response.IsSucceeded = true;
            response.RecordsTotal = questionRepository.ListQueryable.Count();
            response.RecordsFiltered = questionRepository.ListQueryable.AddSearchFilters(new QuestionFilterModel
            {
                Active = true,
                Deleted = false
            }).Count();
            response.Result = questionRepository.ListQueryable.AddSearchFilters(new QuestionFilterModel
            {
                Active = true,
                Deleted = false
            }).AddOrderAndPageFilters(new QuestionFilterModel
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

            var repoResponse = questionRepository.AllListQueryable(r => !r.IsDelete).Select(x => new
            {
                Id = x.Id,
                IsActive = x.IsActive,
                Content= x.Content,
            });

            var totalResultsCount = repoResponse.Count();
            var filteredResultsCount = repoResponse.Count();

            if (!string.IsNullOrEmpty(searchBy))
            {
                repoResponse = repoResponse.Where(r => r.Content.Contains(searchBy));
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

        public ServiceResponse<Question> GetQuestionById(int id)
        {
                var response = new ServiceResponse<Question>();

                response.IsSucceeded = true;
                response.Result = questionRepository.GetById(id);

                return response;

        }

        public ServiceResponse<List<Question>> GetQuestions(QuestionFilterModel filter)
        {

            var response = new ServiceResponse<List<Question>>();
            response.IsSucceeded = true;

            response.RecordsTotal = questionRepository.ListQueryable.Count();
            response.RecordsFiltered = questionRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = questionRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;


        }

        public ServiceResponse<Question> Update(Question Question)
        {
            var response = new ServiceResponse<Question>();
            if (response.Validation(new QuestionValidation().Validate(Question)))
            {
                questionRepository.Detach(Question);

                var repositoryResponse = questionRepository.GetById(Question.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.Content =Question.Content;

                    response.Result = questionRepository.Update(repositoryResponse);
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
