# aula-poo-github

Projeto acadêmico em C# para modelar um sistema de incubação de projetos inovadores desenvolvidos por estudantes, com foco em Programação Orientada a Objetos.

O repositório está organizado para evoluir um sistema console baseado no enunciado da prova, cobrindo associações, encapsulamento, validações de domínio, proteção de invariantes, agregação e composição.

## Estado atual

Neste momento, o repositório possui:

- estrutura do projeto .NET criada em `TrabalhoPOO`;
- documentação do domínio, requisitos funcionais e casos de uso;
- registro histórico das sessões de trabalho em `TrabalhoPOO/docs/sessoes`;
- implementação inicial das entidades de base:
	- `Curso`;
	- `Estudante`;
	- `ValidadorDominio`.

Ainda não estão implementados os demais elementos centrais do domínio descritos na documentação, como:

- `Professor`;
- `Laboratorio`;
- `ProjetoIncubado`;
- `ConsultorExterno`;
- `RecursoInstitucional`;
- `MarcoDesenvolvimento`;
- `AvaliacaoMarco`;
- fluxo demonstrativo final em `Program.cs`.

Em outras palavras: a base documental do problema está bem definida, mas a implementação ainda está em fase inicial.

## Objetivo do projeto

Implementar um modelo orientado a objetos que represente o domínio de incubação acadêmica, respeitando regras de negócio como:

- criação de objetos sempre em estado válido;
- associação obrigatória entre projeto, estudante proponente, professor mentor e laboratório;
- associação opcional com professor coorientador;
- controle de membros da equipe;
- uso de agregação para consultores externos e recursos institucionais;
- uso de composição para marcos de desenvolvimento e avaliações;
- finalização do projeto apenas quando as pré-condições forem satisfeitas.

## Estrutura do repositório

```text
.
├── README.md
└── TrabalhoPOO/
		├── Program.cs
		├── TrabalhoPOO.csproj
		├── Controllers/
		├── Models/
		│   ├── Agregacoes/
		│   ├── Composicoes/
		│   ├── EntidadesBase/
		│   └── Projetos/
		├── ViewModels/
		├── Views/
		│   └── ConsoleViews/
		└── docs/
				├── dominio_do_sistema/
				│   ├── prova_poo_associacoes_incubadora_projetos_alunos.md
				│   ├── requisitos_funcionais/
				│   └── uses_cases/
				└── sessoes/
```

## Implementação disponível hoje

### `ValidadorDominio`

Classe utilitária responsável por validações reutilizáveis do domínio, incluindo:

- campos obrigatórios;
- validação simples de e-mail com expressão regular.

### `Curso`

Entidade base simples, com nome obrigatório e representação textual via `ToString()`.

### `Estudante`

Entidade base com:

- nome editável com validação;
- matrícula obrigatória;
- e-mail validado;
- associação com `Curso`.

Há dois construtores: um recebendo o nome do curso como texto e outro recebendo um objeto `Curso`.

## Documentação do domínio

Os documentos mais importantes para entendimento do projeto estão em `TrabalhoPOO/docs/dominio_do_sistema`.

Sugestão de leitura:

1. `prova_poo_associacoes_incubadora_projetos_alunos.md`
2. `requisitos_funcionais/requisitos_funcionais_prova_poo_associacoes_incubadora_projetos_alunos.md`
3. `uses_cases/casos_de_uso_incubadora_projetos_alunos.md`

Esses arquivos definem o escopo funcional esperado e servem como referência para a implementação.

## Registro de sessões

O histórico de evolução do trabalho está em `TrabalhoPOO/docs/sessoes`.

Ali ficam registradas decisões, alterações realizadas, status e próximos passos de cada sessão.

## Requisitos de ambiente

- .NET SDK com suporte ao alvo definido no projeto (`net10.0`)
- terminal com `dotnet` disponível no PATH

## Como executar

No estado atual, o projeto ainda executa apenas o programa padrão do template console.

Para restaurar, compilar e executar:

```bash
cd TrabalhoPOO
dotnet run
```

Saída esperada no momento:

```text
Hello, World!
```

## Como compilar

```bash
cd TrabalhoPOO
dotnet build
```

## Próximos passos naturais

- implementar as entidades restantes do domínio;
- criar a classe `ProjetoIncubado` com suas associações e invariantes;
- modelar agregações e composições previstas no enunciado;
- substituir o `Program.cs` atual por uma demonstração válida do fluxo exigido pela prova;
- adicionar cenários inválidos tratados com `try/catch`, conforme os requisitos funcionais.

## Licença

Este repositório possui o arquivo `LICENSE` na raiz. Consulte esse arquivo para os termos aplicáveis.
