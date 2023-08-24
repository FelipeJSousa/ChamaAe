using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace ChamaAe.Servico.Domain.Validators
{
    public abstract class PersonValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        private readonly int _validLength;
        private readonly string _errorMessage;

        protected abstract IEnumerable<int> FirstMultiplierCollection { get; }
        protected abstract IEnumerable<int> SecondMultiplierCollection { get; }

        protected PersonValidator(int validLength, string errorMessage)
        {
            this._validLength = validLength;
            this._errorMessage = errorMessage;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return string.IsNullOrWhiteSpace(_errorMessage) ? base.GetDefaultMessageTemplate(errorCode) : _errorMessage;
        }

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            var val = value as string ?? string.Empty;
            val = Regex.Replace(val, "[^a-zA-Z0-9]", "");

            if (IsValidLength(val) || 
                AllDigitsAreEqual(val) || 
                value == null) return false;

            var cpf = val.Select(x => (int)char.GetNumericValue(x)).ToArray();
            var digits = GetDigits(cpf);

            return val.EndsWith(digits);
        }

        private static bool AllDigitsAreEqual (string value) => value.All(x => x == value.FirstOrDefault());

        private bool IsValidLength (string value) => !string.IsNullOrWhiteSpace(value) && value.Length != _validLength;

        private string GetDigits(int[] cpf)
        {
            var first = CalculateValue(FirstMultiplierCollection, cpf);
            var second = CalculateValue(SecondMultiplierCollection, cpf);

            return $"{CalculateDigit(first)}{CalculateDigit(second)}";
        }

        private static int CalculateValue(IEnumerable<int> weight, IReadOnlyList<int> numbers)
        {
            return weight.Select((t, i) => t * numbers[i]).Sum();
        }

        private static int CalculateDigit(int sum)
        {
            var modResult = (sum % 11);
            return modResult < 2 ? 0 : 11 - modResult;
        }
    }
}