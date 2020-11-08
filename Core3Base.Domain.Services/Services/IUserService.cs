using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
   public interface IUserService
   {
        ServiceResponse<User> Add(User user);
        ServiceResponse<User> GetByMail(string email);
        ServiceResponse<User> GetByUserName(string userName);
        ServiceResponse<User> GetId(int userId);
        ServiceResponse<User> Get(string userName, string password);
        ServiceResponse<User> GetByMailConfirmationCode(string MailConfirmationCode);
        ServiceResponse<User> GetByChangePasswordCode(string ChangePasswordCode);
        ServiceResponse<List<User>> GetListByUserName(string name);
        ServiceResponse<List<User>> GetAllOrderByName(UserFilterModel filter);
        ServiceResponse<List<User>> GetAllOrderByDescendingLastLoginDate(UserFilterModel filter);
        ServiceResponse<int> GetAllCount();
        ServiceResponse<List<User>> GetAll();
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<User> Update(User user);
        //AccessToken CreateAccessToken(User user);
        ServiceResponse<bool> MailConfirmation(string code);
        ServiceResponse<bool> changePasswordCode(string email, string code, string password);

    }
}
