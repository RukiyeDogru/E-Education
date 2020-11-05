using Core3Base.Domain.Filters;
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
                    response.Result = SubjectsRepository.Update(repositoryResponse);
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
