using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core3Base.Api.Mobile.Model;
using Core3Base.Domain.Filters;
using Core3Base.Domain.Services.Services;
using Core3Base.Infra.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Core3Base.Api.Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasssController : ControllerBase
    {
        private IClasssService _ClasssService;

        public ClasssController(IClasssService ClasssService)
        {
            _ClasssService = ClasssService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(Classs))]
        public IActionResult Get(int id)
        {
            var ClasssResponse = _ClasssService.GetClasssById(id);

            if (ClasssResponse.IsSucceeded && ClasssResponse.Result != null)
            {
                return Ok(ClasssResponse.Result);
            }
            return ClasssResponse.HttpGetResponse();

        }

        [HttpPost("add-Classs")]
        [SwaggerResponse(200, "", typeof(Classs))]
        public IActionResult Add([FromQuery] ClasssViewModel model)
        {
            var ClasssResponse = _ClasssService.Add(new Classs
            {
                ClassName = model.ClassName
               
            }) ;

            if (ClasssResponse.IsSucceeded && ClasssResponse.Result != null)
            {

                return Ok(ClasssResponse.Result);

            }
            return ClasssResponse.HttpGetResponse();

        }
        [HttpPost("edit-Classs/{id}")]
        [SwaggerResponse(200, "", typeof(Classs))]
        public IActionResult Edit(int id, ClasssViewModel model)
        {
            var getClasss = _ClasssService.GetClasssById(id);

            if (getClasss != null && getClasss.IsSucceeded)

            {
                getClasss.Result.ClassName = model.ClassName;

                var updateResult = _ClasssService.Update(getClasss.Result);

                if (updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                return updateResult.HttpGetResponse();

            }
            return getClasss.HttpGetResponse();
        }


        [HttpPost("delete/{id}")]
        [SwaggerResponse(200, "", typeof(Classs))]
        public IActionResult DeleteClasss(int id)
        {
            try
            {
                var result = _ClasssService.Delete(id);

                if (result.IsSucceeded)
                {
                    return Ok(result.Result);
                }
                return BadRequest(result.HttpGetResponse());
                //return result.HttpGetResponse();
            }
            catch (Exception e)

            {

                return BadRequest(e.Message);
            }

        }
        [HttpPost]
        [SwaggerResponse(200, "", typeof(List<Classs>))]
        public IActionResult GetClassss(ClasssFilterModel model)
        {
            try

            {
                var result = _ClasssService.GetClasss(model);
                if (result.IsSucceeded)
                {
                    return Ok(result.Result);

                }
                return BadRequest(result.ErrorMessage);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
