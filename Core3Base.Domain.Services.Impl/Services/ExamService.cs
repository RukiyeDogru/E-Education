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
