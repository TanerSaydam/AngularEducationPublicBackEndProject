using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(p=> p.Name).NotEmpty().WithMessage("Kitap adı boş olamaz");
            RuleFor(p=> p.Name).MinimumLength(3).WithMessage("Kitap adı en az 3 karakter olmalıdır");
            RuleFor(p=> p.Writer).NotEmpty().WithMessage("Kitap yazarı boş olamaz");
            RuleFor(p=> p.Writer).MinimumLength(3).WithMessage("Kitap yazarı en az 3 karakter olmalıdır");
            RuleFor(p=> p.PublishDate).NotEmpty().WithMessage("Kitap yayınlanma tarihi boş olamaz");
            RuleFor(p=> p.ImageUrl).NotEmpty().WithMessage("Kitap resmi boş olamaz");
        }
    }
}
