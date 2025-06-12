using FluentValidation;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Update
{
    public class ChartOfAccountsUpdateCommandValidator : AbstractValidator<ChartOfAccountsUpdateCommand>
    {
        public ChartOfAccountsUpdateCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(100).WithMessage("Nome não pode exceder 100 caracteres.");

            RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("O campo não pode ser vazio.")
            // Regex: Começa com dígitos, permite pontos seguidos de dígitos. Sem duplos pontos, sem iniciar ou terminar com ponto.
            .Matches(@"^\d+(\.\d+)*$")
            .WithMessage("O campo deve conter apenas números e pontos, não podendo começar/terminar com ponto, nem ter pontos juntos.")
            // Lógica: ao remover os pontos, número deve ser <= 999999999999999
            .Must(ValorMenorOuIgualLimite)
            .WithMessage("O deve estar entre 0 e 999.999.999.999.999.");

            RuleFor(x => x.TenantId)
                .NotEmpty().WithMessage("TenantId é obrigatorio.");

            //TODO VERIFICAR POR ENUM
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Tipo deve Receita ou Despesa.");


        }

        private bool ValorMenorOuIgualLimite(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return false;

            // Remove os pontos
            var semPontos = codigo.Replace(".", "");
            // Tenta converter para um número inteiro longo
            if (long.TryParse(semPontos, out var valor))
            {
                return valor > 0 && valor <= 999999999999999;
            }
            return false;
        }

    }
}
