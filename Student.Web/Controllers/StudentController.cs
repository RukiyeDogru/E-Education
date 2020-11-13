using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core3Base.Domain.Filters;
using Core3Base.Domain.Model; 
using Core3Base.Domain.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Student.Web.Models;

namespace Student.Web.Controllers
{ 
    [Route("ogrenci")]
    public class StudentController : Controller
    {
        private IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
         
      

        [Route("liste")] 
        [HttpGet]
        public ActionResult StudentList()
        {
            var model = new StudentModel
            {
                Students = new List<Core3Base.Infra.Data.Entity.Student>()
            };
            return View(model);
        }
        [Route("student-query")] 
        [HttpPost]
        public JsonResult StudentListQuery(DataTablesModel.DataTableAjaxPostModel model)
        {
            try
            {

                var data = _studentService.GetAllForDatatables(model);
                return Json(data);
            }
            catch (Exception e)
            {
                 
                return Json("");
            }
        }
    }
}
