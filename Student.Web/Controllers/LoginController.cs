using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core3Base.Domain.Helper;
using Core3Base.Domain.Model;
using Core3Base.Domain.Model.Base;
using Core3Base.Domain.Services.Services;
using Core3Base.Infra.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
namespace Student.Web.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {

        private readonly IUserService _userService;

        private readonly ILogger<LoginController> _logger;

        public LoginController(IUserService userService, ILogger<LoginController> logger)
        {
            _userService = userService;
        
            _logger = logger;

        }
     

        [Route("giris")]
       public IActionResult Login()
        {

            return View();

        }


        [HttpPost]
        [Route("giris")]
        public IActionResult Login(LoginViewModel model)
        {
            var response = new ServiceResponse<User>();
            response = _userService.GetByUserName(model.Email);
            if (response.Result.LoginFailed >= 5)
            {
                response.Result.LoginFailed = response.Result.LoginFailed + 1;
                response.Result.IsActive = false;
                _userService.Update(response.Result);
                model.HasError = true;
                model.ErrorMessage = Messages.LoginLimitFullMessage;
            }
            else if (response.IsSucceeded)
            {
                var getUser = _userService.Get(model.Email, model.Password);
                if (getUser.Result == null)
                {
                    response.IsSucceeded = false;
                }
                if (response.IsSucceeded)
                {
                    model.HasError = false;
                    model.ErrorMessage = Messages.SuccessfulLogin;
                    response.Result.LastLoginDate = DateTime.Now; ;
                    _userService.Update(response.Result);
                }
                else
                {
                    response.Result.LoginFailed = response.Result.LoginFailed + 1;
                    _userService.Update(response.Result);
                    model.HasError = true;
                    model.ErrorMessage = Messages.LoginLimitErrorMessage;

                }
            }


            return Json(model);
        }



    }
}
