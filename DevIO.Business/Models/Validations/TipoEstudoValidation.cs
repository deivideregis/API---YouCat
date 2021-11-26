using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Business.Models.Validations
{
    public class TipoEstudoValidation : AbstractValidator<TipoEstudo>
    {
        public TipoEstudoValidation()
        {
            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(10, 250)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
