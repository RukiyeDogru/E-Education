using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
    public class StudentAnswerValidation : AbstractValidator<StudentAnswer>
    {
        public StudentAnswerValidation()
        {
            RuleFor(x => x.StudentId).NotEmpty().WithMessage("StudentId boş olamaz.");

        }
    }
}
