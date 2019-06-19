using AlugaTudo.Modelo;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlugaTudo.Manager
{
    public class CarroValidator : AbstractValidator<Carro>
    {
        public CarroValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage("Carro não pode ser nulo.");
            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty().WithMessage(x => $"{x.Nome} é campo obrigatório.")
                .Length(5, 100).WithMessage(x => $"{x.Nome} deve ter no mínimo 5 e no máximo 100 caracteres.");
            RuleFor(x => x.Ano)
                .GreaterThan(DateTime.Now.Date.AddYears(-120).Year).WithMessage("Ano de fabricação inválido. O carro não pode ter mais de 120 anos.");
            RuleFor(x => x.Fabricante)
                .NotNull()
                .NotEmpty().WithMessage(x => $"{x.Fabricante} é campo obrigatório.")
                .Length(2, 100).WithMessage(x => $"{x.Fabricante} deve ter no mínimo 5 e no máximo 100 caracteres.");
        }

    }
}
