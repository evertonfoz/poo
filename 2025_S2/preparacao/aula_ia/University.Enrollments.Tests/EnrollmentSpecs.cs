using Xunit;

namespace University.Enrollments.Tests;

public class EnrollmentSpecs
{
    [Fact]
    public void Enroll_cria_vinculo_e_respeita_unicidade_do_par()
    {
        // TODO: Arrange - preparar studentId e courseId distintos/iguais conforme cenário
        // TODO: Act - chamar método Enroll
        // TODO: Assert - verificar que vínculo criado apenas uma vez e não duplica
    }

    [Fact]
    public void Enroll_fora_da_janela_deve_falhar()
    {
        // TODO: Arrange - preparar curso com janela de inscrição fechada
        // TODO: Act - tentar efetuar Enroll
        // TODO: Assert - garantir que operação falha (ex: lança exceção ou retorna erro)
    }

    [Fact]
    public void Enroll_sem_vagas_deve_falhar()
    {
        // TODO: Arrange - preparar curso com vagas esgotadas
        // TODO: Act - tentar efetuar Enroll
        // TODO: Assert - garantir que operação falha por falta de vagas
    }

    [Fact]
    public void Unenroll_remove_vinculo_e_atualiza_contadores()
    {
        // TODO: Arrange - ter um vínculo existente e contadores de vagas/inscritos
        // TODO: Act - chamar Unenroll
        // TODO: Assert - verificar que vínculo removido e contadores atualizados
    }

    [Fact]
    public void Conclusao_transiciona_para_Completed_quando_criterios_ok()
    {
        // TODO: Arrange - preparar matrícula que satisfaça critérios de conclusão
        // TODO: Act - executar ação de conclusão
        // TODO: Assert - verificar estado transicionado para Completed
    }

    [Fact]
    public void Transicoes_invalidas_devem_ser_barradas()
    {
        // TODO: Arrange - preparar matrícula em estado que não permite certa transição
        // TODO: Act - tentar realizar transição inválida
        // TODO: Assert - garantir que transição é rejeitada (ex: exceção ou retorno inválido)
    }

    [Fact]
    public void Equality_baseada_em_studentId_courseId()
    {
        // TODO: Arrange - criar duas entidades com mesmo studentId/courseId e outra diferente
        // TODO: Act - comparar igualdade/inequality
        // TODO: Assert - igualdade baseada apenas em studentId+courseId
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void Navegabilidade_unidirecional_cobre_casos_basicos(int caseId)
    {
        _ = caseId; // referenced to satisfy xUnit parameter usage; remove when implementing
        // TODO: Arrange - montar objetos relacionados com navegabilidade unidirecional
        // Use caseId to select scenario variation (1,2) when implementing
        // TODO: Act - navegar pelas propriedades/coleções
        // TODO: Assert - verificar casos básicos de acesso e integridade
    }
}
