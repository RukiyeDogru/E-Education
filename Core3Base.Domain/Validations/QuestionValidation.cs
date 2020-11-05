using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
    public class QuestionValidation : AbstractValidator<Question>
    {
        public QuestionValidation()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("TeacherId boş olamaz.");

        }
    }
}
