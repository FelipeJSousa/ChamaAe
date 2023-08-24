using System.Collections.Generic;

namespace ChamaAe.Servico.Domain.Validators
{
    /// <summary>
    /// Ensures that the property value is a valid CPF number.
    /// </summary>
    public class CpfValidator<T, TProperty> : PersonValidator<T, TProperty>
    {
        internal CpfValidator(int validLength, string errorMessage) 
            : base(validLength, errorMessage)
        { }

        public CpfValidator(string errorMessage)
            : this(11, errorMessage)
        { }

        public CpfValidator()
            : this("O CPF é inválido!")
        { }

        protected override IEnumerable<int> FirstMultiplierCollection => new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        protected override IEnumerable<int> SecondMultiplierCollection => new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        public override string Name => "CPFValidator";
    }
}