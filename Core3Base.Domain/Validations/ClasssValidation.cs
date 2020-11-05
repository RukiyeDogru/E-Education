using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
    public class ClasssValidation : AbstractValidator<Classs>
    {
        public ClasssValidation()
        {
            RuleFor(x => x.ClassName).NotEmpty().WithMessage("DersAdı boş olamaz.");

        }
    }
}
