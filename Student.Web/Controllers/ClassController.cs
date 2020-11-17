using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core3Base.Domain.Model;
using Core3Base.Domain.Services.Services;
using Core3Base.Infra.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Student.Web.Models;

namespace Class.Web.Controllers
{
    [Route("sınıf")]

    public class ClassController : Controller
    {
        private IClasssService _classService;

        public ClassController(IClasssService classsService)
        {
            _classService = classsService;

        }



        [Route("liste")]
        [HttpGet]
        public ActionResult ClassList()
        {
            var model = new ClassModel
            {
                Classss = new List<Core3Base.Infra.Data.Entity.Classs>()
            };
            return View(model);
        }
        [Route("sınıf-query")]
        [HttpPost]
        public JsonResult ClassListQuery(DataTablesModel.DataTableAjaxPostModel model)
        {
            try
            {
                var data =_classService.GetAllForDatatables(model);
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
        public ActionResult ClassCreate()
        {
            return RedirectToAction("ClassEdit", new {ClassId = 0 });
        }

        [Route("duzenle/{ClassId}")]
        [HttpGet]
        public ActionResult ClassEdit(int ClassId)
        {
            try
            {

                var model = new ClassModel
                {
                    Classs = ClassId == 0 ? new Core3Base.Infra.Data.Entity.Classs
                    {
                        IsActive = true,
                        Id = 0
                    } : _classService.GetClasssById(ClassId).Result,
                };
                return View(model);
            }
            catch (Exception e)
            {
                return View(new StudentModel());
            }
        }

        [Route("duzenle/{ClassId}")]
        [HttpPost]
        public JsonResult ClassEdit(int ClassId, ClassModel ClassModel)
        {
            try
            {
                if (ClassId == 0)
                {
                    Core3Base.Infra.Data.Entity.Classs classs = new Core3Base.Infra.Data.Entity.Classs
                    {
                        ClassName = ClassModel.Classs.ClassName,
                        IsActive = ClassModel.Classs.IsActive,
                    };


                    return Json(_classService.Add(classs).HttpGetResponse());
                }
                else
                {
                    var Class = _classService.GetClasssById(ClassId).Result;
                    Class.IsActive = ClassModel.Classs.IsActive;
                    Class.ClassName = ClassModel.Classs.ClassName;

                    return Json(_classService.Update(Class).HttpGetResponse());


                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet]
        public bool ClassActiveChange(int id, bool active)
        {
            try
            {
                string sonuc = "";
                var ClassResponse = _classService.GetClasssById(id);
                if (ClassResponse.IsSucceeded)
                {
                    var classs = ClassResponse.Result;
                    classs.IsActive = active;
                    if (active)
                    {
                        sonuc = "aktif";
                    }
                    else
                    {
                        sonuc = "pasif";
                    }

                    _classService.Update(classs);
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
                        _classService.Delete(Convert.ToInt32(silinecekler[i]));
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
        public bool ClasssDelete(int id)
        {
            try
            {
                _classService.Delete(id);
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
