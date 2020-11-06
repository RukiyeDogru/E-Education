using Core3Base.Domain.Filters;
using Core3Base.Domain.Helper;
using Core3Base.Domain.Model.Base;
using Core3Base.Domain.Services.Impl.Helper;
using Core3Base.Domain.Services.Services;
using Core3Base.Domain.Validations;
using Core3Base.Infra.Data.Entity;
using Core3Base.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Core3Base.Domain.Services.Impl.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public ServiceResponse<User> Add(User user)
        {
            var response = new ServiceResponse<User>();
            byte[] passwordHash, passwordSalt;

            if (response.Validation(new UserValidation().Validate(user)))
            {
                HashingHelper.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
                var userAdd = new User
                {
                    EMail = user.EMail,
                    Name = user.Name,
                    LastName = user.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt

                };
                response.Result = userRepository.Insert(userAdd);
            }

            return response;
        }
        public ServiceResponse<User> Update(User user)
        {
            var response = new ServiceResponse<User>();
            if (response.Validation(new UserValidation().Validate(user)))
            {
                userRepository.Detach(user);

                var repositoryResponse = userRepository.GetById(user.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.EMail = user.EMail;
                    repositoryResponse.UserName = user.UserName;
                    repositoryResponse.Name = user.Name;
                    repositoryResponse.LastName = user.LastName;
                    repositoryResponse.Phone = user.Phone;
                    repositoryResponse.LastLoginDate = user.LastLoginDate;
                    repositoryResponse.LoginFailed = user.LoginFailed;
                    repositoryResponse.IsActive = user.IsActive;
                    repositoryResponse.IsDelete = user.IsDelete;
                    response.Result = userRepository.Update(repositoryResponse);
                }
                else
                {
                    response.SetError("Veri Bulunamadı");
                }


            }

            return response;
        }
        public ServiceResponse<User> GetByMail(string email)
        {
            var response = new ServiceResponse<User>();

            response.IsSucceeded = true;
            response.Result = userRepository.Get(x => x.EMail == email);

            return response;
        }
        public ServiceResponse<User> GetByUserName(string userName)
        {
            var response = new ServiceResponse<User>();

            response.IsSucceeded = true;
            response.Result = userRepository.Get(x => x.UserName == userName);

            return response;
        }

        public ServiceResponse<User> GetId(int userId)
        {
            var response = new ServiceResponse<User>();

            response.IsSucceeded = true;
            response.Result = userRepository.GetById(userId);

            return response;
        }
        public ServiceResponse<User> Get(string userName, string password)
        {
            var response = new ServiceResponse<User>();
            var userToCheck = userRepository.Get(x => x.UserName == userName);
            if (HashingHelper.VerifyPasswordHash(password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                response.IsSucceeded = true;
                response.ErrorMessage = Messages.SuccessfulLogin;
                response.Result = userToCheck;
            }
            else
            {
                response.IsSucceeded = false;
                response.HasError = true;
                response.ErrorMessage = Messages.SuccessfulLoginFailed;

            }
            return response;
        }

        public ServiceResponse<User> GetByMailConfirmationCode(string MailConfirmationCode)
        {
            var response = new ServiceResponse<User>();

            response.IsSucceeded = true;
            response.Result = userRepository.Get(p => p.MailConfirmationCode == MailConfirmationCode);

            return response;
        }

        public ServiceResponse<User> GetByChangePasswordCode(string ChangePasswordCode)
        {

            var response = new ServiceResponse<User>();

            response.IsSucceeded = true;
            response.Result = userRepository.Get(p => p.ChangePasswordCode == ChangePasswordCode && p.IsDelete == false && p.IsActive == true);

            return response;
        }


        public ServiceResponse<List<User>> GetListByUserName(string name)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<List<User>> GetAllOrderByName(UserFilterModel filter)
        {
            var response = new ServiceResponse<List<User>>();
            response.IsSucceeded = true;
            response.RecordsTotal = userRepository.ListQueryable.Count();
            response.RecordsFiltered = userRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = userRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;
        }

        public ServiceResponse<List<User>> GetAllOrderByDescendingLastLoginDate(UserFilterModel filter)
        {
            var response = new ServiceResponse<List<User>>();
            response.IsSucceeded = true;
            response.RecordsTotal = userRepository.ListQueryable.Count();
            response.RecordsFiltered = userRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = userRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;
        }

        public ServiceResponse<int> GetAllCount()
        {
            var response = new ServiceResponse<int>();
            response.IsSucceeded = true;
            response.RecordsTotal = userRepository.ListQueryable.Count();
            return response;
        }

        public ServiceResponse<List<User>> GetAll()
        {
            var response = new ServiceResponse<List<User>>();
            response.IsSucceeded = true;
            response.RecordsTotal = userRepository.ListQueryable.Count();
            response.RecordsFiltered = userRepository.ListQueryable.Count();
            response.Result = userRepository.ListQueryable.ToList();
            return response;
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = userRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                userRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;
        }


        public ServiceResponse<bool> MailConfirmation(string code)
        {
            var response = new ServiceResponse<bool>();


            var repositoryResponse = userRepository.Get(r => r.MailConfirmationCode == code);
            if (repositoryResponse != null)
            {
                response.IsSucceeded = true;
                response.ErrorMessage = Messages.MailConfirmation;
                repositoryResponse.MailConfirmation = true;
                userRepository.Update(repositoryResponse);
            }
            else
            {
                response.SetError(Messages.MailConfirmationFailed);
            }
            return response;
        }

        public ServiceResponse<bool> changePasswordCode(string email, string code, string password)
        {
            byte[] passwordHash, passwordSalt;
            var response = new ServiceResponse<bool>();
            var changePassword = userRepository.Get(r => r.ChangePasswordCode == code && r.EMail == email && r.IsActive == true && r.IsDelete == false);
            if (changePassword != null)
            {
                HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                changePassword.PasswordHash = passwordHash;
                changePassword.PasswordSalt = passwordSalt;
                changePassword.ChangePasswordCode = Guid.NewGuid().ToString().Replace("-", "").ToLower();
                userRepository.Update(changePassword);
                return response;
            }
            return response;

        }

    }
}
