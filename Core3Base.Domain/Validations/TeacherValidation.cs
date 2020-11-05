using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
    public class TeacherValidation : AbstractValidator<Teacher>
    {
        public TeacherValidation()
        {
            RuleFor(x => x.LessonId).NotEmpty().WithMessage("Ders boş olamaz.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ders boş olamaz.");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("Ders boş olamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Ders boş olamaz.");
        }
    }
}
