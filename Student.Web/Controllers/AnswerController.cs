using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core3Base.Domain.Model;
using Core3Base.Domain.Services.Services;
using Core3Base.Infra.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Student.Web.Models;

namespace Student.Web.Controllers
{
    [Route("cevap")]

    public class AnswerController : Controller
    {
        private IAnswerService _answerService;
        private IQuestionService QuestionService;

        public AnswerController(IAnswerService answerService, IQuestionService questionService)
        {
            _answerService = answerService;
            QuestionService = questionService;
        }
        [Route("liste")]
        [HttpGet]
        public ActionResult AnswerList()
        {
            var model = new AnswerModel
            {
                Answers = new List<Core3Base.Infra.Data.Entity.Answer>()
            };
            return View(model);
        }


        [Route("answer-query")]
        [HttpPost]
        public JsonResult AnswerListQuery(DataTablesModel.DataTableAjaxPostModel model)
        {
            try
            {
                var data = _answerService.GetAllForDatatables(model);
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
        public ActionResult AnswerCreate()
        {
            return RedirectToAction("AnswerEdit", new {AnswerId = 0 });
        }


        [Route("duzenle/{AnswerId}")]
        [HttpGet]
        public ActionResult AnswerEdit(int AnswerId)
        {
            try
            {

                var model = new AnswerModel
                {
                    Answer = AnswerId == 0 ? new Answer
                    {
                        IsActive = true,
                        Id = 0
                    } : _answerService.GetAnswerById(AnswerId).Result,
                      QuestionGroup = QuestionService.GetAllActiveQuestion().Result
                };
                return View(model);
            }
            catch (Exception e)
            {

                return View(new AnswerModel());
            }
        }


        [Route("duzenle/{AnswerId}")]
        [HttpPost]
        public JsonResult AnswerEdit(int AnswerId, AnswerModel AnswerModel)
        {
            try
            {
                if (AnswerId == 0)
                {
                    Core3Base.Infra.Data.Entity.Answer Answer = new Core3Base.Infra.Data.Entity.Answer
                    {
                        Responce=AnswerModel.Answer.Responce,
                        IsActive = AnswerModel.Answer.IsActive,
                    };


                    return Json(_answerService.Add(Answer).HttpGetResponse());
                }
                else
                {
                    var Answer = _answerService.GetAnswerById(AnswerId).Result;
                    Answer.Responce = AnswerModel.Answer.Responce;
                    Answer.IsActive = AnswerModel.Answer.IsActive;

                    return Json(_answerService.Update(Answer).HttpGetResponse());


                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }



        [HttpGet]
        public bool AnswerActiveChange(int id, bool active)
        {
            try
            {
                string sonuc = "";
                var AnswerResponse = _answerService.GetAnswerById(id);
                if (AnswerResponse.IsSucceeded)
                {
                    var Answer = AnswerResponse.Result;
                    Answer.IsActive = active;
                    if (active)
                    {
                        sonuc = "aktif";
                    }
                    else
                    {
                        sonuc = "pasif";
                    }

                    _answerService.Update(Answer);
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
                        _answerService.Delete(Convert.ToInt32(silinecekler[i]));
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
        public bool AnswerDelete(int id)
        {
            try
            {
                _answerService.Delete(id);
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

