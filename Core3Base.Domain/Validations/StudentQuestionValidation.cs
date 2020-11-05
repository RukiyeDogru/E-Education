using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
    public class StudentQuestionValidation : AbstractValidator<StudentQuestionAnswer>
    {

        public StudentQuestionValidation()
        {
            RuleFor(x => x.QuestionId).NotEmpty().WithMessage("QuestionId boş olamaz.");
            RuleFor(x => x.StudentId).NotEmpty().WithMessage("StudentId boş olamaz.");

        }


    }
}
