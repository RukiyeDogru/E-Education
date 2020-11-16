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
        private IClasssService _classsService;

        public StudentController(IStudentService studentService, IClasssService classsService)
        {
            _studentService = studentService;
            _classsService = classsService;
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
                if (data.IsSucceeded)
                {
                    return Json(data.Result);
                }
                return Json(data.ErrorMessage);
            }
            catch (Exception e)
            {

                return Json("");
            }
        }

        [Route("ekle")]
        [HttpGet]
        public ActionResult StudentCreate()
        {
            return RedirectToAction("StudentEdit", new { StudentId = 0 });
        }


        [Route("duzenle/{StudentId}")]
        [HttpGet]
        public ActionResult StudentEdit(int StudentId)
        {
            try
            {

                var model = new StudentModel
                {
                    Student = StudentId == 0 ? new Core3Base.Infra.Data.Entity.Student
                    {
                        IsActive = true,
                        Id = 0
                    } : _studentService.GetStudentById(StudentId).Result,
                    ClasGroup = _classsService.GetAllActiveClasss().Result
                };
                return View(model);
            }
            catch (Exception e)
            {

                return View(new StudentModel());
            }
        }


        [Route("duzenle/{StudentId}")]
        [HttpPost]
        public JsonResult StudentEdit(int StudentId, StudentModel StudentModel)
        {
            try
            {
                if (StudentId == 0)
                {
                    Core3Base.Infra.Data.Entity.Student Student = new Core3Base.Infra.Data.Entity.Student
                    {
                        Name = StudentModel.Student.Name,
                        Email = StudentModel.Student.Email,
                        ClassId = StudentModel.Student.ClassId,
                        IsActive = StudentModel.Student.IsActive,
                        Surname = StudentModel.Student.Surname
                    };
                     

                    return Json(_studentService.Add(Student).HttpGetResponse());
                }
                else
                {
                    var Student = _studentService.GetStudentById(StudentId).Result;
                    Student.Name = StudentModel.Student.Name;
                    Student.Email = StudentModel.Student.Email;
                    Student.ClassId = StudentModel.Student.ClassId;
                    Student.IsActive = StudentModel.Student.IsActive;
                    Student.Surname = StudentModel.Student.Surname;
                    Student.DateModified = DateTime.Now;
                     
                    return  Json(_studentService.Update(Student).HttpGetResponse());
                  

                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }



        [HttpGet]
        public bool StudentActiveChange(int id, bool active)
        {
            try
            {
                string sonuc = "";
                var StudentResponse = _studentService.GetStudentById(id);
                if (StudentResponse.IsSucceeded)
                {
                    var Student = StudentResponse.Result;
                    Student.IsActive = active;
                    if (active)
                    {
                        sonuc = "aktif";
                    }
                    else
                    {
                        sonuc = "pasif";
                    }

                   _studentService.Update(Student);
                }

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        #region Delete

        [Route("secilmislerisil/{ids}")]
        [HttpGet]
        public bool SelectedDelete(string ids)
        {
            try
            {
                if (!string.IsNullOrEmpty(ids))
                {
                    if (ids.StartsWith(","))
                    {
                        ids = ids.Remove(0, 1);
                    }
                    if (ids.EndsWith(","))
                    {
                        ids = ids.Remove(ids.Length - 1, 1);
                    }
                    var silinecekler = ids.Split(',');
                    for (int i = 0; i < silinecekler.Length; i++)
                    {
                        _studentService.Delete(Convert.ToInt32(silinecekler[i]));
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }



        [Route("sil/{id}")]
        [HttpGet]
        public bool StudentDelete(int id)
        {
            try
            {
               _studentService.Delete(id);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
