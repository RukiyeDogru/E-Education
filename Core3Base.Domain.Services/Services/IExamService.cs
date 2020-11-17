using Core3Base.Domain.Filters;
using Core3Base.Domain.Model;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
   public interface IExamService
    {
        ServiceResponse<Exam> Add(Exam Exam);
        ServiceResponse<Exam> Update(Exam Exam);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<Exam> GetExamById(int id);
        ServiceResponse<List<Exam>> GetExams(ExamFilterModel filter);
        ServiceResponse<DataTablesModel.DataTableReturnModel> GetAllForDatatables(
            DataTablesModel.DataTableAjaxPostModel model);


    }
}
