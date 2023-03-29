using System.Globalization;

namespace Store.Domain.StoreContext.Utils;
/// <summary>
/// Validações comuns para documentos do Brasil
/// </summary>
public static class DocumentsBR
{
    public static bool IsValidCPF(string value)
    {
        if (value == null)
            return false;

        // Remove quaisquer caracteres não numéricos
        var cpf = new string(value.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return false;

        if (!long.TryParse(cpf, NumberStyles.Number, CultureInfo.InvariantCulture, out _))
            return false;

        int[] digitsCPF = cpf
            .ToString()
            .Select(c => int.Parse(c.ToString()))
            .ToArray();

        // Calcula o primeiro dígito verificador
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += digitsCPF[i] * (10 - i);
        }
        int rest = sum % 11;
        int primeiroDigitoVerificador = (rest < 2) ? 0 : (11 - rest);

        // Calcula o segundo dígito verificador
        sum = 0;
        for (int i = 0; i < 10; i++)
        {
            sum += digitsCPF[i] * (11 - i);
        }
        rest = sum % 11;
        int segundoDigitoVerificador = (rest < 2) ? 0 : (11 - rest);

        // Verifica se os dígitos verificadores calculados são iguais aos dígitos do CPF
        return (digitsCPF[9] == primeiroDigitoVerificador && digitsCPF[10] == segundoDigitoVerificador);
    }
}