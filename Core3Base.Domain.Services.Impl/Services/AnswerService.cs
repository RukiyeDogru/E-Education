using Core3Base.Domain.Filters;
using Core3Base.Domain.Model;
using Core3Base.Domain.Model.Base;
using Core3Base.Domain.Services.Impl.Helper;
using Core3Base.Domain.Services.Services;
using Core3Base.Domain.Validations;
using Core3Base.Infra.Data.Entity;
using Core3Base.Infra.Data.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Core3Base.Domain.Services.Impl.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IRepository<Answer> AnswerRepository;

        public AnswerService(IRepository<Answer> AnswerRepository)
        {
            this.AnswerRepository = AnswerRepository;
        }

        public ServiceResponse<Answer> Add(Answer Answer)
        {
            var response = new ServiceResponse<Answer>();
            if (response.Validation(new AnswerValidation().Validate(Answer)))
            {
                response.Result = AnswerRepository.Insert(Answer);
            }
            return response;
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = AnswerRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                AnswerRepository.Delete(repoResponse);
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

            var repoResponse = AnswerRepository.AllListQueryable(r => !r.IsDelete).Select(x => new
            {
                Id = x.Id,
                IsActive = x.IsActive,
                Responce=x.Responce
            });

            var totalResultsCount = repoResponse.Count();
            var filteredResultsCount = repoResponse.Count();

            if (!string.IsNullOrEmpty(searchBy))
            {
                repoResponse = repoResponse.Where(r => r.Responce.Contains(searchBy));
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

        public ServiceResponse<Answer> GetAnswerById(int id)
        {
            var response = new ServiceResponse<Answer>();

            response.IsSucceeded = true;
            response.Result = AnswerRepository.GetById(id);

            return response;

        }

        public ServiceResponse<List<Answer>> GetAnswers(AnswerFilterModel filter)
        {
            var response = new ServiceResponse<List<Answer>>();
            response.IsSucceeded = true;
            response.RecordsTotal = AnswerRepository.ListQueryable.Count();
            response.RecordsFiltered = AnswerRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = AnswerRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;
        }

        public ServiceResponse<Answer> Update(Answer Answer)
        {

            var response = new ServiceResponse<Answer>();
            if (response.Validation(new AnswerValidation().Validate(Answer)))
            {
                AnswerRepository.Detach(Answer);

                var repositoryResponse = AnswerRepository.GetById(Answer.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.Responce =Answer.Responce;

                    repositoryResponse.IsActive = Answer.IsActive;

                    response.Result = AnswerRepository.Update(repositoryResponse);
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
