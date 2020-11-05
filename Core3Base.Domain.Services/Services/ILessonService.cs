using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
    public interface ILessonService
    {
        ServiceResponse<Lesson> Add(Lesson lesson);
        ServiceResponse<Lesson> Update(Lesson lesson);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<Lesson> GetLessonById(int id);
        ServiceResponse<List<Lesson>> GetLessons(LessonFilterModel filter);

    }
}
