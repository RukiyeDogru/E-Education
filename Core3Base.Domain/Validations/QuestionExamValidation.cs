using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
    public class QuestionExamValidation : AbstractValidator<QuestionExam>
    {

        public QuestionExamValidation()
        {
            RuleFor(x => x.ExamId).NotEmpty().WithMessage("ExamId boş olamaz.");
            RuleFor(x => x.QuestionId).NotEmpty().WithMessage("QuestionId boş olamaz");
        }


    }
}
