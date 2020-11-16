using Core3Base.Domain.Filters;
using Core3Base.Domain.Model;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
   public interface ITeacherService
    {
        ServiceResponse<Teacher> Add(Teacher teacher);
        ServiceResponse<Teacher> Update(Teacher teacher);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<Teacher> GetTeacherById(int id);
        ServiceResponse<List<Teacher>> GetTeachers(TeacherFilterModel filter);
        ServiceResponse<DataTablesModel.DataTableReturnModel> GetAllForDatatables(
          DataTablesModel.DataTableAjaxPostModel model);
    }
}
