using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core3Base.Domain.Filters;
using Core3Base.Domain.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Student.Web.Controllers
{
    public class StudentController : Controller
    {
        private IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            StudentFilterModel studentFilterModel = new StudentFilterModel();
            studentFilterModel.Term = "";
            studentFilterModel.OrderByDescending = "DateCreated";
            var StudentList = _studentService.GetStudents(studentFilterModel);
            ViewBag.StudentList = StudentList.Result.Take(9);
            return View();
        }
    }
}
