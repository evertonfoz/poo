# Requisitos Funcionais

Documento derivado de [prova_poo_associacoes_incubadora_projetos_alunos.md](/Users/evertoncoimbradearaujo/Documents/GitHub/aula-poo-github/TrabalhoPOO/docs/prova_poo_associacoes_incubadora_projetos_alunos.md).

## Objetivo

Registrar, de forma estruturada, os requisitos funcionais presentes no enunciado da prova sobre incubacao de projetos inovadores desenvolvidos por estudantes.

## Escopo considerado

Foram considerados apenas requisitos funcionais do dominio e do comportamento esperado do sistema. Regras tecnicas de implementacao, criterios de avaliacao e conhecimentos previos nao foram classificados como requisitos funcionais.

## Requisitos funcionais identificados

| ID | Requisito funcional | Origem no enunciado |
|---|---|---|
| RF01 | O sistema deve permitir criar estudantes com dados obrigatorios validados. | Objetos validos desde a criacao |
| RF02 | O sistema deve permitir criar professores com dados obrigatorios validados. | Objetos validos desde a criacao |
| RF03 | O sistema deve permitir criar laboratorios com dados obrigatorios validados. | Objetos validos desde a criacao |
| RF04 | O sistema deve permitir criar consultores externos com dados obrigatorios validados. | Objetos validos desde a criacao |
| RF05 | O sistema deve permitir criar recursos institucionais com dados obrigatorios validados. | Objetos validos desde a criacao |
| RF06 | O sistema deve permitir criar projetos incubados somente quando houver estudante proponente, professor mentor e laboratorio de referencia validos associados ao projeto. | Associacao 1:1 obrigatoria |
| RF07 | O sistema deve permitir definir, opcionalmente e em momento posterior, um professor coorientador para o projeto incubado. | Associacao 1:1 opcional |
| RF08 | O sistema deve impedir que o professor coorientador seja o mesmo professor definido como mentor do projeto. | Associacao 1:1 opcional |
| RF09 | O sistema deve permitir adicionar membros de equipe a um projeto incubado. | Associacao 1:N com membros da equipe |
| RF10 | O sistema deve permitir remover membros de equipe de um projeto incubado. | Associacao 1:N com membros da equipe |
| RF11 | O sistema deve validar as operacoes sobre membros da equipe, impedindo membro nulo, duplicado, uso do proponente como membro comum, alteracoes apos finalizacao, remocao de membro inexistente e remocao que viole as regras de marcos obrigatorios. | Invariantes dos membros da equipe |
| RF12 | O sistema deve permitir adicionar consultores externos a um projeto incubado. | Associacao 1:N por agregacao com consultores externos |
| RF13 | O sistema deve permitir remover consultores externos de um projeto incubado. | Associacao 1:N por agregacao com consultores externos |
| RF14 | O sistema deve validar as operacoes sobre consultores externos, impedindo consultor nulo, duplicado, alteracoes apos finalizacao e remocao de consultor inexistente no projeto. | Invariantes dos consultores externos |
| RF15 | O sistema deve permitir adicionar recursos institucionais utilizados por um projeto incubado. | Associacao 1:N por agregacao com recursos institucionais |
| RF16 | O sistema deve permitir remover recursos institucionais de um projeto incubado. | Associacao 1:N por agregacao com recursos institucionais |
| RF17 | O sistema deve validar as operacoes sobre recursos institucionais, impedindo recurso nulo, duplicado, alteracoes apos finalizacao e remocao de recurso inexistente no projeto. | Invariantes dos recursos institucionais |
| RF18 | O sistema deve permitir cadastrar marcos de desenvolvimento de um projeto incubado a partir dos dados informados, criando-os internamente no proprio projeto. | Composicao com marcos de desenvolvimento |
| RF19 | O sistema deve validar os marcos de desenvolvimento, impedindo descricao invalida, data prevista anterior ao inicio do projeto, marco duplicado, inclusao apos finalizacao e finalizacao sem ao menos um marco obrigatorio. | Invariantes dos marcos de desenvolvimento |
| RF20 | O sistema deve permitir registrar avaliacao de marco de desenvolvimento informando membro da equipe, marco, descricao da evidencia e indicacao de aprovacao. | Composicao com avaliacoes dos marcos |
| RF21 | O sistema deve validar o registro de avaliacoes, impedindo avaliacao por estudante que nao seja membro da equipe, avaliacao para marco que nao pertence ao projeto, descricao invalida, avaliacao duplicada do mesmo membro para o mesmo marco e registro apos finalizacao do projeto. | Composicao com avaliacoes dos marcos |
| RF22 | O sistema deve permitir finalizar um projeto incubado. | Finalizacao do projeto incubado |
| RF23 | O sistema deve verificar, antes da finalizacao, se o projeto atende a todas as pre-condicoes de negocio: associacoes obrigatorias validas, ao menos um membro de equipe, ao menos um marco obrigatorio, avaliacao em todos os marcos obrigatorios, ausencia de duplicidades e consistencia entre mentor, coorientador e proponente. | Finalizacao do projeto incubado |
| RF24 | O sistema deve bloquear novas alteracoes estruturais no projeto apos sua finalizacao, impedindo inclusao de membros, consultores, recursos, marcos e avaliacoes. | Finalizacao do projeto incubado |
| RF25 | O sistema deve permitir consultar o percentual de marcos aprovados de um projeto finalizado. | Calculo do percentual de marcos aprovados |
| RF26 | O sistema deve calcular o percentual de marcos aprovados considerando como aprovado cada marco que possua pelo menos uma avaliacao aprovada registrada. | Calculo do percentual de marcos aprovados |
| RF27 | O sistema deve impedir a consulta ao percentual de marcos aprovados antes da finalizacao do projeto. | Calculo do percentual de marcos aprovados |
| RF28 | O sistema deve apresentar, em Program.cs, uma demonstracao de fluxo valido contendo criacao dos objetos principais, criacao do projeto, definicao opcional de coorientador, adicao de membros, consultores, recursos, marcos, registro de avaliacoes, finalizacao e exibicao do percentual de marcos aprovados. | Demonstracao obrigatoria |
| RF29 | O sistema deve apresentar, em Program.cs, pelo menos quatro cenarios invalidos tratados com try/catch e exibicao de mensagens adequadas no console. | Demonstracao obrigatoria |

## Observacoes de engenharia de software

- Os requisitos acima foram redigidos em formato verificavel, buscando deixar claro o comportamento esperado do sistema.
- Regras como encapsulamento, uso de `IReadOnlyCollection<T>`, `private readonly List<T>`, ausencia de frameworks e outras decisoes de implementacao foram tratadas no enunciado como restricoes tecnicas, nao como requisitos funcionais.
- As validacoes de invariantes foram incorporadas aos requisitos funcionais porque afetam diretamente o comportamento observado do sistema.