using System.Net.Mail;
using System.Text.RegularExpressions;
using SistemaGestaoEventosAcademicos.Negocio.Excecoes;

namespace SistemaGestaoEventosAcademicos.Negocio.Entidades;

/// <summary>
/// Representa uma pessoa física cadastrada no sistema (RN09, RN10, RN11).
/// </summary>
public class Participante
{
    public int ParticipanteId { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Instituicao { get; private set; } = string.Empty;

    public Participante(int participanteId, string nome, string cpf, string email, string instituicao)
    {
        ValidarObrigatoriedade(nome, cpf, email);
        var cpfNormalizado = NormalizarCpf(cpf);
        ValidarCpf(cpfNormalizado);
        ValidarEmail(email);

        ParticipanteId = participanteId;
        Nome = nome.Trim();
        Cpf = cpfNormalizado;
        Email = email.Trim().ToLowerInvariant();
        Instituicao = instituicao?.Trim() ?? string.Empty;
    }

    public void AtualizarDados(string nome, string email, string instituicao)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email))
        {
            throw new RegraNegocioException("Nome, CPF e E-mail são obrigatórios e devem ser preenchidos.");
        }

        ValidarEmail(email);

        Nome = nome.Trim();
        Email = email.Trim().ToLowerInvariant();
        Instituicao = instituicao?.Trim() ?? string.Empty;
    }

    public void AtualizarCpf(string novoCpf)
    {
        if (string.IsNullOrWhiteSpace(novoCpf))
        {
            throw new RegraNegocioException("Nome, CPF e E-mail são obrigatórios e devem ser preenchidos.");
        }

        var cpfNormalizado = NormalizarCpf(novoCpf);
        ValidarCpf(cpfNormalizado);
        Cpf = cpfNormalizado;
    }

    public static string NormalizarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return string.Empty;

        return Regex.Replace(cpf, @"[^\d]", "");
    }

    private static void ValidarObrigatoriedade(string nome, string cpf, string email)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(cpf) || string.IsNullOrWhiteSpace(email))
        {
            throw new RegraNegocioException("Nome, CPF e E-mail são obrigatórios e devem ser preenchidos.");
        }
    }

    private static void ValidarCpf(string cpf)
    {
        if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
        {
            throw new RegraNegocioException("CPF inválido ou já cadastrado para outro participante.");
        }

        if (new string(cpf[0], 11) == cpf)
        {
            throw new RegraNegocioException("CPF inválido ou já cadastrado para outro participante.");
        }

        int[] multiplicadores1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicadores2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        string tempCpf = cpf[..9];
        int soma = 0;

        for (int i = 0; i < 9; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];
        }

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito1;
        soma = 0;

        for (int i = 0; i < 10; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];
        }

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        if (!cpf.EndsWith($"{digito1}{digito2}"))
        {
            throw new RegraNegocioException("CPF inválido ou já cadastrado para outro participante.");
        }
    }

    private static void ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new RegraNegocioException("E-mail em formato inválido ou já cadastrado no sistema.");
        }

        try
        {
            var mailAddress = new MailAddress(email.Trim());
            if (!mailAddress.Address.Contains('.') || !Regex.IsMatch(email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new RegraNegocioException("E-mail em formato inválido ou já cadastrado no sistema.");
            }
        }
        catch (FormatException)
        {
            throw new RegraNegocioException("E-mail em formato inválido ou já cadastrado no sistema.");
        }
    }
}
