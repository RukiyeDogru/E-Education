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
using System.Text;

namespace Core3Base.Domain.Services.Impl.Services
{
    public class ClasssService : IClasssService
    {
        private readonly IRepository<Classs> classsRepository;

        public ClasssService(IRepository<Classs> classsRepository)
        {
            this.classsRepository =  classsRepository;
        }

        public ServiceResponse<Classs> Add(Classs classs)
        {
            var response = new ServiceResponse<Classs>();
            if (response.Validation(new ClasssValidation().Validate(classs)))
            {
                response.Result = classsRepository.Insert(classs);
            }

            return response;
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = classsRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                classsRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;
        }

        public ServiceResponse<List<Classs>> GetClasss(ClasssFilterModel filter)
        {
            var response = new ServiceResponse<List<Classs>>();
            response.IsSucceeded = true;
            response.RecordsTotal = classsRepository.ListQueryable.Count();
            response.RecordsFiltered = classsRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = classsRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;

        }
        public ServiceResponse<List<Classs>> GetAllActiveClasss()
        {
            var response = new ServiceResponse<List<Classs>>();
            response.IsSucceeded = true;
            response.RecordsTotal = classsRepository.ListQueryable.Count();
            response.RecordsFiltered = classsRepository.ListQueryable.AddSearchFilters(new ClasssFilterModel
            {
                Active = true,
                Deleted = false
            }).Count();
            response.Result = classsRepository.ListQueryable.AddSearchFilters(new ClasssFilterModel
            {
                Active = true,
                Deleted = false
            }).AddOrderAndPageFilters(new ClasssFilterModel
            {
                Active = true,
                Deleted = false
            }).ToList();
            return response;

        }
        public ServiceResponse<Classs> GetClasssById(int id)
        {
            var response = new ServiceResponse<Classs>();

            response.IsSucceeded = true;
            response.Result = classsRepository.GetById(id);

            return response;

        }

        public ServiceResponse<Classs> Update(Classs classs)
        {
            var response = new ServiceResponse<Classs>();
            if (response.Validation(new ClasssValidation().Validate(classs)))
            {
                classsRepository.Detach(classs);

                var repositoryResponse = classsRepository.GetById(classs.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.ClassName =classs.ClassName;

                    repositoryResponse.IsActive = classs.IsActive;
                    response.Result = classsRepository.Update(repositoryResponse);
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

            var repoResponse = classsRepository.AllListQueryable(r => !r.IsDelete).Select(x => new
            {
                Id = x.Id,
                ClassName=x.ClassName,

            });

            var totalResultsCount = repoResponse.Count();
            var filteredResultsCount = repoResponse.Count();

            if (!string.IsNullOrEmpty(searchBy))
            {
                repoResponse = repoResponse.Where(r => r.ClassName.Contains(searchBy));
                filteredResultsCount = repoResponse.Count();
            }
            //repoResponse = repoResponse.OrderBy($"{sortBy} {sortDir}").Skip(skip).Take(take);

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
    }
}
