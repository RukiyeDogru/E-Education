using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Core3Base.Domain.Services.Impl.Helper
{
    public static class LogHelper
    {
        public static void Info<T>(ILogger<T> _logger, LogHelperModel model)
        {
            try
            {

                _logger.LogInformation(model.Message);
                //var config = new Dictionary<string, object>();
                //config.Add("DateCreated", DateTime.Now.ToString("O"));
                //config.Add("DateModified", DateTime.Now.ToString("O"));

                //config.Add("AcenteNo", model.UserNo);
                //config.Add("ActionTaken", model.ActionTaken);
                //config.Add("UserName", model.UserName);

                //using (_logger.BeginScope(config))
                //{
                //    _logger.LogInformation(model.Message);
                //}
            }
            catch (Exception e)
            {

            }
        }
    }
    public class LogHelperModel
    {
        public string UserNo { get; set; }
        public string ActionTaken { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}
