using System.Globalization;

namespace SmnHelpDesk.Domain.Empresa
{
    public class EmpresaService : IEmpresaService
    {
        private readonly Notification _notification;

        public EmpresaService(Notification notification)
        {
            _notification = notification;
        }

        public bool IsValidCpf(decimal numeroCpf)
        {
            var cpf = numeroCpf.ToString(CultureInfo.CurrentCulture);

            if (cpf.Length != 11)
            {
                _notification.Add("Cpf com tamanho inválido");
                return false;
            }

            if (cpf == "0" || cpf == "11111111111" || cpf == "22222222222" || cpf == "3333333333" || cpf == "44444444444" ||
                cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999")
            {
                _notification.Add("Cpf inválido");
                return false;
            }

            var somaDigito1 = 0;
            var multDigito1 = 10;
            var resultado = 0;

            for (var i = 0; i < 9; i++)
            {
                if (multDigito1 < 2) continue;
                somaDigito1 = somaDigito1 + (int.Parse(cpf.Substring(i, 1))) * multDigito1;
                multDigito1--;
            }
            resultado = somaDigito1 % 11 < 2 ? 0 : 11 - somaDigito1 % 11;

            if (resultado == int.Parse(cpf.Substring(9, 1)))
            {
                var somaDigito2 = 0;
                var multDigito2 = 11;
                for (var i = 0; i < 10; i++)
                {
                    if (multDigito2 < 2) continue;
                    somaDigito2 = somaDigito2 + (int.Parse(cpf.Substring(i, 1))) * multDigito2;
                    multDigito2--;
                }
                resultado = somaDigito2 % 11 < 2 ? 0 : 11 - somaDigito2 % 11;
                if (resultado == int.Parse(cpf.Substring(10, 1)))
                    return true;

                _notification.Add("Cpf inválido");
                return false;
            }

            _notification.Add("Cpf inválido");
            return false;
        }

        public bool IsValidCnpj(decimal numeroCnpj)
        {
            var cnpj = numeroCnpj.ToString(CultureInfo.CurrentCulture);

            if (cnpj.Length != 14)
            {
                _notification.Add("Cnpj com tamanho inválido");
                return false;
            }

            var somaDigito1 = 0;
            var multDigito1 = 5;
            var resultado = 0;

            for (int i = 0; i < 4; i++)
            {
                if (multDigito1 < 2) continue;
                somaDigito1 = somaDigito1 + (int.Parse(cnpj.Substring(i, 1))) * multDigito1;
                multDigito1--;
            }

            multDigito1 = 9;

            for (int i = 0; i < 9; i++)
            {
                if (multDigito1 < 2) continue;
                somaDigito1 = somaDigito1 + (int.Parse(cnpj.Substring(i, 1))) * multDigito1;
                multDigito1--;
            }
            resultado = somaDigito1 % 11 < 2 ? 0 : 11 - somaDigito1 % 11;
            if (resultado == int.Parse(cnpj.Substring(12, 1)))
            {
                var somaDigito2 = 0;
                var multDigito2 = 6;

                for (int i = 0; i < 4; i++)
                {
                    if (multDigito2 < 2) continue;
                    somaDigito2 = somaDigito2 + int.Parse(cnpj.Substring(i, 1)) * multDigito2;
                    multDigito2--;
                }

                multDigito2 = 9;

                for (int i = 0; i < 10; i++)
                {
                    if (multDigito2 < 2) continue;
                    somaDigito2 = somaDigito2 + int.Parse(cnpj.Substring(i, 1)) * multDigito2;
                    multDigito2--;
                }

                resultado = somaDigito2 % 11 < 2 ? 0 : 11 - somaDigito2 % 11;
                if (resultado == int.Parse(cnpj.Substring(13, 1)))
                    return true;

                _notification.Add("Cnpj inválido");
                return false;
            }
            _notification.Add("Cnpj inválido");
            return false;
        }
    }
}
