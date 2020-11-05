
using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
    public class LessonStudentValidation : AbstractValidator<LessonStudent>
    {
        public LessonStudentValidation()
        {
            RuleFor(x => x.LessonId).NotEmpty().WithMessage("DersID boş olamaz.");

            RuleFor(x => x.StudentId).NotEmpty().WithMessage("ÖğrenciID boş olamaz.");

        }
    }
}
