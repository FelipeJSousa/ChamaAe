using FluentValidation;

namespace ChamaAe.Servico.Domain.Validators.Extensions
{
    public static partial class DefaultValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> IsValidCpf<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CpfValidator<T, string>());
        }
    }
}