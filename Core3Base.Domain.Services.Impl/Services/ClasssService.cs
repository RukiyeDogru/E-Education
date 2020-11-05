﻿using Core3Base.Domain.Filters;
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

                    response.Result = classsRepository.Update(repositoryResponse);
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
