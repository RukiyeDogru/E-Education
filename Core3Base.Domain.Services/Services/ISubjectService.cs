using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
    public interface ISubjectService
    {
        ServiceResponse<Subjects> Add(Subjects Subjects);
        ServiceResponse<Subjects> Update(Subjects Subjects);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<Subjects> GetSubjectById(int id);
        ServiceResponse<List<Subjects>> GetSubjects(SubjectFilterModel filter);

    }
}
