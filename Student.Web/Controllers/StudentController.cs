using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return View();
        }
    }
}
