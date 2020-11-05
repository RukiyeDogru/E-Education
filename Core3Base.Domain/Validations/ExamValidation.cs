using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
   public class ExamValidation: AbstractValidator<Exam>
    {
        public ExamValidation()
        {
            RuleFor(x => x.ExamName).NotEmpty().WithMessage("ExamName boş olamaz.");
        }

   }
}
