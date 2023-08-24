using ChamaAe.Servico.Domain.Entities;
using ChamaAe.Servico.Domain.Validators.Extensions;
using FluentValidation;
using FluentValidation.Results;

namespace ChamaAe.Servico.Domain.Validators;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(x => x.Nome).MinimumLength(10).WithMessage("O nome do usuário precisa ter ao menos 10 caracteres.");
        RuleFor(x => x.Nome).MaximumLength(50).WithMessage("O nome do usuário precisa ter no máximo 150 caracteres.");

        RuleFor(x => x.Senha).MinimumLength(6).WithMessage("A senha do usuário precisa ter ao menos 6 caracteres.");
        RuleFor(x => x.Senha).MaximumLength(150).WithMessage("A senha do usuário precisa ter no máximo 150 caracteres.");

        RuleFor(x => x.Email).EmailAddress().WithMessage("O email informado não é válido.");
        RuleFor(x => x.Email).MinimumLength(7).WithMessage("O email do usuário precisa ter ao menos 10 caracteres.");
        RuleFor(x => x.Email).MaximumLength(60).WithMessage("O email do usuário precisa ter no máximo 60 caracteres.");
        
        RuleFor(x => x.Cpf).IsValidCpf().WithMessage("O CPF informado não é válido");

        RuleFor(x => x.Endereco).MinimumLength(3).WithMessage("O endereço do usuário precisa ter ao menos 3 caracteres.");
        RuleFor(x => x.Endereco).MaximumLength(100).WithMessage("O endereço do usuário precisa ter no máximo 100 caracteres.");

        RuleFor(x => x.EnderecoNumero).MinimumLength(1).WithMessage("O número do endereço do usuário precisa ter ao menos 1 caracter.");
        RuleFor(x => x.EnderecoNumero).MaximumLength(10).WithMessage("O número do endereço do usuário precisa ter no máximo 10 caracteres.");

        RuleFor(x => x.EnderecoBairro).MinimumLength(3).WithMessage("O bairro do usuário precisa ter ao menos 3 caracteres.");
        RuleFor(x => x.EnderecoBairro).MaximumLength(100).WithMessage("O bairro do usuário precisa ter no máximo 100 caracteres.");

        RuleFor(x => x.EnderecoReferencia).MaximumLength(150).WithMessage("A referência do endereço do usuário precisa ter no máximo 150 caracteres.");
        
        RuleFor(x => x.EnderecoCidade).MinimumLength(3).WithMessage("A cidade do usuário precisa ter ao menos 3 caracteres.");
        RuleFor(x => x.EnderecoCidade).MaximumLength(50).WithMessage("A cidade do usuário precisa ter no máximo 50 caracteres.");

        RuleFor(x => x.EnderecoEstado).MinimumLength(2).WithMessage("O estado do usuário precisa ter ao menos 2 caracteres.");
        RuleFor(x => x.EnderecoEstado).MaximumLength(2).WithMessage("O estado do usuário precisa ter no máximo 2 caracteres.");

        RuleFor(x => x.EnderecoCep).MinimumLength(9).WithMessage("O CEP é inválido.");

    }
}