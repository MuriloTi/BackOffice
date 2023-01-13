using System.ComponentModel.DataAnnotations;

namespace BackOffice.Application.Validators
{
    public class CPFValidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return true;
            }
            else
            {
                return ValidateCPF(value.ToString());
            }
        }

        private static string RemoveNotNumber(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }

        public static bool ValidateCPF(string cpf)
        {
            cpf = RemoveNotNumber(cpf);

            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool equal = true;
            for (int i = 1; i < 11 && equal; i++)
                if (cpf[i] != cpf[0])
                    equal = false;

            if (equal || cpf == "12345678909")
                return false;

            int[] number = new int[11];

            for (int i = 0; i < 11; i++)
                number[i] = int.Parse(cpf[i].ToString());

            int total = 0;
            for (int i = 0; i < 9; i++)
                total += (10 - i) * number[i];

            int result = total % 11;

            if (result == 1 || result == 0)
            {
                if (number[9] != 0)
                    return false;
            }
            else if (number[9] != 11 - result)
                return false;

            total = 0;
            for (int i = 0; i < 10; i++)
                total += (11 - i) * number[i];

            result = total % 11;

            if (result == 1 || result == 0)
            {
                if (number[10] != 0)
                    return false;
            }
            else
                if (number[10] != 11 - result)
                return false;

            return true;
        }
    }
}
