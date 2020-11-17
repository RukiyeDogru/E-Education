using Core3Base.Domain.Filters;
using Core3Base.Domain.Model;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
   public interface IAnswerService
    {
        ServiceResponse<Answer> Add(Answer Answer);
        ServiceResponse<Answer> Update(Answer Answer);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<Answer> GetAnswerById(int id);
        ServiceResponse<List<Answer>> GetAnswers(AnswerFilterModel filter);
      
        ServiceResponse<DataTablesModel.DataTableReturnModel> GetAllForDatatables(
          DataTablesModel.DataTableAjaxPostModel model);
    }
}
