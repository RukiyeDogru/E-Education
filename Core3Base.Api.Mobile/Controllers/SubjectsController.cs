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
    public class SubjectsController : ControllerBase
    {
        private ISubjectService _SubjectsService;

        public SubjectsController(ISubjectService SubjectsService)
        {
            _SubjectsService = SubjectsService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(Subjects))]
        public IActionResult Get(int id)
        {
            var SubjectsResponse = _SubjectsService.GetSubjectById(id);

            if (SubjectsResponse.IsSucceeded && SubjectsResponse.Result != null)
            {
                return Ok(SubjectsResponse.Result);
            }
            return SubjectsResponse.HttpGetResponse();

        }

        [HttpPost("add-Subjects")]
        [SwaggerResponse(200, "", typeof(Subjects))]
        public IActionResult Add([FromQuery] SubjectViewModel model)
        {
            var SubjectsResponse = _SubjectsService.Add(new Subjects
            {

                LessonId = model.LessonId


            });

            if (SubjectsResponse.IsSucceeded && SubjectsResponse.Result != null)
            {

                return Ok(SubjectsResponse.Result);

            }
            return SubjectsResponse.HttpGetResponse();

        }
        [HttpPost("edit-Subjects/{id}")]
        [SwaggerResponse(200, "", typeof(Subjects))]
        public IActionResult Edit(int id, SubjectViewModel model)
        {
            var getSubjects = _SubjectsService.GetSubjectById(id);

            if (getSubjects != null && getSubjects.IsSucceeded)

            {
                getSubjects.Result.LessonId = model.LessonId;

                var updateResult = _SubjectsService.Update(getSubjects.Result);

                if (updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                return updateResult.HttpGetResponse();

            }
            return getSubjects.HttpGetResponse();
        }


        [HttpPost("delete/{id}")]
        [SwaggerResponse(200, "", typeof(Subjects))]
        public IActionResult DeleteSubjects(int id)
        {
            try

            {
                var result = _SubjectsService.Delete(id);

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
        [SwaggerResponse(200, "", typeof(List<Subjects>))]
        public IActionResult GetSubjectss(SubjectFilterModel model)
        {
            try

            {
                var result = _SubjectsService.GetSubjects(model);
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
