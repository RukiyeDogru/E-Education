using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
    public class StudentValidation : AbstractValidator<Student>
    {
        public StudentValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad boş olamaz.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad boş olamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş olamaz.");
        }
    }
}
