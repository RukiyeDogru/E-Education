using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
    public class LessonValidation : AbstractValidator<Lesson>
    {
        public LessonValidation()
        {
            RuleFor(x => x.LessonName).NotEmpty().WithMessage("DersAdı boş olamaz.");
           
        }
    }
}
