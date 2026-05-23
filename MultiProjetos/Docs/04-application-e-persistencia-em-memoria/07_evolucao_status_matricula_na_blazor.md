# Evolucao: Status de Matricula na Blazor

## Objetivo

Evoluir a tela didatica `Features/FluxoAcademico.razor` para permitir mudanca de status de matricula diretamente pela interface.

## O que foi adicionado

1. Botao **Cancelar** para matriculas ativas.
2. Botao **Concluir** para matriculas ativas.
3. Atualizacao de lista apos operacao bem-sucedida.
4. Reuso do feedback padronizado (sucesso/erro) ja existente na tela.

## Regra de exibicao na UI

Os botoes aparecem apenas quando o status da matricula e `Ativa`.

Isso evita enviar acoes invalidas para matriculas ja canceladas ou concluidas.

## Responsabilidade arquitetural

A UI apenas dispara a acao:

1. `EnrollmentService.CancelEnrollment(id)`
2. `EnrollmentService.CompleteEnrollment(id)`

A regra de negocio continua no dominio, via metodos da entidade `Matricula`:

1. `Cancelar()`
2. `Concluir()`

## Aprendizado didatico

Esta evolucao reforca que:

1. Interface orquestra interacao e exibicao.
2. Application orquestra caso de uso.
3. Domain protege invariantes de estado.

## Cenarios de teste manual

### Preparacao

1. Executar a BlazorApp.
2. Acessar a rota `/academico`.
3. Criar ao menos 1 curso e 1 aluno.
4. Realizar uma matricula.

### Cenario 1 - Concluir matricula ativa

1. Na lista de matriculas, localizar item com status `Ativa`.
2. Clicar em **Concluir**.

Resultado esperado:

1. Mensagem de sucesso exibida.
2. Status muda para `Concluida`.
3. Botoes **Cancelar** e **Concluir** deixam de aparecer para essa matricula.

### Cenario 2 - Cancelar matricula ativa

1. Criar nova matricula para teste.
2. Na lista de matriculas, clicar em **Cancelar**.

Resultado esperado:

1. Mensagem de sucesso exibida.
2. Status muda para `Cancelada`.
3. Botoes de acao deixam de aparecer para essa matricula.

### Cenario 3 - Evitar transicao invalida via UI

1. Verificar uma matricula `Concluida` ou `Cancelada`.

Resultado esperado:

1. Nao ha botoes de acao disponiveis para novo cancelamento/conclusao.

### Cenario 4 - Validar feedback de erro de negocio

1. Concluir uma matricula ativa.
2. Sem recarregar, tentar forcar chamada repetida pela mesma acao (se houver evento duplicado).

Resultado esperado:

1. Caso a chamada ocorra, o dominio deve bloquear a transicao invalida.
2. A interface exibe mensagem de erro retornada pela Application.

### Cenario 5 - Integridade de listagem

1. Executar sequencia: criar matricula -> concluir/cancelar -> criar nova matricula.

Resultado esperado:

1. Lista permanece consistente.
2. Cada item mostra status correto.
3. Nenhuma matricula perde vinculacao de aluno/curso na exibicao.
