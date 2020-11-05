using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
    public interface IClasssService
    {
        ServiceResponse<Classs> Add(Classs classs);
        ServiceResponse<Classs> Update(Classs classs);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<Classs> GetClasssById(int id);
        ServiceResponse<List<Classs>> GetClasss(ClasssFilterModel filter);

    }
}
