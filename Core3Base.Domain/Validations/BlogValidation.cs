using System;
using System.Collections.Generic;
using System.Text;
using Core3Base.Infra.Data.Entity;
using FluentValidation;

namespace Core3Base.Domain.Validations
{
    public class BlogValidation : AbstractValidator<Blog>
    {
        public BlogValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş olamaz.");
        }
    }
}
