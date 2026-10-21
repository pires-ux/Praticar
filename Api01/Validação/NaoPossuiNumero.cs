using System.ComponentModel.DataAnnotations;

namespace Api01.Validação
{
    public class NaoPossuiNumero: ValidationAttribute
    {
        private readonly string Nome;
        public NaoPossuiNumero(string nome)
        {
            Nome = nome;
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }
            var valorString = value.ToString();
            foreach (var caractere in valorString)
            {
                if (char.IsDigit(caractere))
                {
                    return false;
                }
            }
            ErrorMessage = $"O campo {Nome} não deve conter números.";
            return true;
        }
    }
}
