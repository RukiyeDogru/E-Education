using Core3Base.Infra.Data.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Validations
{
    public class QuestionAnswerValidation : AbstractValidator<QuestionsAnswer>
    {
        public QuestionAnswerValidation()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content boş olamaz.");

            RuleFor(x => x.Option).NotEmpty().WithMessage("Option boş olamaz.");

            RuleFor(x => x.QuestionsId).NotEmpty().WithMessage("QuestionsId boş olamaz.");

            RuleFor(x => x.AnswerId).NotEmpty().WithMessage("AnswerId boş olamaz.");


        }

    }
}
