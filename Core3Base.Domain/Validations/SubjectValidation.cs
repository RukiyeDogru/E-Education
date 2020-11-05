using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
    public class SubjectValidation : AbstractValidator<Subjects>
    {
        public SubjectValidation()
        {
            RuleFor(x => x.LessonId).NotEmpty().WithMessage("DersId boş olamaz.");

        }
    }
}
