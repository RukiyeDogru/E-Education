using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
   public class AnswerValidation:AbstractValidator<Answer>
    {

        public AnswerValidation()
        {
            RuleFor(x => x.Responce).NotEmpty().WithMessage("Cevap boş olamaz.");
        }


    }
}
