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
    public class LessonStudentService : ILessonStudentService
    {
        private readonly IRepository<LessonStudent> lessonStudentRepository;

        public LessonStudentService(IRepository<LessonStudent> lessonStudentRepository)
        {
            this.lessonStudentRepository = lessonStudentRepository;

        }

        public ServiceResponse<Infra.Data.Entity.LessonStudent> Add(Infra.Data.Entity.LessonStudent lessonStudent)
        {
            var response = new ServiceResponse<LessonStudent>();
            if (response.Validation(new LessonStudentValidation().Validate(lessonStudent)))
            {
                response.Result = lessonStudentRepository.Insert(lessonStudent);
            }
            return response;
        }

        public ServiceResponse<bool> Delete(int id)
        {

            var response = new ServiceResponse<bool>();
            var repoResponse = lessonStudentRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                lessonStudentRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;
        }


        public ServiceResponse<LessonStudent> GetLessonStudentById(int id)
        {
            var response = new ServiceResponse<LessonStudent>();

            response.IsSucceeded = true;
            response.Result = lessonStudentRepository.GetById(id);

            return response;

        }

        public ServiceResponse<List<LessonStudent>> GetLessonStudents(LessonStudentFilterModel filter)
        {
            var response = new ServiceResponse<List<LessonStudent>>();

            response.IsSucceeded = true;

            response.RecordsTotal = lessonStudentRepository.ListQueryable.Count();
            response.RecordsFiltered = lessonStudentRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = lessonStudentRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;
        }

        public ServiceResponse<Infra.Data.Entity.LessonStudent> Update(Infra.Data.Entity.LessonStudent lessonStudent)
        {
            var response = new ServiceResponse<LessonStudent>();
            if (response.Validation(new LessonStudentValidation().Validate(lessonStudent)))
            {
                lessonStudentRepository.Detach(lessonStudent);

                var repositoryResponse = lessonStudentRepository.GetById(lessonStudent.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.LessonId = lessonStudent.LessonId;
                    repositoryResponse.StudentId = lessonStudent.StudentId;

                    response.Result = lessonStudentRepository.Update(repositoryResponse);
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
