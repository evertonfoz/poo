# Casos de Uso - Sistema de Incubacao de Projetos de Alunos

## 1. Objetivo

Documentar os casos de uso funcionais que orientam toda a implementacao futura do sistema de incubacao de projetos, com base nos requisitos funcionais RF01 a RF29.

## 2. Atores

- Operador da Incubadora: usuario que cadastra entidades, configura projetos e conduz o fluxo administrativo.
- Membro da Equipe (Estudante): estudante colaborador habilitado a registrar avaliacoes de marcos.

## 3. Catalogo de Casos de Uso

## UC01 - Cadastrar Estudante

- Ator principal: Operador da Incubadora
- Objetivo: criar um estudante valido no sistema.
- Pre-condicoes: nenhuma.
- Pos-condicoes de sucesso: estudante criado com dados obrigatorios validos.
- Fluxo principal:
1. Operador informa os dados do estudante.
2. Sistema valida campos obrigatorios.
3. Sistema cria o estudante.
4. Sistema confirma cadastro.
- Fluxos alternativos/excecoes:
1. Campo obrigatorio invalido (nulo/vazio/branco): sistema rejeita operacao com erro de validacao.
- Requisitos cobertos: RF01.

## UC02 - Cadastrar Professor

- Ator principal: Operador da Incubadora
- Objetivo: criar um professor valido no sistema.
- Pre-condicoes: nenhuma.
- Pos-condicoes de sucesso: professor criado com dados obrigatorios validos.
- Fluxo principal:
1. Operador informa os dados do professor.
2. Sistema valida campos obrigatorios.
3. Sistema cria o professor.
4. Sistema confirma cadastro.
- Fluxos alternativos/excecoes:
1. Campo obrigatorio invalido: sistema rejeita operacao com erro de validacao.
- Requisitos cobertos: RF02.

## UC03 - Cadastrar Laboratorio

- Ator principal: Operador da Incubadora
- Objetivo: criar um laboratorio valido no sistema.
- Pre-condicoes: nenhuma.
- Pos-condicoes de sucesso: laboratorio criado com dados obrigatorios validos.
- Fluxo principal:
1. Operador informa os dados do laboratorio.
2. Sistema valida campos obrigatorios.
3. Sistema cria o laboratorio.
4. Sistema confirma cadastro.
- Fluxos alternativos/excecoes:
1. Campo obrigatorio invalido: sistema rejeita operacao com erro de validacao.
- Requisitos cobertos: RF03.

## UC04 - Cadastrar Consultor Externo

- Ator principal: Operador da Incubadora
- Objetivo: criar um consultor externo valido para uso por agregacao em projetos.
- Pre-condicoes: nenhuma.
- Pos-condicoes de sucesso: consultor externo criado e disponivel para associacao.
- Fluxo principal:
1. Operador informa dados do consultor.
2. Sistema valida campos obrigatorios.
3. Sistema cria consultor externo.
4. Sistema confirma cadastro.
- Fluxos alternativos/excecoes:
1. Campo obrigatorio invalido: sistema rejeita operacao com erro de validacao.
- Requisitos cobertos: RF04.

## UC05 - Cadastrar Recurso Institucional

- Ator principal: Operador da Incubadora
- Objetivo: criar um recurso institucional valido para uso por agregacao em projetos.
- Pre-condicoes: nenhuma.
- Pos-condicoes de sucesso: recurso institucional criado e disponivel para associacao.
- Fluxo principal:
1. Operador informa dados do recurso.
2. Sistema valida campos obrigatorios.
3. Sistema cria recurso institucional.
4. Sistema confirma cadastro.
- Fluxos alternativos/excecoes:
1. Campo obrigatorio invalido: sistema rejeita operacao com erro de validacao.
- Requisitos cobertos: RF05.

## UC06 - Criar Projeto Incubado

- Ator principal: Operador da Incubadora
- Objetivo: criar um projeto com associacoes obrigatorias 1:1 validas.
- Pre-condicoes:
1. Estudante proponente existente e valido.
2. Professor mentor existente e valido.
3. Laboratorio de referencia existente e valido.
- Pos-condicoes de sucesso: projeto incubado criado com estado consistente.
- Fluxo principal:
1. Operador informa titulo e dados iniciais do projeto.
2. Operador seleciona proponente, mentor e laboratorio.
3. Sistema valida dados obrigatorios e associacoes obrigatorias.
4. Sistema cria o projeto incubado.
5. Sistema confirma criacao do projeto.
- Fluxos alternativos/excecoes:
1. Falta de qualquer associacao obrigatoria: sistema impede criacao.
2. Dados textuais obrigatorios invalidos: sistema impede criacao.
- Requisitos cobertos: RF06.

## UC07 - Definir Coorientador do Projeto

- Ator principal: Operador da Incubadora
- Objetivo: associar opcionalmente um professor coorientador ao projeto.
- Pre-condicoes:
1. Projeto incubado existente.
2. Professor coorientador existente.
- Pos-condicoes de sucesso: projeto passa a ter coorientador valido (opcional).
- Fluxo principal:
1. Operador solicita definicao de coorientador para um projeto.
2. Sistema valida professor informado.
3. Sistema valida que coorientador e diferente do mentor.
4. Sistema associa coorientador ao projeto.
5. Sistema confirma operacao.
- Fluxos alternativos/excecoes:
1. Professor informado invalido: sistema rejeita.
2. Coorientador igual ao mentor: sistema rejeita.
- Requisitos cobertos: RF07, RF08.

## UC08 - Gerenciar Membros da Equipe

- Ator principal: Operador da Incubadora
- Objetivo: adicionar e remover estudantes membros da equipe do projeto.
- Pre-condicoes:
1. Projeto incubado existente.
2. Estudante existente.
- Pos-condicoes de sucesso:
1. Membro adicionado ou removido respeitando invariantes.
2. Equipe permanece consistente com regras do dominio.
- Fluxo principal (adicionar):
1. Operador solicita adicao de membro.
2. Sistema valida membro nao nulo.
3. Sistema valida ausencia de duplicidade.
4. Sistema valida que membro nao e o proponente.
5. Sistema valida que projeto nao esta finalizado.
6. Sistema adiciona membro.
7. Sistema confirma operacao.
- Fluxo principal (remover):
1. Operador solicita remocao de membro.
2. Sistema valida existencia do membro no projeto.
3. Sistema valida que projeto nao esta finalizado.
4. Sistema valida que remocao nao viola regra de marcos obrigatorios.
5. Sistema remove membro.
6. Sistema confirma operacao.
- Fluxos alternativos/excecoes:
1. Membro nulo.
2. Membro duplicado.
3. Proponente informado como membro comum.
4. Projeto finalizado.
5. Membro inexistente para remocao.
6. Remocao que viola invariantes do projeto.
- Requisitos cobertos: RF09, RF10, RF11.

## UC09 - Gerenciar Consultores Externos

- Ator principal: Operador da Incubadora
- Objetivo: adicionar e remover consultores externos associados ao projeto.
- Pre-condicoes:
1. Projeto incubado existente.
2. Consultor externo existente.
- Pos-condicoes de sucesso: conjunto de consultores atualizado sem violar invariantes.
- Fluxo principal (adicionar):
1. Operador solicita adicao de consultor.
2. Sistema valida consultor nao nulo.
3. Sistema valida ausencia de duplicidade no projeto.
4. Sistema valida que projeto nao esta finalizado.
5. Sistema adiciona consultor.
6. Sistema confirma operacao.
- Fluxo principal (remover):
1. Operador solicita remocao de consultor.
2. Sistema valida existencia do consultor no projeto.
3. Sistema valida que projeto nao esta finalizado.
4. Sistema remove consultor.
5. Sistema confirma operacao.
- Fluxos alternativos/excecoes:
1. Consultor nulo.
2. Consultor duplicado.
3. Projeto finalizado.
4. Consultor inexistente para remocao.
- Requisitos cobertos: RF12, RF13, RF14.

## UC10 - Gerenciar Recursos Institucionais

- Ator principal: Operador da Incubadora
- Objetivo: adicionar e remover recursos institucionais associados ao projeto.
- Pre-condicoes:
1. Projeto incubado existente.
2. Recurso institucional existente.
- Pos-condicoes de sucesso: conjunto de recursos atualizado sem violar invariantes.
- Fluxo principal (adicionar):
1. Operador solicita adicao de recurso.
2. Sistema valida recurso nao nulo.
3. Sistema valida ausencia de duplicidade no projeto.
4. Sistema valida que projeto nao esta finalizado.
5. Sistema adiciona recurso.
6. Sistema confirma operacao.
- Fluxo principal (remover):
1. Operador solicita remocao de recurso.
2. Sistema valida existencia do recurso no projeto.
3. Sistema valida que projeto nao esta finalizado.
4. Sistema remove recurso.
5. Sistema confirma operacao.
- Fluxos alternativos/excecoes:
1. Recurso nulo.
2. Recurso duplicado.
3. Projeto finalizado.
4. Recurso inexistente para remocao.
- Requisitos cobertos: RF15, RF16, RF17.

## UC11 - Cadastrar Marcos de Desenvolvimento do Projeto

- Ator principal: Operador da Incubadora
- Objetivo: criar marcos de desenvolvimento internos ao projeto (composicao).
- Pre-condicoes:
1. Projeto incubado existente.
2. Projeto nao finalizado.
- Pos-condicoes de sucesso: marcos criados e associados internamente ao projeto.
- Fluxo principal:
1. Operador solicita cadastro de marco com descricao, data prevista e obrigatoriedade.
2. Sistema valida descricao obrigatoria.
3. Sistema valida data prevista em relacao ao inicio do projeto.
4. Sistema valida ausencia de marco duplicado.
5. Sistema valida que projeto nao esta finalizado.
6. Sistema cria marco internamente no projeto.
7. Sistema confirma operacao.
- Fluxos alternativos/excecoes:
1. Descricao invalida.
2. Data prevista invalida.
3. Marco duplicado.
4. Projeto finalizado.
- Requisitos cobertos: RF18, RF19.

## UC12 - Registrar Avaliacao de Marco

- Ator principal: Membro da Equipe (Estudante)
- Ator de suporte: Operador da Incubadora
- Objetivo: registrar avaliacao/evidencia de marco por membro valido da equipe.
- Pre-condicoes:
1. Projeto incubado existente e nao finalizado.
2. Marco pertencente ao projeto.
3. Estudante avaliador membro da equipe.
- Pos-condicoes de sucesso: avaliacao registrada no projeto, vinculada ao marco.
- Fluxo principal:
1. Membro solicita registro de avaliacao informando marco, descricao e status de aprovacao.
2. Sistema valida que estudante e membro da equipe.
3. Sistema valida que marco pertence ao projeto.
4. Sistema valida descricao obrigatoria.
5. Sistema valida ausencia de avaliacao duplicada do mesmo membro para o mesmo marco.
6. Sistema valida que projeto nao esta finalizado.
7. Sistema registra avaliacao.
8. Sistema confirma operacao.
- Fluxos alternativos/excecoes:
1. Estudante nao membro da equipe.
2. Marco nao pertencente ao projeto.
3. Descricao invalida.
4. Avaliacao duplicada.
5. Projeto finalizado.
- Requisitos cobertos: RF20, RF21.

## UC13 - Finalizar Projeto Incubado

- Ator principal: Operador da Incubadora
- Objetivo: finalizar o projeto somente quando todas as pre-condicoes de negocio forem satisfeitas.
- Pre-condicoes:
1. Projeto incubado existente.
2. Projeto ainda nao finalizado.
- Pos-condicoes de sucesso:
1. Projeto marcado como finalizado.
2. Alteracoes estruturais passam a ser bloqueadas.
- Fluxo principal:
1. Operador solicita finalizacao do projeto.
2. Sistema verifica associacoes obrigatorias validas (proponente, mentor, laboratorio).
3. Sistema verifica existencia minima de membros e marcos obrigatorios.
4. Sistema verifica avaliacao em todos os marcos obrigatorios.
5. Sistema verifica ausencia de duplicidades e consistencia entre papeis (mentor/coorientador/proponente).
6. Sistema finaliza o projeto.
7. Sistema confirma finalizacao.
- Fluxos alternativos/excecoes:
1. Ausencia de membro de equipe.
2. Ausencia de marco obrigatorio.
3. Marco obrigatorio sem avaliacao.
4. Duplicidades ou inconsistencias detectadas.
5. Projeto ja finalizado.
- Requisitos cobertos: RF22, RF23, RF24.

## UC14 - Consultar Percentual de Marcos Aprovados

- Ator principal: Operador da Incubadora
- Objetivo: obter o percentual de marcos aprovados de projeto finalizado.
- Pre-condicoes:
1. Projeto incubado existente.
2. Projeto finalizado.
- Pos-condicoes de sucesso: percentual calculado e exibido.
- Fluxo principal:
1. Operador solicita percentual de marcos aprovados.
2. Sistema valida que projeto esta finalizado.
3. Sistema calcula percentual com base em marcos que tenham ao menos uma avaliacao aprovada.
4. Sistema retorna resultado percentual.
- Fluxos alternativos/excecoes:
1. Projeto nao finalizado: sistema impede consulta.
- Requisitos cobertos: RF25, RF26, RF27.

## UC15 - Executar Demonstracao de Fluxo e Cenarios Invalidos no Program

- Ator principal: Operador da Incubadora
- Objetivo: demonstrar funcionamento completo (valido e invalido) em console.
- Pre-condicoes: implementacao das entidades e regras de negocio disponivel.
- Pos-condicoes de sucesso:
1. Fluxo valido executado ate finalizacao e exibicao do percentual.
2. Pelo menos quatro cenarios invalidos tratados com try/catch e mensagens no console.
- Fluxo principal:
1. Sistema executa criacao das entidades principais.
2. Sistema executa criacao de projeto e configuracoes.
3. Sistema executa adicoes de membros, consultores, recursos, marcos e avaliacoes.
4. Sistema finaliza projeto.
5. Sistema exibe percentual de marcos aprovados.
6. Sistema executa cenarios invalidos e trata excecoes com mensagens.
- Fluxos alternativos/excecoes:
1. Qualquer cenario invalido sem tratamento: demonstracao e considerada incompleta.
- Requisitos cobertos: RF28, RF29.

## 4. Matriz de Rastreabilidade (RF x UC)

| Requisito | Caso(s) de Uso |
|---|---|
| RF01 | UC01 |
| RF02 | UC02 |
| RF03 | UC03 |
| RF04 | UC04 |
| RF05 | UC05 |
| RF06 | UC06 |
| RF07 | UC07 |
| RF08 | UC07 |
| RF09 | UC08 |
| RF10 | UC08 |
| RF11 | UC08 |
| RF12 | UC09 |
| RF13 | UC09 |
| RF14 | UC09 |
| RF15 | UC10 |
| RF16 | UC10 |
| RF17 | UC10 |
| RF18 | UC11 |
| RF19 | UC11 |
| RF20 | UC12 |
| RF21 | UC12 |
| RF22 | UC13 |
| RF23 | UC13 |
| RF24 | UC13 |
| RF25 | UC14 |
| RF26 | UC14 |
| RF27 | UC14 |
| RF28 | UC15 |
| RF29 | UC15 |

## 5. Observacoes para implementacao futura

- Os casos de uso foram definidos para orientar implementacao por comportamento observavel de dominio.
- Cada UC deve ser convertido em cenarios de teste (feliz e excecao), garantindo cobertura de invariantes.
- A modelagem pode evoluir em detalhes tecnicos, desde que preserve as regras funcionais mapeadas na matriz acima.
